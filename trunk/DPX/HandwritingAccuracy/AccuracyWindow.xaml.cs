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
        /// The list of tests that have been run.
        /// </summary>
        private Collection<RecognitionTest> tests = new Collection<RecognitionTest>();

        /// <summary>
        /// The current test.
        /// </summary>
        private RecognitionTest rt;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccuracyWindow"/> class.
        /// </summary>
        public AccuracyWindow()
        {
            InitializeComponent();
            this.NewTest();
        }

        /// <summary>
        /// Load a new test.
        /// </summary>
        private void NewTest()
        {
            this.rt = new RecognitionDoubleTest();
            this.tests.Add(this.rt);
            this.LoadTest(this.rt);
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

            this.NewTest();
        }
    }
}
