// <copyright file="SettingsWindow.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Interaction logic for SettingsWindow.xaml</summary>
namespace HandwritingAccuracy
{
    using System;
    using System.Collections.Generic;
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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
        /// </summary>
        public SettingsWindow()
        {
            InitializeComponent();

            // Load all of the current settings
            this.TextBoxDatabaseName.Text = HandwritingAccuracy.Properties.Settings.Default.DBName;
            this.TextBoxHost.Text = HandwritingAccuracy.Properties.Settings.Default.DBHost;
            this.TextBoxPassword.Text = HandwritingAccuracy.Properties.Settings.Default.DBPass;
            this.TextBoxPortNumber.Text = HandwritingAccuracy.Properties.Settings.Default.DBPort;
            this.TextBoxUserName.Text = HandwritingAccuracy.Properties.Settings.Default.DBUid;
        }

        /// <summary>
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            // Save the new settings
            HandwritingAccuracy.Properties.Settings.Default.DBName = this.TextBoxDatabaseName.Text;
            HandwritingAccuracy.Properties.Settings.Default.DBHost = this.TextBoxHost.Text;
            HandwritingAccuracy.Properties.Settings.Default.DBPass = this.TextBoxPassword.Text;
            HandwritingAccuracy.Properties.Settings.Default.DBPort = this.TextBoxPortNumber.Text;
            HandwritingAccuracy.Properties.Settings.Default.DBUid = this.TextBoxUserName.Text;

            // Close this window
            this.Close();
        }
    }
}
