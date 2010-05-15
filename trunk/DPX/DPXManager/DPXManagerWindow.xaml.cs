// <copyright file="DPXManagerWindow.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The main window of the application.</summary>
namespace DPXManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using DPXCommon;

    /// <summary>
    /// The main window of the application.
    /// </summary>
    public partial class DPXManagerWindow : Window
    {
        /// <summary>
        /// Creates an instance of the singleton controller.
        /// </summary>
        private Controller c = Controller.Instance();

        /// <summary>
        /// Initializes a new instance of the DPXManagerWindow class.
        /// </summary>
        public DPXManagerWindow()
        {
            InitializeComponent();
            FileMenuClose.IsEnabled = false;
            FileMenuRoster.IsEnabled = false;
        }

        /// <summary>
        /// Display the AboutDPX window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void DisplayAboutWindow(object sender, RoutedEventArgs e)
        {
            AboutDPX popupWindow = new AboutDPX();
            popupWindow.Owner = this;
            popupWindow.ShowDialog();
        }

        /// <summary>
        /// Open a database file.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OpenFile(object sender, RoutedEventArgs e)
        {
            // The database is not open
            try
            {
                if (!this.c.IsDatabaseOpen())
                {
                    // Configure open file dialog box
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                    dlg.DefaultExt = ".accdb"; // Default file extension
                    dlg.Filter = "Database File (.accdb)|*.accdb"; // Filter files by extension

                    // Show open file dialog box
                    Nullable<bool> result = dlg.ShowDialog();

                    // Process open file dialog box results
                    if (result == true)
                    {
                        // Open document
                        this.c.OpenDatabaseFile(dlg.FileName);
                        FileMenuOpen.IsEnabled = false;
                        FileMenuClose.IsEnabled = true;
                        FileMenuRoster.IsEnabled = true;
                        FileMenuNew.IsEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Close the database file.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CloseFile(object sender, RoutedEventArgs e)
        {
            this.c.CloseDatabase();
            FileMenuOpen.IsEnabled = true;
            FileMenuClose.IsEnabled = false;
            FileMenuRoster.IsEnabled = false;
            FileMenuNew.IsEnabled = true;
        }

        /// <summary>
        /// Display the Import Roster window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ImportRoster_Click(object sender, RoutedEventArgs e)
        {
            RosterImporter ri = new RosterImporter();
            if (ri.IsOpened)
            {
                ri.ShowDialog();
            }
        }

        /// <summary>
        /// Create a new database file by creating a copy of the template database.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void NewFile(object sender, RoutedEventArgs e)
        {
            try
            {
                // Location of the empty database that is included as part of the installer
                string locationOfNewDatabase = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\NewDPXDatabase.accdb";

                // Remove file:\ from the beginning of the string
                locationOfNewDatabase = locationOfNewDatabase.Substring(6);

                // Configure save file dialog box
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.DefaultExt = ".seat"; // Default file extension
                dlg.Filter = "Database (.accdb)|*.accdb"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    if (System.IO.File.Exists(locationOfNewDatabase))
                    {
                        System.IO.File.Copy(locationOfNewDatabase, dlg.FileName);

                        // Open document
                        this.c.OpenDatabaseFile(dlg.FileName);
                        FileMenuOpen.IsEnabled = false;
                        FileMenuClose.IsEnabled = true;
                        FileMenuRoster.IsEnabled = true;
                        FileMenuNew.IsEnabled = false;
                    }
                    else
                    {
                        MessageBox.Show("File not found: " + locationOfNewDatabase, "Error: file not found");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
