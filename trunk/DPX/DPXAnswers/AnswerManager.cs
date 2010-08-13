// <copyright file="AnswerManager.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The manager for processing the file and interpreting the answers.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Threading;
    using DPXCommon;
    using DPXReader.DyKnow;

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
        /// The list of threads.
        /// </summary>
        private List<Thread> workers;

        /// <summary>
        /// The queue of work that needs to be performed.
        /// </summary>
        private Queue<QueueItem> workerQueue;

        /// <summary>
        /// Prevents a default instance of the <see cref="AnswerManager"/> class from being created.
        /// </summary>
        private AnswerManager()
        {
            // Create the list fo answers
            this.answers = new Dictionary<int, PanelAnswer>();

            // Create the worker queue
            this.workerQueue = new Queue<QueueItem>();

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
                this.answerWindow.SelectedPanelId = n;
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
            // Display the answer information to the user
            Grid g = this.answerWindow.GridRecognizedAnswers;
            g.Children.Clear();
            for (int i = 0; i < panel.Keys.Count; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = GridLength.Auto;
                g.RowDefinitions.Add(rd);

                // Add the panel number
                Label num = new Label();
                num.Content = panel.GetRecognizedString(panel.Keys[i]);
                num.BorderBrush = Brushes.DarkGray;
                num.BorderThickness = new Thickness(1);
                num.Tag = panel.Keys[i];
                num.MouseEnter += new System.Windows.Input.MouseEventHandler(this.answerWindow.AnswerMouseEnter);
                num.MouseLeave += new System.Windows.Input.MouseEventHandler(this.answerWindow.AnswerMouseLeave);
                Grid.SetRow(num, i);
                Grid.SetColumn(num, 0);
                g.Children.Add(num);
            }
        }

        /// <summary>
        /// Processes the panel answer boxes.
        /// </summary>
        /// <param name="n">The panel number.</param>
        internal void ProcessPanelAnswers(int n)
        {
            // Create the answer
            PanelAnswer pa = new PanelAnswer();
            lock (this.answers)
            {
                this.answers.Add(n, pa);
            }

            // Create the new task
            AnswerProcessQueueItem apqi = new AnswerProcessQueueItem(this.dyknow, n, pa);

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
                }
            }
        }
    }
}
