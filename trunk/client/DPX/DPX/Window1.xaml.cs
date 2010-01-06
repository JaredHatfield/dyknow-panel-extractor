// <copyright file="Window1.xaml.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace DPX
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

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        /// <summary>
        /// 
        /// </summary>
        private Controller c = Controller.Instance();

        /// <summary>
        /// 
        /// </summary>
        public Window1()
        {
            InitializeComponent();
            FileMenuClose.IsEnabled = false;
            FileMenuRoster.IsEnabled = false;
            this.c.SetProgressBarMaster(progressBarMaster);
        }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OpenFile(object sender, RoutedEventArgs e)
        {
            // The database is not open
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
                    }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CloseFile(object sender, RoutedEventArgs e)
        {
            this.c.CloseDatabase();
            FileMenuOpen.IsEnabled = true;
            FileMenuClose.IsEnabled = false;
            FileMenuRoster.IsEnabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportRoster_Click(object sender, RoutedEventArgs e)
        {
            RosterImporter ri = new RosterImporter();
            if (ri.IsOpened)
            {
                ri.ShowDialog();
            }
        }
    }
}
