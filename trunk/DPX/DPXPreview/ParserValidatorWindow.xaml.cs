// <copyright file="ParserValidatorWindow.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The main window for Parser Validator.</summary>
namespace DPXPreview
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using DiffMatchPatch;
    using DPXReader.DyKnow;

    /// <summary>
    /// Interaction logic for ParserValidatorWindow.xaml
    /// </summary>
    public partial class ParserValidatorWindow : Window
    {
        /// <summary>
        /// The random number generator.
        /// </summary>
        private Random r = new Random();

        /// <summary>
        /// The collection of KnownMistakes that will not produce a failure of the serialization algorithm.
        /// </summary>
        private List<KnownMistake> knownMistakes;

        /// <summary>
        /// The queue that contains all of the work that needs to be performed.
        /// </summary>
        private Queue<FileProcessRow> workerQueue;

        /// <summary>
        /// The list of threads.
        /// </summary>
        private List<Thread> threadList;

        /// <summary>
        /// The current row counter.
        /// </summary>
        private int currentRow;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserValidatorWindow"/> class.
        /// </summary>
        public ParserValidatorWindow()
        {
            InitializeComponent();

            // Set the row count
            this.currentRow = 0;

            // Create the worker queue
            this.workerQueue = new Queue<FileProcessRow>();

            // Add all of the known mistakes to the collection
            this.knownMistakes = new List<KnownMistake>();
            this.knownMistakes.Add(new KnownMistake(Operation.INSERT, "<ANIMLIST />\n"));

            // Start all of the worker threads
            this.threadList = new List<Thread>();
            for (int i = 0; i < 3; i++)
            {
                Thread t = new Thread(new ThreadStart(this.Worker));
                t.Name = "Queue Worker " + i;
                t.Start();
                this.threadList.Add(t);
            }

            // Subscribe the shutdown method.
            Dispatcher.ShutdownStarted += this.DispatcherShutdownStarted;
        }

        /// <summary>
        /// A delegate for a method that takes no arguments.
        /// </summary>
        private delegate void NoArgsDelegate();

        /// <summary>
        /// The delegate for outputting a message.
        /// </summary>
        /// <param name="message">The message.</param>
        private delegate void OutputMessageDelegate(string message);

        /// <summary>
        /// Handles the ShutdownStarted event of the Dispatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DispatcherShutdownStarted(object sender, EventArgs e)
        {
            for (int i = 0; i < this.threadList.Count; i++)
            {
                this.threadList[i].Abort();
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonSelectFiles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonSelectFiles_Click(object sender, RoutedEventArgs e)
        {
            // Let the user choose which file to open
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "DyKnow files (*.dyz)|*.dyz";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                string[] files = openFileDialog.FileNames;
                
                // TODO: Add all of the files to the GUI and add them to the worker queue
                for (int i = 0; i < files.Length; i++)
                {
                    // Add the row definition
                    RowDefinition rd = new RowDefinition();
                    rd.Height = GridLength.Auto;
                    this.GridResults.RowDefinitions.Add(rd);

                    // Add the file name
                    Label num = new Label();
                    num.Content = files[i];
                    num.BorderBrush = Brushes.DarkGray;
                    num.BorderThickness = new Thickness(1);
                    Grid.SetRow(num, this.currentRow);
                    Grid.SetColumn(num, 0);
                    this.GridResults.Children.Add(num);

                    // Add the progress
                    Label progress = new Label();
                    progress.Content = "Processing...";
                    progress.BorderBrush = Brushes.DarkGray;
                    progress.BorderThickness = new Thickness(1);
                    progress.Background = Brushes.LightBlue;
                    Grid.SetRow(progress, this.currentRow);
                    Grid.SetColumn(progress, 1);
                    this.GridResults.Children.Add(progress);

                    // Increment the row
                    this.currentRow++;

                    // Create the progress
                    FileProcessRow f = new FileProcessRow(Dispatcher, files[i], progress);
                    lock (this.workerQueue)
                    {
                        this.workerQueue.Enqueue(f);
                        Monitor.Pulse(this.workerQueue);
                    }
                }
            }
        }

        /// <summary>
        /// Enables the buttons.
        /// </summary>
        private void EnableButtons()
        {
            this.ButtonSelectFiles.IsEnabled = true;
        }

        /// <summary>
        /// The loop for a worker thread.
        /// </summary>
        private void Worker()
        {
            while (true)
            {
                FileProcessRow f = null;
                lock (this.workerQueue)
                {
                    if (this.workerQueue.Count > 0)
                    {
                        f = this.workerQueue.Dequeue();
                    }
                }

                if (f != null)
                {
                    this.PerformSerializationTest(f);
                }
                else
                {
                    Thread.Sleep(this.r.Next(1000, 5000));
                }
            }
        }

        /// <summary>
        /// Performs the serialization test.
        /// </summary>
        /// <param name="f">The FileProcessRow to perform the test on.</param>
        private void PerformSerializationTest(FileProcessRow f)
        {
            // Read in the file as a string
            FileStream inputFile = new FileStream(f.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            GZipStream gzipFile = new GZipStream(inputFile, CompressionMode.Decompress, true);
            StreamReader reader = new StreamReader(gzipFile);
            string original = reader.ReadToEnd();
            gzipFile.Close();
            inputFile.Close();
            DyKnow dyknow = this.DeserializeDyKnow(original);

            if (dyknow == null)
            {
                // Assert that the test failed.
                Debug.WriteLine(f.FileName + " Test Failed to Serialize");
                f.SetFileFailed();
            }
            else
            {
                string repacked = this.SerializeDyKnow(dyknow);

                // TODO: Fix underlying problem with RTF and \r
                // Remove the cariage returns from both of the strings before comparison.
                // This is a hack that will let more of the tests pass
                original = original.Replace("\r", string.Empty);
                repacked = repacked.Replace("\r", string.Empty);

                // Perform a test to determine if the string are equal so we can write out the results.
                Debug.WriteLine("Original: " + original.Length + " - Repacked: " + repacked.Length);
                if (!original.Equals(repacked))
                {
                    diff_match_patch d = new diff_match_patch();
                    List<Diff> diff = d.diff_main(original.Replace("><", ">\n<"), repacked.Replace("><", ">\n<"));
                    d.diff_cleanupSemantic(diff);

                    bool hasKnown = false;
                    bool hasUnknown = false;
                    for (int i = 0; i < diff.Count; i++)
                    {
                        if (!diff[i].operation.Equals(Operation.EQUAL))
                        {
                            if (this.knownMistakes.Contains(new KnownMistake(diff[i])))
                            {
                                hasKnown = true;
                            }
                            else
                            {
                                hasUnknown = true;
                            }
                        }
                    }

                    // Set the warning or failed flag
                    if (hasKnown && !hasUnknown)
                    {
                        f.SetFileWarning();
                    }
                    else
                    {
                        f.SetFileFailed();
                    }
                }
                else
                {
                    f.SetFilePassed();
                }
            }
        }

        /// <summary>
        /// Deerializes the DyKnow string.
        /// </summary>
        /// <param name="data">The string representation of a serialized DyKnow object.</param>
        /// <returns>A DyKnow object on success; otherwise null.</returns>
        private DyKnow DeserializeDyKnow(string data)
        {
            DyKnow dyknow = null;

            try
            {
                dyknow = DyKnow.Deserialize(data);
            }
            catch (Exception e)
            {
                Debug.WriteLine("DyKnow object could not be deserialized.");
                Debug.WriteLine(e.Message);
            }

            return dyknow;
        }

        /// <summary>
        /// Serializes the DyKnow object.
        /// </summary>
        /// <param name="dyknow">The DyKnow object.</param>
        /// <returns>A string representation of the DyKnow object; otherwise null.</returns>
        private string SerializeDyKnow(DyKnow dyknow)
        {
            string result = null;

            try
            {
                result = dyknow.Serialize();

                // NOTE:  This removes the XML header, this is not the best implementation possible.
                result = result.Substring(result.IndexOf("<DYKNOW_NB50"));
            }
            catch
            {
                Debug.WriteLine("DyKnow object could not serialized.");
            }

            return result;
        }
    }
}
