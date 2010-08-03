// <copyright file="AnswerWindow.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The main application for Preview.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.Generic;
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
        /// Initializes a new instance of the <see cref="AnswerWindow"/> class.
        /// </summary>
        public AnswerWindow()
        {
            InitializeComponent();

            // Set up the AnswerManager.
            this.answerManager = AnswerManager.Instance();
            this.answerManager.SetAnswerWindow(this);

            // Subscribe the shutdown method.
            Dispatcher.ShutdownStarted += this.answerManager.Close;
        }

        /// <summary>
        /// Does the nothing.
        /// </summary>
        private void DoNothing()
        {
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
                this.answerManager.SetFilename(openFileDialog.FileName);
                Thread t = new Thread(new ThreadStart(this.DoNothing));
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
            // TODO: Implement me!
        }
    }
}
