// <copyright file="Window1.xaml.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace DyKnowPanelSorter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using QuickReader;

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void OpenDialog_Box(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".dyz"; // Default file extension
            dlg.Filter = "DyKnow Files (.dyz)|*.dyz"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                inputfilename.Text = dlg.FileName;
            }
        }

        private void SaveDialog_Box(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".dyz"; // Default file extension
            dlg.Filter = "DyKnow Files (.dyz)|*.dyz"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                outputfilename.Text = dlg.FileName;
            }
        }

        private void InputFileName_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Nothing here yet.
        }

        private void OutputFileName_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Nothing here yet.
        }

        private void ProcessSort_Click(object sender, RoutedEventArgs e)
        {
            // Perform some checks so we don't do any damage
            if (!File.Exists(inputfilename.Text))
            {
                MessageBox.Show("Source file does not exist!", "Alert");
            }
            else if (File.Exists(outputfilename.Text))
            {
                MessageBox.Show("Destination file already exists!", "Alert");
            }
            else if (outputfilename.Text.Length > 0)
            {
                // Lets make sure something is in paths that were chosen
                PanelSorter ps = new PanelSorter(inputfilename.Text, outputfilename.Text);
                if (radioButtonSN.IsChecked == true)
                {
                    ps.SetSortByFullName();
                }
                else if (radioButtonUN.IsChecked == true)
                {
                    ps.SetSortByUsername();
                }

                ps.ProcessSort();
                MessageBox.Show("File sort finished.", "Success");
            }
            else
            {
                MessageBox.Show("A destination file must be specified.", "Alert");
            }
        }

        private void DisplayAboutWindow(object sender, RoutedEventArgs e)
        {
            AboutDPX popupWindow = new AboutDPX();
            popupWindow.Owner = this;
            popupWindow.ShowDialog();
        }
    }
}
