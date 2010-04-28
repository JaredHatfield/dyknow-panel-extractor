// <copyright file="ImportFile.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The tab that displays the interface for importing a DyKnow file.</summary>
namespace DPX
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using DPXDatabase;
    using QuickReader;

    /// <summary>
    /// The tab that displays the interface for importing a DyKnow file.
    /// </summary>
    public partial class ImportFile : Page
    {   
        /// <summary>
        /// Creates an instance of the singleton controller.
        /// </summary>
        private Controller c = Controller.Instance();

        /// <summary>
        /// DyKnowReader used to read the contents of the specified file.
        /// </summary>
        private DyKnowReader dr;

        /// <summary>
        /// Initializes a new instance of the ImportFile class.
        /// </summary>
        public ImportFile()
        {
            InitializeComponent();
            textBoxSaveFileName.IsReadOnly = true;
            grid1.Background = Brushes.White;
            buttonImport.IsEnabled = false;

            this.c.SetComboBoxDateImport(comboBoxDates);
        }

        /// <summary>
        /// Clears the grid of all information that was previously loaded.
        /// </summary>
        public void ClearDyKnowGrid()
        {
            grid1.Children.Clear();
            grid1.RowDefinitions.Clear();
        }

        /// <summary>
        /// Loads the preview for the panel which was specified by this specific button.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ShowPreview(object sender, System.EventArgs e)
        {
            Button b = sender as Button;
            int i = (int)b.Tag;
            PanelPreview pp = new PanelPreview(this.dr);
            pp.DisplayPanel(i);
            pp.ShowDialog();
        }

        /// <summary>
        /// Loads the file using the DyKnow reader and displays its contents in the grid.
        /// </summary>
        /// <param name="file">The path to the file to oepn.</param>
        private void DisplayFile(string file)
        {
            this.dr = new DyKnowReader(file);
            this.ClearDyKnowGrid();
            buttonImport.IsEnabled = true;
            for (int i = 0; i < this.dr.NumOfPages(); i++)
            {
                DyKnowPage dp = this.dr.GetDyKnowPage(i);

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
                if (dp.Finished.Equals("Yes"))
                {
                    analysis.Background = Brushes.LightGreen;
                }
                else if (dp.Finished.Equals("Maybe"))
                {
                    analysis.Background = Brushes.LightYellow;
                }
                else if (dp.Finished.Equals("No"))
                {
                    analysis.Background = Brushes.LightPink;
                }

                Grid.SetColumn(analysis, 4);
                Grid.SetRow(analysis, i);
                analysis.BorderThickness = new Thickness(1);
                analysis.BorderBrush = Brushes.Black;
                grid1.Children.Add(analysis);

                Button buttonNum = new Button();
                buttonNum.Content = "Show";
                Grid.SetColumn(buttonNum, 5);
                Grid.SetRow(buttonNum, i);
                buttonNum.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.ShowPreview));
                buttonNum.Tag = i;
                buttonNum.BorderBrush = Brushes.Black;
                buttonNum.BorderThickness = new Thickness(1);
                grid1.Children.Add(buttonNum);
            }
        }

        /// <summary>
        /// Open a DyKnow file to be read into the application.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
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
                textBoxSaveFileName.Text = dlg.FileName;
                this.DisplayFile(dlg.FileName);
            }
        }

        /// <summary>
        ///  Import a file into the database.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.dr == null)
            {
                MessageBox.Show("Please open a DyKnow file", "Error");
            }
            else if (!this.c.IsDatabaseOpen())
            {
                MessageBox.Show("Please open a database file.", "Error");
            }
            else if (comboBoxDates.SelectedValue == null)
            {
                MessageBox.Show("Please select a date.", "Error");
            }
            else
            {
                int i = this.c.AddDyKnowFile(
                    this.dr, 
                    System.IO.Path.GetFileNameWithoutExtension(textBoxSaveFileName.Text), 
                    comboBoxDates.SelectedValue as Classdate);
                if (i < 0)
                {
                    MessageBox.Show("Import failed to create file record in the database.", "Error");
                }
                else
                {
                    this.ClearDyKnowGrid();
                    textBoxSaveFileName.Clear();
                    buttonImport.IsEnabled = false;
                    this.c.RefreshStudents();
                    this.c.RefreshStudents();
                }
            }
        }

        /// <summary>
        /// Provides the window to add a new date to the database.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonAddDate_Click(object sender, RoutedEventArgs e)
        {
            if (!this.c.IsDatabaseOpen())
            {
                MessageBox.Show("Error: No database is open.");
            }
            else
            {
                AddNewDate popupWindow = new AddNewDate();
                popupWindow.ShowDialog();
            }
        }
    }
}
