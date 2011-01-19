// <copyright file="AnswerManager.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The manager for processing the file and interpreting the answers.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Threading;
    using DPXCommon;
    using DPXReader.DyKnow;
    using GradeLibrary;

    /// <summary>
    /// The manager for processing the file and interpreting the answers.
    /// </summary>
    internal class AnswerManager
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static AnswerManager instance;

        /// <summary>
        /// The answer window.
        /// </summary>
        private AnswerWindow answerWindow;

        /// <summary>
        /// The clustering algorithms that are available to the application.
        /// </summary>
        private List<IClusterAlgorithm> clusterAlgorithms;

        /// <summary>
        /// The file name that will be opened.
        /// </summary>
        private string filename;

        /// <summary>
        /// The DyKnow file.
        /// </summary>
        private DyKnow dyknow;

        /// <summary>
        /// The collection of answers.
        /// </summary>
        private Dictionary<int, PanelAnswer> answers;

        /// <summary>
        /// The answer rect factory.
        /// </summary>
        private AnswerRectFactory answerRectFactory;

        /// <summary>
        /// The list of threads.
        /// </summary>
        private List<Thread> workers;

        /// <summary>
        /// The queue of work that needs to be performed.
        /// </summary>
        private Queue<QueueItem> workerQueue;

        /// <summary>
        /// The list of grade rows, kept to insure garbage collection.
        /// </summary>
        private List<GradeRow> gradeRows;

        /// <summary>
        /// The answerRect that is currently selected.
        /// </summary>
        private AnswerRect answerRect;

        /// <summary>
        /// Prevents a default instance of the <see cref="AnswerManager"/> class from being created.
        /// </summary>
        private AnswerManager()
        {
            // Create the list fo answers
            this.answers = new Dictionary<int, PanelAnswer>();

            // Make the AnswerRectFactory
            this.answerRectFactory = new AnswerRectFactory();

            // Create the worker queue
            this.workerQueue = new Queue<QueueItem>();

            // Create the list for traking gradeRows
            this.gradeRows = new List<GradeRow>();

            // Load the external DLL files that include the clustering algorithms
            string exeName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string folder = Path.GetDirectoryName(exeName);
            this.clusterAlgorithms = GetPlugins<IClusterAlgorithm>(folder);

            // Start all of the worker threads
            this.workers = new List<Thread>();
            Debug.WriteLine("Starting " + Environment.ProcessorCount + " threads for processing.");
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                Thread t = new Thread(new ThreadStart(this.Worker));
                t.Name = "Queue Worker " + i;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                this.workers.Add(t);
            }
        }

        /// <summary>
        /// The delegate for call with no arguments.
        /// </summary>
        private delegate void NoArgsDelegate();

        /// <summary>
        /// The delegate for display recognized answer.
        /// </summary>
        /// <param name="panel">The panel answer.</param>
        private delegate void DisplayRecognizedAnswerDelegate(PanelAnswer panel);

        /// <summary>
        /// The delegate for displaying a panel.
        /// </summary>
        /// <param name="index">The index of the panel to display.</param>
        private delegate void DisplayPanelDelegate(int index);

        /// <summary>
        /// Returns the singleton instance.
        /// </summary>
        /// <returns>An instance of AnswerManager.</returns>
        public static AnswerManager Instance()
        {
            if (instance == null)
            {
                AnswerManager.instance = new AnswerManager();
            }

            return AnswerManager.instance;
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        internal void Close(object sender, EventArgs e)
        {
            for (int i = 0; i < this.workers.Count; i++)
            {
                this.workers[i].Abort();
            }
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        internal void Cleanup()
        {
            if (this.answerRect != null)
            {
                (this.answerRect.Cluster.Groups as INotifyCollectionChanged).CollectionChanged -= this.AnswerManagerCollectionChanged;
            }

            this.answerRect = null;
        }

        /// <summary>
        /// Sets the answer window.
        /// </summary>
        /// <param name="answerWindow">The answer window.</param>
        internal void SetAnswerWindow(AnswerWindow answerWindow)
        {
            this.answerWindow = answerWindow;
        }

        /// <summary>
        /// Opens the specified DyKnow file.
        /// </summary>
        /// <param name="name">The file name.</param>
        /// <returns>The opened DyKnow file.</returns>
        internal DyKnow OpenFile(string name)
        {
            this.filename = name;
            lock (this.answers)
            {
                this.answers.Clear();
            }

            lock (this.answerRectFactory)
            {
                this.answerRectFactory.Reset();
            }

            this.dyknow = DyKnow.DeserializeFromFile(this.filename);
            return this.dyknow;
        }

        /// <summary>
        /// Displays a panel.
        /// </summary>
        /// <param name="n">The panel to display.</param>
        internal void DisplayPanel(int n)
        {
            if (this.dyknow != null && n >= 0 && n < this.dyknow.DATA.Count)
            {
                if (this.answerWindow.SelectedPanelId >= 0)
                {
                    // Update the panel highlighting and the panel number
                    (this.answerWindow.PanelScrollView.Children[this.answerWindow.SelectedPanelId] as Border).BorderBrush = Brushes.Black;
                    this.answerWindow.SelectedPanelId = n;
                    (this.answerWindow.PanelScrollView.Children[this.answerWindow.SelectedPanelId] as Border).BorderBrush = Brushes.Gold;
                }
                else
                {
                    // Special case where the panel images haven't finished processing so we can update highlighting
                    this.answerWindow.SelectedPanelId = n;
                }

                this.dyknow.Render(this.answerWindow.Inky, n);
                string oner = (this.dyknow.DATA[n] as DPXReader.DyKnow.Page).ONER;
                string onern = (this.dyknow.DATA[n] as DPXReader.DyKnow.Page).ONERN;
                if (oner != null)
                {
                    this.answerWindow.TextBoxStudentName.Text = onern;
                }

                if (onern != null)
                {
                    this.answerWindow.TextBoxUserName.Text = oner;
                }

                // Display the panel answers
                PanelAnswer pa = null;
                lock (this.answers)
                {
                    pa = this.RetreivePanelAnswer(n);
                }

                if (pa != null)
                {
                    this.DisplayRecognizedAnswer(pa);
                    if (!pa.IsProcessed)
                    {
                        pa.DidProcess += new PanelAnswer.PanelProcessedDelegate(this.DisplayRecognizedAnswerDispatch);
                    }
                }
                else
                {
                    // Subscribe the event to update the gui when processing is complete
                    Debug.WriteLine("Panel Answer is null: " + n);
                }
            }
        }

        /// <summary>
        /// Displays the panel request.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DPXAnswers.DisplayPanelEventArgs"/> instance containing the event data.</param>
        internal void DisplayPanelRequest(object sender, DisplayPanelEventArgs e)
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(new DisplayPanelDelegate(this.DisplayPanel), DispatcherPriority.Input, e.Index);
        }

        /// <summary>
        /// Displays the recognized answer dispatch.
        /// </summary>
        internal void DisplayRecognizedAnswerDispatch()
        {
            int id = this.answerWindow.SelectedPanelId;
            PanelAnswer pa = this.RetreivePanelAnswer(id);
            pa.DidProcess -= new PanelAnswer.PanelProcessedDelegate(this.DisplayRecognizedAnswerDispatch);
            this.answerWindow.Dispatcher.BeginInvoke(new DisplayRecognizedAnswerDelegate(this.DisplayRecognizedAnswer), pa);
        }

        /// <summary>
        /// Displays the recognized answers.
        /// </summary>
        /// <param name="panel">The panel answer.</param>
        internal void DisplayRecognizedAnswer(PanelAnswer panel)
        {
            Grid g = this.answerWindow.GridRecognizedAnswers;
            g.Children.Clear();

            // Clean up all of the old grade rows
            for (int i = 0; i < this.gradeRows.Count; i++)
            {
                this.gradeRows[i].Cleanup();
            }

            this.gradeRows.Clear();

            // Display the answer information to the user
            for (int i = 0; i < panel.Keys.Count; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = GridLength.Auto;
                g.RowDefinitions.Add(rd);
                GradeRow gr = new GradeRow(g, this.answerWindow, panel, i);
                this.gradeRows.Add(gr);
            }
        }

        /// <summary>
        /// Displays the answers.
        /// </summary>
        internal void DisplayAnswers()
        {
            // Display the answer information to the user
            this.answerWindow.ComboBoxBoxList.Items.Clear();
            ReadOnlyCollection<AnswerRect> rects = this.answerRectFactory.AnswerRect;
            for (int i = 0; i < rects.Count; i++)
            {
                // Add the value to the drop down list
                AnswerRect ar = rects[i];
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = ar;
                this.answerWindow.ComboBoxBoxList.Items.Add(cbi);

                // Select the first value
                if (i == 0)
                {
                    // This will trigger an event that will display the contents
                    this.answerWindow.ComboBoxBoxList.SelectedIndex = 0;
                }
            }

            this.answerWindow.ComboBoxBoxList.IsEnabled = true;
        }

        /// <summary>
        /// Displays the grade groups.
        /// </summary>
        /// <param name="ar">The AnswerRect to be displayed.</param>
        internal void DisplayGradeGroups(AnswerRect ar)
        {
            if (this.answerRect != null)
            {
                (this.answerRect.Cluster.Groups as INotifyCollectionChanged).CollectionChanged -= this.AnswerManagerCollectionChanged;
            }

            this.answerRect = ar;
            if (ar != null)
            {
                (this.answerRect.Cluster.Groups as INotifyCollectionChanged).CollectionChanged += this.AnswerManagerCollectionChanged;
            }

            this.RefreshGradeGroups(ar);
        }

        /// <summary>
        /// Refreshes the grade groups.
        /// </summary>
        /// <param name="ar">The AnswerRect.</param>
        internal void RefreshGradeGroups(AnswerRect ar)
        {
            Grid g = this.answerWindow.GridGroups;

            // Remove all of the old references from the event handlers for garbage collection purposes
            for (int i = 0; i < g.Children.Count; i++)
            {
                GradeGroup gg = g.Children[i] as GradeGroup;
                gg.DisplayPanel -= this.DisplayPanelRequest;
                gg.Cleanup();
            }

            g.Children.Clear();
            g.RowDefinitions.Clear();

            // Now display all of the answer groups
            if (ar != null)
            {
                for (int j = 0; j < ar.Cluster.Groups.Count; j++)
                {
                    RowDefinition rd = new RowDefinition();
                    rd.Height = GridLength.Auto;
                    g.RowDefinitions.Add(rd);

                    GradeGroup gg = new GradeGroup(ar.Cluster.Groups[j], j);
                    gg.DisplayPanel += this.DisplayPanelRequest;
                    Grid.SetRow(gg, j);
                    Grid.SetColumn(gg, 0);
                    g.Children.Add(gg);
                }
            }
        }

        /// <summary>
        /// Handles the CollectionChanged event of the AnswerManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        internal void AnswerManagerCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.answerRect != null)
            {
                // Refresh all of the groups since we changed one of them
                this.RefreshGradeGroups(this.answerRect);

                // The panel answers should also be refreshed to insure proper event bindings
                PanelAnswer pa = null;
                lock (this.answers)
                {
                    pa = this.RetreivePanelAnswer(this.answerWindow.SelectedPanelId);
                }

                if (pa != null)
                {
                    this.DisplayRecognizedAnswer(pa);
                    if (!pa.IsProcessed)
                    {
                        pa.DidProcess += new PanelAnswer.PanelProcessedDelegate(this.DisplayRecognizedAnswerDispatch);
                    }
                }
            }
        }

        /// <summary>
        /// Processes the panel answer boxes.
        /// </summary>
        /// <param name="n">The panel number.</param>
        internal void ProcessPanelAnswers(int n)
        {
            // Create the answer
            PanelAnswer pa = new PanelAnswer(this.answerRectFactory);
            lock (this.answers)
            {
                this.answers.Add(n, pa);
            }

            // Create the new task
            AnswerProcessQueueItem apqi = new AnswerProcessQueueItem(this.dyknow, n, pa, this.answerWindow.Dispatcher);

            // Add the item to the queue
            lock (this.workerQueue)
            {
                this.workerQueue.Enqueue(apqi);

                // Notify a thread that work is available
                Monitor.Pulse(this.workerQueue);
            }
        }

        /// <summary>
        /// Retreives the panel answer.
        /// </summary>
        /// <param name="n">The panel number.</param>
        /// <returns>The PanelAnswer if it has been processed; otherwise null.</returns>
        internal PanelAnswer RetreivePanelAnswer(int n)
        {
            lock (this.answers)
            {
                if (this.answers.ContainsKey(n))
                {
                    return this.answers[n];
                }
            }

            return null;
        }

        /// <summary>
        /// Run the first clustering algorithm on all of the clusters.
        /// </summary>
        internal void ClusterEverything()
        {
            if (this.clusterAlgorithms.Count > 0)
            {
                for (int i = 0; i < this.answerRectFactory.AnswerRect.Count; i++)
                {
                    AnswerRect ar = this.answerRectFactory.AnswerRect[i];
                    this.clusterAlgorithms[0].Cluster = ar.Cluster;
                    this.clusterAlgorithms[0].Process();
                }
            }
            else
            {
                Debug.WriteLine("No Clustering Algorithm Available!");
            }
        }

        /// <summary>
        /// Creates the output report for a CSV file.
        /// </summary>
        /// <returns>The string that contains the contents of the CSV file.</returns>
        internal string CreateOutputReport()
        {
            StringBuilder sb = new StringBuilder();
            ReadOnlyCollection<AnswerRect> boxes = this.answerRectFactory.AnswerRect;
            sb.Append("Panel,User,Name,");
            foreach (AnswerRect r in boxes)
            {
                sb.Append("Box " + r.Index + " Answer, Box " + r.Index + " Grade,");
            }

            foreach (KeyValuePair<int, PanelAnswer> pa in this.answers)
            {
                DPXReader.DyKnow.Page p = this.dyknow.DATA[pa.Key] as DPXReader.DyKnow.Page;
                sb.Append("\n" + (pa.Key + 1) + ",\"" + p.ONER + "\",\"" + p.ONERN + "\"");
                foreach (AnswerRect r in boxes)
                {
                    BoxAnalysis ba = pa.Value.GetBoxAnalysis(r);
                    if (ba != null)
                    {
                        Grade g = r.Cluster.GetGroup(ba).Label.Grade;
                        sb.Append(",\"" + ba.Answer + "\"," + BoxAnalysis.BoxGradeString(g));
                    }
                    else
                    {
                        sb.Append(",,");
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Determines whether the queue is empty.
        /// </summary>
        /// <returns><c>true</c> if [is queue empty]; otherwise, <c>false</c>.
        /// </returns>
        internal bool IsQueueEmpty()
        {
            // If there are things left in the queue, the queue is not empty
            lock (this.workerQueue)
            {
                if (this.workerQueue.Count != 0)
                {
                    return false;
                }
            }

            // If one of the workers is busy, the queue is not empty.
            lock (this.workers)
            {
                foreach (Thread t in this.workers)
                {
                    if (t.ThreadState == System.Threading.ThreadState.Running)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the plugins.
        /// </summary>
        /// <typeparam name="T">The interface type to initialize.</typeparam>
        /// <param name="folder">The folder.</param>
        /// <returns>The list of plugin objects.</returns>
        private List<T> GetPlugins<T>(string folder)
        {
            string[] files = Directory.GetFiles(folder, "*.dll");
            List<T> tlist = new List<T>();
            Debug.Assert(typeof(T).IsInterface, "The must be an interface");
            foreach (string file in files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (!type.IsClass || type.IsNotPublic)
                        {
                            continue;
                        }

                        Type[] interfaces = type.GetInterfaces();
                        if (((System.Collections.IList)interfaces).Contains(typeof(T)))
                        {
                            object obj = Activator.CreateInstance(type);
                            T t = (T)obj;
                            tlist.Add(t);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            return tlist;
        }

        /// <summary>
        /// The worker thread.
        /// </summary>
        private void Worker()
        {
            while (true)
            {
                QueueItem queueItem = null;
                lock (this.workerQueue)
                {
                    if (this.workerQueue.Count > 0)
                    {
                        queueItem = this.workerQueue.Dequeue();
                    }
                    else
                    {
                        Monitor.Wait(this.workerQueue);
                    }
                }

                if (queueItem != null)
                {
                    queueItem.Run();
                    this.answerWindow.IncrementProgressBar();
                }
            }
        }
    }
}
