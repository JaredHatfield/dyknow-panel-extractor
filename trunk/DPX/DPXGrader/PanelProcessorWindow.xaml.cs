// <copyright file="PanelProcessorWindow.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The interface for processing panels and producing output.</summary>
namespace DPXGrader
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using DPXCommon;
    using DPXReader.DyKnow;

    /// <summary>
    /// Interaction logic for PanelProcessorWindow.xaml
    /// </summary>
    public partial class PanelProcessorWindow : Window
    {
        /// <summary>
        /// The dyknow file that is read in.
        /// </summary>
        private DyKnow dyknow;

        /// <summary>
        /// The collection of results.
        /// </summary>
        private Collection<string[]> results;

        /// <summary>
        /// The size of the grade box.
        /// </summary>
        private int boxSize;

        /// <summary>
        /// The location of the grade box.
        /// </summary>
        private BoxLocation boxLocation;

        /// <summary>
        /// The panel number that is selected.
        /// </summary>
        private int selectedPanelId;

        /// <summary>
        /// The name of the file that is being opened.
        /// </summary>
        private string fileName;

        /// <summary>
        /// The list of threads.
        /// </summary>
        private List<Thread> workers;

        /// <summary>
        /// The queue of work that needs to be performed.
        /// </summary>
        private Queue<QueueItem> workerQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelProcessorWindow"/> class.
        /// </summary>
        public PanelProcessorWindow()
        {
            InitializeComponent();

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

            // Set up everything else
            this.boxSize = 50;
            this.boxLocation = BoxLocation.TopLeft;
            this.selectedPanelId = -1;
            this.results = new Collection<string[]>();
            this.DisableStepTwo();
            this.DisableStepThree();

            // Subscribe the shutdown method.
            Dispatcher.ShutdownStarted += this.DispatcherShutdownStarted;
        }

        /// <summary>
        /// The delegate for adding a row to the results table.
        /// </summary>
        /// <param name="number">The panel number.</param>
        /// <param name="onern">The name on the panel.</param>
        /// <param name="oner">The username on the panel.</param>
        /// <param name="text">The recognized text.</param>
        /// <param name="value">The numeric version of the recognized text.</param>
        internal delegate void AddRowToResultsDelegate(int number, string onern, string oner, string text, double value);

        /// <summary>
        /// The delegate for updating the progress bar.
        /// </summary>
        /// <param name="progress">The current progress for the bar.</param>
        /// <param name="goal">The goal for the progress bar. </param>
        internal delegate void UpdateProgressBarDelegate(int progress, int goal);

        /// <summary>
        /// The delegate for call with no arguments.
        /// </summary>
        private delegate void NoArgsDelegate();

        /// <summary>
        /// The delegate for displaying a panel.
        /// </summary>
        /// <param name="number">The panel number.</param>
        private delegate void DisplayPanelDelegate(int number);

        /// <summary>
        /// The delegate for creating and adding a thumbnail to the timeline.
        /// </summary>
        /// <param name="number">The panel number.</param>
        private delegate void AddThumnailDelegate(int number);

        /// <summary>
        /// The possible locations of the grade box
        /// </summary>
        private enum BoxLocation
        {
            /// <summary>
            /// Location represented by the top left corner.
            /// </summary>
            TopLeft,

            /// <summary>
            /// Location represented by the top right corner.
            /// </summary>
            TopRight,

            /// <summary>
            /// Location represented by the bottom left corner.
            /// </summary>
            BottomLeft,

            /// <summary>
            /// Location represented by the bottom right corner.
            /// </summary>
            BottomRight
        }

        /// <summary>
        /// Gets the results.
        /// </summary>
        /// <value>The results.</value>
        internal Collection<string[]> Results
        {
            get { return this.results; }
        }

        /// <summary>
        /// Gets the DyKnow file.
        /// </summary>
        /// <value>The DyKnow file.</value>
        internal DyKnow DyKnow
        {
            get { return this.dyknow; }
        }

        /// <summary>
        /// Gets or sets the selected panel id.
        /// </summary>
        /// <value>The selected panel id.</value>
        internal int SelectedPanelId
        {
            get { return this.selectedPanelId; }
            set { this.selectedPanelId = value; }
        }

        /// <summary>
        /// Updates the process progress bar.
        /// </summary>
        /// <param name="progress">The current progress.</param>
        /// <param name="goal">The goal for the bar.</param>
        internal void UpdateProcessProgressBar(int progress, int goal)
        {
            this.ProgressBarProcess.Maximum = goal;
            this.ProgressBarProcess.Value = progress;
        }

        /// <summary>
        /// Gets the selected area.
        /// </summary>
        /// <returns>The rectangle that represents the area.</returns>
        internal Rect GetRectangleArea()
        {
            if (this.boxLocation.Equals(BoxLocation.TopLeft))
            {
                return new Rect(0, 0, this.boxSize, this.boxSize);
            }
            else if (this.boxLocation.Equals(BoxLocation.TopRight))
            {
                return new Rect(400 - this.boxSize, 0, this.boxSize, this.boxSize);
            }
            else if (this.boxLocation.Equals(BoxLocation.BottomLeft))
            {
                return new Rect(0, 300 - this.boxSize, this.boxSize, this.boxSize);
            }
            else if (this.boxLocation.Equals(BoxLocation.BottomRight))
            {
                return new Rect(400 - this.boxSize, 300 - this.boxSize, this.boxSize, this.boxSize);
            }

            throw new Exception("Rectangle could not be generated");
        }

        /// <summary>
        /// Adds the row to results.
        /// </summary>
        /// <param name="i">The panel number.</param>
        /// <param name="onern">The onern value.</param>
        /// <param name="oner">The oner value.</param>
        /// <param name="text">The recognized text.</param>
        /// <param name="value">The numeric value.</param>
        internal void AddRowToResults(int i, string onern, string oner, string text, double value)
        {
            RowDefinition rd = new RowDefinition();
            rd.Height = GridLength.Auto;
            this.GridResults.RowDefinitions.Add(rd);

            // Add the panel number
            Label num = new Label();
            num.Content = (i + 1).ToString();
            num.BorderBrush = Brushes.DarkGray;
            num.BorderThickness = new Thickness(1);
            Grid.SetRow(num, i);
            Grid.SetColumn(num, 0);
            this.GridResults.Children.Add(num);

            // Add the name
            Label name = new Label();
            name.Content = onern;
            name.BorderBrush = Brushes.DarkGray;
            name.BorderThickness = new Thickness(1);
            Grid.SetRow(name, i);
            Grid.SetColumn(name, 1);
            this.GridResults.Children.Add(name);

            // Add the username
            Label user = new Label();
            user.Content = oner;
            user.BorderBrush = Brushes.DarkGray;
            user.BorderThickness = new Thickness(1);
            Grid.SetRow(user, i);
            Grid.SetColumn(user, 2);
            this.GridResults.Children.Add(user);

            // Add the recognized text
            Label cont = new Label();
            cont.Content = text;
            cont.BorderBrush = Brushes.DarkGray;
            cont.BorderThickness = new Thickness(1);
            Grid.SetRow(cont, i);
            Grid.SetColumn(cont, 3);
            this.GridResults.Children.Add(cont);

            // Add the recognized text converted to a number
            Label digit = new Label();
            digit.Content = value;
            digit.BorderBrush = Brushes.DarkGray;
            digit.BorderThickness = new Thickness(1);
            Grid.SetRow(digit, i);
            Grid.SetColumn(digit, 4);
            this.GridResults.Children.Add(digit);
        }

        /// <summary>
        /// Event that is fired when a panel in the scroll view is selected.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        internal void PanelSelected(object sender, EventArgs e)
        {
            (this.PanelScrollView.Children[this.selectedPanelId] as Border).BorderBrush = Brushes.Black;
            Border b = sender as Border;
            int panelIndex = (int)b.Tag;
            this.selectedPanelId = panelIndex;
            b.BorderBrush = Brushes.Gold;
            this.DisplayPanel((int)b.Tag);
        }

        /// <summary>
        /// Handles the ShutdownStarted event of the Dispatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DispatcherShutdownStarted(object sender, EventArgs e)
        {
            for (int i = 0; i < this.workers.Count; i++)
            {
                this.workers[i].Abort();
            }
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

        /// <summary>
        /// Disables the part of the interface related to step two.
        /// </summary>
        private void DisableStepTwo()
        {
            this.TextBoxPreviewOutput.IsEnabled = false;
            this.TextBoxStudentName.IsEnabled = false;
            this.TextBoxUserName.IsEnabled = false;
            this.ButtonPreview.IsEnabled = false;
            this.ButtonProcess.IsEnabled = false;
        }

        /// <summary>
        /// Enables the part of the interface related to step two.
        /// </summary>
        private void EnableStepTwo()
        {
            this.TextBoxPreviewOutput.IsEnabled = true;
            this.TextBoxStudentName.IsEnabled = true;
            this.TextBoxUserName.IsEnabled = true;
            this.ButtonPreview.IsEnabled = true;
            this.ButtonProcess.IsEnabled = true;
        }

        /// <summary>
        /// Disables the part of the interface related to step three.
        /// </summary>
        private void DisableStepThree()
        {
            this.ButtonSave.IsEnabled = false;
        }

        /// <summary>
        /// Enables the part of the interface related to step three.
        /// </summary>
        private void EnableStepThree()
        {
            this.ButtonSave.IsEnabled = true;
        }

        /// <summary>
        /// Redraws the rectangle on the canvas based on the new location and size.
        /// </summary>
        private void RedrawRectangle()
        {
            // Set the size of the box
            this.Boxy.Width = this.boxSize;
            this.Boxy.Height = this.boxSize;

            // Set the relative position of the box
            if (this.boxLocation.Equals(BoxLocation.TopLeft))
            {
                Canvas.SetLeft(this.Boxy, 0);
                Canvas.SetTop(this.Boxy, 0);
            }
            else if (this.boxLocation.Equals(BoxLocation.TopRight))
            {
                Canvas.SetLeft(this.Boxy, this.Inky.Width - this.boxSize);
                Canvas.SetTop(this.Boxy, 0);
            }
            else if (this.boxLocation.Equals(BoxLocation.BottomLeft))
            {
                Canvas.SetLeft(this.Boxy, 0);
                Canvas.SetTop(this.Boxy, this.Inky.Height - this.boxSize);
            }
            else if (this.boxLocation.Equals(BoxLocation.BottomRight))
            {
                Canvas.SetLeft(this.Boxy, this.Inky.Width - this.boxSize);
                Canvas.SetTop(this.Boxy, this.Inky.Height - this.boxSize);
            }
        }

        /// <summary>
        /// Loads the DyKnow file.
        /// </summary>
        private void LoadDyKnowFile()
        {
            string file = this.fileName;
            Dispatcher.Invoke(new UpdateProgressBarDelegate(this.UpdateOpenProgressBar), DispatcherPriority.Normal, 0, 1);
            Dispatcher.Invoke(new UpdateProgressBarDelegate(this.UpdateProcessProgressBar), DispatcherPriority.Input, 0, 1);

            // Reset the GUI
            Dispatcher.Invoke(new NoArgsDelegate(this.ClearInterface), DispatcherPriority.Normal);
            this.selectedPanelId = -1;

            // Read in the file
            this.dyknow = DyKnow.DeserializeFromFile(file);
            int goal = this.dyknow.DATA.Count + 1;

            // Set up the progress bar.
            Dispatcher.Invoke(new UpdateProgressBarDelegate(this.UpdateOpenProgressBar), DispatcherPriority.Input, 1, goal);

            // Display the first panel
            if (this.dyknow.DATA.Count > 0)
            {
                Dispatcher.Invoke(new DisplayPanelDelegate(this.DisplayPanel), DispatcherPriority.Input, 0);
                this.selectedPanelId = 0;
            }

            // Render and add all of the thumbnails
            for (int i = 0; i < this.dyknow.DATA.Count; i++)
            {
                ThumbnailQueueItem tqi = new ThumbnailQueueItem(this, i);
                Dispatcher.BeginInvoke(new NoArgsDelegate(tqi.Run), DispatcherPriority.Background);
                Dispatcher.BeginInvoke(new UpdateProgressBarDelegate(this.UpdateOpenProgressBar), DispatcherPriority.Background, i, goal);
            }

            Dispatcher.BeginInvoke(new NoArgsDelegate(this.EnableStepTwo), DispatcherPriority.ContextIdle);
            Dispatcher.BeginInvoke(new UpdateProgressBarDelegate(this.UpdateOpenProgressBar), DispatcherPriority.Background, goal, goal);
            Dispatcher.BeginInvoke(new NoArgsDelegate(this.ReEnableOpen), DispatcherPriority.ContextIdle);
        }

        // Methods that are dispatched to update the GUI

        /// <summary>
        /// Clears the interface.
        /// </summary>
        private void ClearInterface()
        {
            this.DisableStepTwo();
            this.DisableStepThree();
            this.GridResults.Children.Clear();
            this.Inky.Children.Clear();
            this.Inky.Strokes.Clear();
            this.PanelScrollView.Children.Clear();
            this.TextBoxPreviewOutput.Text = string.Empty;
            this.TextBoxStudentName.Text = string.Empty;
            this.TextBoxUserName.Text = string.Empty;
            this.TextBoxFileName.Text = this.fileName;
        }

        /// <summary>
        /// Updates the open progress bar.
        /// </summary>
        /// <param name="progress">The current progress.</param>
        /// <param name="goal">The goal for the bar.</param>
        private void UpdateOpenProgressBar(int progress, int goal)
        {
            this.ProgressBarOpen.Maximum = goal;
            this.ProgressBarOpen.Value = progress;
        }

        /// <summary>
        /// Displays a panel.
        /// </summary>
        /// <param name="n">The panel to display.</param>
        private void DisplayPanel(int n)
        {
            if (this.dyknow != null && n >= 0 && n < this.dyknow.DATA.Count)
            {
                this.selectedPanelId = n;
                this.dyknow.Render(this.Inky, n);
                string oner = (this.dyknow.DATA[n] as DPXReader.DyKnow.Page).ONER;
                string onern = (this.dyknow.DATA[n] as DPXReader.DyKnow.Page).ONERN;
                if (oner != null)
                {
                    this.TextBoxStudentName.Text = onern;
                }

                if (onern != null)
                {
                    this.TextBoxUserName.Text = oner;
                }
            }
        }

        /// <summary>
        /// Re-Enable the open button.
        /// </summary>
        private void ReEnableOpen()
        {
            this.ButtonOpen.IsEnabled = true;
        }

        /// <summary>
        /// Re-Enable the process button.
        /// </summary>
        private void ReEnableProcess()
        {
            this.ButtonOpen.IsEnabled = true;
            this.ButtonPreview.IsEnabled = true;
            this.ButtonProcess.IsEnabled = true;
        }

        /// <summary>
        /// Clears the results.
        /// </summary>
        private void ClearResults()
        {
            this.results.Clear();
            this.GridResults.Children.Clear();
            this.DisableStepThree();
        }

        /// <summary>
        /// Disables the box controls for the slider and position.
        /// </summary>
        private void DisableBoxControls()
        {
            this.SliderSize.IsEnabled = false;
            this.RadioButtonBottomLeft.IsEnabled = false;
            this.RadioButtonBottomRight.IsEnabled = false;
            this.RadioButtonTopLeft.IsEnabled = false;
            this.RadioButtonTopRight.IsEnabled = false;
        }

        /// <summary>
        /// Enables the box controls for the slider and position.
        /// </summary>
        private void EnableBoxControls()
        {
            this.SliderSize.IsEnabled = true;
            this.RadioButtonBottomLeft.IsEnabled = true;
            this.RadioButtonBottomRight.IsEnabled = true;
            this.RadioButtonTopLeft.IsEnabled = true;
            this.RadioButtonTopRight.IsEnabled = true;
        }

        /// <summary>
        /// Do all of the crunching for the processes step.
        /// </summary>
        private void ProcessStep()
        {
            Dispatcher.Invoke(new NoArgsDelegate(this.DisableBoxControls), DispatcherPriority.Normal);
            Dispatcher.Invoke(new NoArgsDelegate(this.ClearResults), DispatcherPriority.Normal);
            int goal = this.dyknow.DATA.Count;
            Dispatcher.Invoke(new UpdateProgressBarDelegate(this.UpdateProcessProgressBar), DispatcherPriority.Input, 0, goal);

            // Add everything to the queue to be processed
            for (int i = 0; i < this.dyknow.DATA.Count; i++)
            {
                AnalyzerQueueItem aqi = new AnalyzerQueueItem(this, i);
                lock (this.workerQueue)
                {
                    this.workerQueue.Enqueue(aqi);
                }
            }

            // Wake up the workers so we can get some stuff done!
            lock (this.workerQueue)
            {
                Monitor.PulseAll(this.workerQueue);
            }

            // Wait until the results queue has been completely filled
            while (true)
            {
                lock (this.results)
                {
                    if (this.results.Count == goal)
                    {
                        break;
                    }
                    else
                    {
                        // We won't check again until the results set changes
                        Monitor.Wait(this.results);
                    }
                }
            }

            // Sort the reqults queue (not efficient, but it should already be nearly sorted)
            lock (this.results)
            {
                for (int pass = 1; pass < this.results.Count; pass++)
                {
                    for (int i = 0; i < this.results.Count - 1; i++)
                    {
                        if (Int32.Parse(this.results[i][0]) > Int32.Parse(this.results[i + 1][0]))
                        {
                            string[] tmp = this.results[i];
                            this.results[i] = this.results[i + 1];
                            this.results[i + 1] = tmp;
                        }
                    }
                }
            }

            // Update the GUI to indicate that the file processing had concluded
            Dispatcher.BeginInvoke(new NoArgsDelegate(this.EnableBoxControls), DispatcherPriority.Input);
            Dispatcher.BeginInvoke(new NoArgsDelegate(this.ReEnableProcess), DispatcherPriority.Input);
            Dispatcher.BeginInvoke(new NoArgsDelegate(this.EnableStepThree), DispatcherPriority.Input);
            Dispatcher.BeginInvoke(new UpdateProgressBarDelegate(this.UpdateProcessProgressBar), DispatcherPriority.Input, goal, goal);
        }

        /// <summary>
        /// Handles the Checked event of the RadioButtonPosition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void RadioButtonPosition_Checked(object sender, RoutedEventArgs e)
        {
            if (this.RadioButtonTopLeft.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.TopLeft;
            }
            else if (this.RadioButtonTopRight.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.TopRight;
            }
            else if (this.RadioButtonBottomLeft.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.BottomLeft;
            }
            else if (this.RadioButtonBottomRight.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.BottomRight;
            }

            // Redraw the rectangle based on the new settings
            this.RedrawRectangle();
        }

        /// <summary>
        /// Handles the ValueChanged event of the SliderSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedPropertyChangedEventArgs&lt;System.Double&gt;"/> instance containing the event data.</param>
        private void SliderSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.boxSize = (int)this.SliderSize.Value;

            // Redraw the rectangle based on the new settings
            this.RedrawRectangle();
        }

        /// <summary>
        /// Handles the Click event of the ButtonOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            // Let the user choose which file to open
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "DyKnow files (*.dyz)|*.dyz";
            if (openFileDialog.ShowDialog() == true)
            {
                this.ButtonOpen.IsEnabled = false;

                // Open the DyKnow file
                this.fileName = openFileDialog.FileName;
                Thread t = new Thread(new ThreadStart(this.LoadDyKnowFile));
                t.Name = "OpenFileThread";
                t.Start();
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonPreview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonPreview_Click(object sender, RoutedEventArgs e)
        {
            StrokeCollection strokes = this.Inky.Strokes.Clone();
            strokes.Clip(this.GetRectangleArea());
            if (strokes.Count > 0)
            {
                try
                {
                    InkAnalyzer theInkAnalyzer = InkAnalysisHelper.Analyze(strokes, 1);
                    this.TextBoxPreviewOutput.Text = theInkAnalyzer.GetRecognizedString();
                }
                catch
                {
                    this.TextBoxPreviewOutput.Text = string.Empty;
                }
            }
            else
            {
                this.TextBoxPreviewOutput.Text = string.Empty;
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonProcess control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonProcess_Click(object sender, RoutedEventArgs e)
        {
            this.ButtonOpen.IsEnabled = false;
            this.ButtonPreview.IsEnabled = false;
            this.ButtonProcess.IsEnabled = false;

            // Start processing all of the DyKnow files
            Thread t = new Thread(new ThreadStart(this.ProcessStep));
            t.Name = "ProcessThread";
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        /// <summary>
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Let the user choose which file to open
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.Filter = "CSV (*.csv)|*.csv";
                if (saveFileDialog.ShowDialog() == true)
                {
                    // Save the CSV file
                    string file = saveFileDialog.FileName;
                    StreamWriter sr = new StreamWriter(file);

                    for (int i = 0; i < this.results.Count; i++)
                    {
                        string[] val;
                        lock (this.results)
                        {
                            val = this.results[i];
                        }

                        for (int j = 0; j < val.Length; j++)
                        {
                            // Remove any line breaks that may be part of the elements and replace them with spaces
                            if (val[j] != null && val[j].Contains(Environment.NewLine))
                            {
                                val[j] = val[j].Replace(Environment.NewLine, " ");
                            }

                            // Any of the strings that contain a comma need to be surrounded with double quotes
                            if (val[j] != null && val[j].Contains(','))
                            {
                                // TODO: What if there are double quotes contained within the string, these should be escaped.
                                val[j] = "\"" + val[j] + "\"";
                            }
                        }

                        string output = string.Join(",", val);
                        sr.WriteLine(output);
                    }

                    sr.Close();
                }
                else
                {
                    MessageBox.Show("Error: The file could not be saved.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The file was not saved successfully.");
                Debug.WriteLine("File was not saved: " + ex.Message);
            }
        }
    }
}
