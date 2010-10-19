// <copyright file="AnswerWindow.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The main application for Preview.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using DPXReader.DyKnow;

    /// <summary>
    /// Interaction logic for AnswerWindow.xaml
    /// </summary>
    public partial class AnswerWindow : Window
    {
        /// <summary>
        /// The instance of the answer manager that is used by the application.
        /// </summary>
        private AnswerManager answerManager;

        /// <summary>
        /// The name of the file that is being opened.
        /// </summary>
        private string filename;

        /// <summary>
        /// The panel number that is selected.
        /// </summary>
        private int selectedPanelId;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerWindow"/> class.
        /// </summary>
        public AnswerWindow()
        {
            InitializeComponent();
            this.filename = null;
            this.selectedPanelId = -1;

            // Set up the AnswerManager.
            this.answerManager = AnswerManager.Instance();
            this.answerManager.SetAnswerWindow(this);

            // Subscribe the shutdown method.
            Dispatcher.ShutdownStarted += this.answerManager.Close;
        }

        /// <summary>
        /// The delegate for displaying a panel.
        /// </summary>
        /// <param name="index">The index of the panel to display.</param>
        public delegate void DisplayPanelDelegate(int index);

        /// <summary>
        /// The delegate for call with no arguments.
        /// </summary>
        private delegate void NoArgsDelegate();

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
            Dispatcher.BeginInvoke(new DisplayPanelDelegate(this.answerManager.DisplayPanel), DispatcherPriority.Input, panelIndex);
        }

        /// <summary>
        /// Handles the mouse over for an answer box rectangle.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs" /> instance containing the event data.</param>
        internal void AnswerMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Label l = (Label)sender;
            l.Background = Brushes.LightYellow;
            AnswerRect ar = (AnswerRect)l.Tag;
            Rect rect = PanelAnswer.Transform(ar.Area, Inky.Width, Inky.Height);
            this.Boxy.Height = rect.Height;
            this.Boxy.Width = rect.Width;
            Canvas.SetTop(this.Boxy, rect.Top);
            Canvas.SetLeft(this.Boxy, rect.Left);
        }

        /// <summary>
        /// Handles the mouse leave for an answer box rectangle.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        internal void AnswerMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Label l = (Label)sender;
            l.Background = Brushes.White;
            this.Boxy.Height = 0;
            this.Boxy.Width = 0;
        }

        /// <summary>
        /// Loads the DyKnow file.
        /// </summary>
        private void LoadDyKnowFile()
        {
            string file = this.filename;

            // Reset the GUI
            Dispatcher.Invoke(new NoArgsDelegate(this.ClearInterface), DispatcherPriority.Normal);
            this.selectedPanelId = -1;

            // Read in the file
            DyKnow dyknow = this.answerManager.OpenFile(file);
            int goal = dyknow.DATA.Count + 1;

            // Display the first panel
            if (dyknow.DATA.Count > 0)
            {
                this.answerManager.ProcessPanelAnswers(0);
                Dispatcher.Invoke(new DisplayPanelDelegate(this.answerManager.DisplayPanel), DispatcherPriority.Input, 0);
                this.selectedPanelId = 0;
            }

            // Loop through all of the panels
            for (int i = 0; i < dyknow.DATA.Count; i++)
            {
                // Process the panels answer boxes
                if (i > 0)
                {
                    this.answerManager.ProcessPanelAnswers(i);
                }

                // Generate and display the thumbnail
                ImageProcessQueueItem tqi = new ImageProcessQueueItem(this, dyknow, i);
                Dispatcher.BeginInvoke(new NoArgsDelegate(tqi.Run), DispatcherPriority.Background);
            }

            // Wait for the queue to empty and everything to process
            while (!this.answerManager.IsQueueEmpty())
            {
                Thread.Sleep(500);
            }

            // Wait until the dispatcher has flushed its queue which is filled with thumbnail render requests
            Dispatcher.Invoke(new NoArgsDelegate(this.DoNothing), DispatcherPriority.ContextIdle);

            // Display the AnswerBox results in the answer column
            Dispatcher.Invoke(new NoArgsDelegate(this.answerManager.DisplayAnswers), DispatcherPriority.Background);

            // Update the interface to indicate that the processing has finished
            Dispatcher.Invoke(new NoArgsDelegate(this.ReEnableOpen), DispatcherPriority.ContextIdle);
        }

        /// <summary>
        /// Does the nothing.
        /// </summary>
        private void DoNothing()
        {
        }

        /// <summary>
        /// Clears the interface.
        /// </summary>
        private void ClearInterface()
        {
            this.GridRecognizedAnswers.Children.Clear();
            this.GridResults.Children.Clear();
            this.Inky.Children.Clear();
            this.Inky.Strokes.Clear();
            this.PanelScrollView.Children.Clear();
            this.TextBoxStudentName.Text = string.Empty;
            this.TextBoxUserName.Text = string.Empty;
        }

        /// <summary>
        /// Re-Enable the open button.
        /// </summary>
        private void ReEnableOpen()
        {
            this.ButtonOpen.IsEnabled = true;
            this.ButtonSave.IsEnabled = true;
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
                this.ButtonSave.IsEnabled = false;

                // Open the DyKnow file
                this.filename = openFileDialog.FileName;
                Thread t = new Thread(new ThreadStart(this.LoadDyKnowFile));
                t.Name = "OpenFileThread";
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }
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
                    sr.Write(this.answerManager.CreateOutputReport());
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
