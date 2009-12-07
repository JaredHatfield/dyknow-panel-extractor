// <copyright file="Window1.xaml.cs" company="DPX on Google Code">
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
        Controller c = Controller.Instance();

        public Window1()
        {
            InitializeComponent();
            FileMenuClose.IsEnabled = false;
            c.setProgressBarMaster(progressBarMaster);
        }

        private void DisplayAboutWindow(object sender, RoutedEventArgs e)
        {
            AboutDPX popupWindow = new AboutDPX();
            popupWindow.Owner = this;
            popupWindow.ShowDialog();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            //The database is not open
            if (!c.isDatabaseOpen())
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
                        c.openDatabaseFile(dlg.FileName);
                        FileMenuOpen.IsEnabled = false;
                        FileMenuClose.IsEnabled = true;
                    }
                
            }
        }

        private void CloseFile(object sender, RoutedEventArgs e)
        {
            c.closeDatabase();
            FileMenuOpen.IsEnabled = true;
            FileMenuClose.IsEnabled = false;
        }




    }
}
