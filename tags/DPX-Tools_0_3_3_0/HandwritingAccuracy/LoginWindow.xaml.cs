// <copyright file="LoginWindow.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The window for logging in.</summary>
namespace HandwritingAccuracy
{
    using System;
    using System.Collections.ObjectModel;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginWindow"/> class.
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
            DatabaseManager db = DatabaseManager.Instance();
            Collection<TabletPC> r = db.GetAllTablets();
            this.ComboBoxTablet.ItemsSource = r;
        }

        /// <summary>
        /// Handles the Click event of the ButtonLogin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            TabletPC t = this.ComboBoxTablet.SelectedItem as TabletPC;

            if (t == null)
            {
                MessageBox.Show("Tablet not selected");
            }
            else
            {
                Participant p = DatabaseManager.Instance().GetParticipant(this.TextBoxFirstName.Text, this.TextBoxLastName.Text);

                if (p == null)
                {
                    MessageBox.Show("User not found!");
                }
                else
                {
                    AccuracyWindow a = new AccuracyWindow(p, t);
                    a.ShowDialog();
                }
            }
        }
    }
}
