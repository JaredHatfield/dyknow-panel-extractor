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
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuickReader;
using DPXDatabase;

namespace DPX
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class ImportFile : Page
    {   
        Controller c = Controller.Instance();
        DyKnowReader dr;

        public ImportFile()
        {
            InitializeComponent();
            //grid1.ShowGridLines = true;
            textBoxSaveFileName.IsReadOnly = true;
            grid1.Background = Brushes.White;
            buttonImport.IsEnabled = false;

            c.setComboBoxDateImport(comboBoxDates);
        }


        public void clearDyKnowGrid()
        {
            grid1.Children.Clear();
            grid1.RowDefinitions.Clear();
        }

        private void displayFile(String file)
        {
            dr = new DyKnowReader(file);
            clearDyKnowGrid();

            buttonImport.IsEnabled = true;


            for (int i = 0; i < dr.NumOfPages(); i++)
            {
                DyKnowPage dp = dr.getDyKnowPage(i);

                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(30);
                grid1.RowDefinitions.Add(rd);

                Label num = new Label();
                num.Content = (i + 1).ToString();
                Grid.SetColumn(num, 0);
                Grid.SetRow(num, i);
                num.BorderThickness = new Thickness(1);
                num.BorderBrush = Brushes.Black;
                grid1.Children.Add(num);

                Label user = new Label();
                user.Content = dp.UserName;
                Grid.SetColumn(user, 1);
                Grid.SetRow(user, i);
                user.BorderThickness = new Thickness(1);
                user.BorderBrush = Brushes.Black;
                grid1.Children.Add(user);

                Label name = new Label();
                name.Content = dp.FullName;
                Grid.SetColumn(name, 2);
                Grid.SetRow(name, i);
                name.BorderThickness = new Thickness(1);
                name.BorderBrush = Brushes.Black;
                grid1.Children.Add(name);

                Label scount = new Label();
                scount.Content = dp.NetStrokeCount;
                Grid.SetColumn(scount, 3);
                Grid.SetRow(scount, i);
                scount.BorderThickness = new Thickness(1);
                scount.BorderBrush = Brushes.Black;
                grid1.Children.Add(scount);

                Label analysis = new Label();
                analysis.Content = dp.Finished;
                Grid.SetColumn(analysis, 4);
                Grid.SetRow(analysis, i);
                analysis.BorderThickness = new Thickness(1);
                analysis.BorderBrush = Brushes.Black;
                grid1.Children.Add(analysis);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //dlg.FileName = "DyKnow"; // Default file name
            dlg.DefaultExt = ".dyz"; // Default file extension
            dlg.Filter = "DyKnow Files (.dyz)|*.dyz"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                textBoxSaveFileName.Text = dlg.FileName;
                displayFile(dlg.FileName);
            }
        }


        // Import a file into the database
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dr == null)
            {
                MessageBox.Show("Please open a DyKnow file", "Error");
            }
            else if (!c.isDatabaseOpen())
            {
                MessageBox.Show("Please open a database file.", "Error");
            }
            else if (comboBoxDates.SelectedValue == null)
            {
                MessageBox.Show("Please select a date.", "Error");
            }
            else
            {
                int i = c.addDyKnowFile(dr, System.IO.Path.GetFileNameWithoutExtension(textBoxSaveFileName.Text), 
                    comboBoxDates.SelectedValue as Classdate);
                if (i < 0)
                {
                    MessageBox.Show("Import failed to create file record in the database.", "Error");
                }
                else
                {
                    clearDyKnowGrid();
                    textBoxSaveFileName.Clear();
                    buttonImport.IsEnabled = false;
                    c.refreshStudents();
                    c.refreshStudents();
                }
            }
        }

        private void buttonAddDate_Click(object sender, RoutedEventArgs e)
        {
            if (!c.isDatabaseOpen())
            {
                MessageBox.Show("Error: No database is open.");
            }
            else
            {
                AddNewDate popupWindow = new AddNewDate();
                //popupWindow.Owner = this.Pare;
                popupWindow.ShowDialog();
            }
        }

    }
}
