// <copyright file="AccuracyWindow.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The window for the accuracy test..</summary>
namespace HandwritingAccuracy
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for AccuracyWindow.xaml
    /// </summary>
    public partial class AccuracyWindow : Window
    {
        /// <summary>
        /// The total number of tests.
        /// </summary>
        private const int TotalTests = 60;

        /// <summary>
        /// A random number generator.
        /// </summary>
        private Random r = new Random();

        /// <summary>
        /// The number of test that have been completed.
        /// </summary>
        private int[] counts;

        /// <summary>
        /// The current test.
        /// </summary>
        private RecognitionTest rt;

        /// <summary>
        /// The experiment id.
        /// </summary>
        private int experimentId;

        /// <summary>
        /// The participant.
        /// </summary>
        private Participant participant;

        /// <summary>
        /// The tablet pc.
        /// </summary>
        private TabletPC tablet;

        /// <summary>
        /// The current test number.
        /// </summary>
        private int currentTestNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccuracyWindow"/> class.
        /// </summary>
        /// <param name="participant">The participant.</param>
        /// <param name="tablet">The tablet.</param>
        public AccuracyWindow(Participant participant, TabletPC tablet)
        {
            this.counts = new int[3];
            this.participant = participant;
            this.tablet = tablet;
            this.currentTestNumber = 0;
            InitializeComponent();

            // Display all of the information on the GUI
            this.TextBoxParticipant.Text = this.participant.LastName + ", " + this.participant.FirstName;
            this.TextBoxTablet.Text = tablet.ToString();

            // Insert the new experiment into the database
            this.experimentId = DatabaseManager.Instance().InsertExperiment(this.participant, this.tablet);
            Debug.WriteLine("The experiment ID is: " + this.experimentId);

            // Enable the required parts of the interface
            this.Inky.IsEnabled = false;
            this.ButtonClear.IsEnabled = false;
            this.ButtonConfirm.IsEnabled = false;
            this.ProgressBar.Maximum = AccuracyWindow.TotalTests;
        }

        /// <summary>
        /// Load a new test.
        /// </summary>
        private void NewTest()
        {
            if (this.currentTestNumber < TotalTests)
            {
                this.ProgressBar.Value = ++this.currentTestNumber;

                while (true)
                {
                    int test = this.r.Next(0, 3);
                    if (this.counts[test] < AccuracyWindow.TotalTests / 3.0)
                    {
                        this.counts[test]++;
                        if (test == 0)
                        {
                            this.rt = new RecognitionDoubleTest();
                        }
                        else if (test == 1)
                        {
                            this.rt = new RecognitionIntegerTest();
                        }
                        else if (test == 2)
                        {
                            this.rt = new RecognitionWordTest();
                        }

                        break;
                    }
                }

                this.LoadTest(this.rt);
            }
            else
            {
                this.LabelPrompt.Content = string.Empty;
                this.ButtonClear.IsEnabled = false;
                this.ButtonConfirm.IsEnabled = false;
            }
        }

        /// <summary>
        /// Loads the test.
        /// </summary>
        /// <param name="test">The test to load.</param>
        private void LoadTest(RecognitionTest test)
        {
            this.LabelPrompt.Content = test.Text;
            this.Inky.Strokes = test.Strokes;
        }

        /// <summary>
        /// Handles the Click event of the ButtonClear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            this.Inky.Strokes.Clear();
        }

        /// <summary>
        /// Handles the Click event of the ButtonConfirm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.TextBoxResults.Text += this.rt.Text + "\t" + this.rt.RecognizedText + "\t";
            if (this.rt.Passed)
            {
                this.TextBoxResults.Text += "Passed\n";
            }
            else
            {
                this.TextBoxResults.Text += "Failed\n";
            }

            // Send the results to the databse
            DatabaseManager.Instance().InsertExperimentRun(this.experimentId, this.currentTestNumber, this.rt);

            this.NewTest();
        }

        /// <summary>
        /// Handles the Click event of the ButtonStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            // Disable the start button
            this.ButtonStart.IsEnabled = false;

            // Enable the required parts of the interface
            this.Inky.IsEnabled = true;
            this.ButtonClear.IsEnabled = true;
            this.ButtonConfirm.IsEnabled = true;

            // Start the experiment
            this.NewTest();
        }
    }
}
