// <copyright file="RosterImporter.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Window for importing a CSV roster file into the database.</summary>
namespace DPXManager
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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
    using DPXDatabase;

    /// <summary>
    /// Window for importing CSV roster file into the database.
    /// </summary>
    public partial class RosterImporter : Window
    {
        /// <summary>
        /// Creates an instance of the singleton controller.
        /// </summary>
        private Controller controller = Controller.Instance();

        /// <summary>
        /// The parsing engine that is part of the model.
        /// </summary>
        private RosterFile rosterFile;

        /// <summary>
        /// Flag that indicates if the file was successfully opened.
        /// </summary>
        private bool isOpened;

        /// <summary>
        /// The column selection boxes that allow the user to choose what each column represents.
        /// </summary>
        private ComboBox[] columns;

        /// <summary>
        /// The first row of data that allows the user to choose if it represents column headers.
        /// </summary>
        private TextBlock[] headers;

        /// <summary>
        /// Initializes a new instance of the RosterImporter class.
        /// </summary>
        public RosterImporter()
        {
            InitializeComponent();

            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "Roster File (.csv)|*.csv"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                this.rosterFile = new RosterFile(dlg.FileName);
                if (this.rosterFile.Count == 0)
                {
                    MessageBox.Show("Unable to import roster.  Check if file is still in use.");
                }

                this.isOpened = true;
                this.myGrid.Background = Brushes.White;
                this.FillGrid();
            }
            else
            {
                this.isOpened = false;
                this.Close();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the file was opened.
        /// </summary>
        /// <value>True if the file was able to be opened.</value>
        public bool IsOpened
        {
            get { return this.isOpened; }
        }

        /// <summary>
        /// Fills the grid on the screen with the information contained in the CSV file.
        /// </summary>
        private void FillGrid()
        {
            // Add all of the necessary columns
            for (int i = 0; i < this.rosterFile.NumColumns; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                myGrid.ColumnDefinitions.Add(cd);
            }

            // Add all of the necessary rows
            for (int i = 0; i < this.rosterFile.Count + 1; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(20);
                myGrid.RowDefinitions.Add(rd);
            }

            // Set up combo boxes
            this.columns = new ComboBox[this.rosterFile.NumColumns];

            // Add the selection menus
            for (int i = 0; i < this.rosterFile.NumColumns; i++)
            {
                ComboBox cbox = new ComboBox();
                this.columns[i] = cbox;
                cbox.Background = Brushes.LightBlue;
                ComboBoxItem cboxitem = new ComboBoxItem();
                cboxitem.Content = "First Name";
                cbox.Items.Add(cboxitem);
                ComboBoxItem cboxitem2 = new ComboBoxItem();
                cboxitem2.Content = "Last Name";
                cbox.Items.Add(cboxitem2);
                ComboBoxItem cboxitem3 = new ComboBoxItem();
                cboxitem3.Content = "User name";
                cbox.Items.Add(cboxitem3);
                ComboBoxItem cboxitem4 = new ComboBoxItem();
                cboxitem4.Content = "Section";
                cbox.Items.Add(cboxitem4);
                Grid.SetRow(cbox, 0);
                Grid.SetColumn(cbox, i);
                myGrid.Children.Add(cbox);
            }

            // Add all of the data into the grid
            for (int i = 0; i < this.rosterFile.Count; i++)
            {
                string[] cols = this.rosterFile.ParsedData[i];
                if (i == 0)
                {
                    this.headers = new TextBlock[cols.Length];
                }

                for (int j = 0; j < cols.Length; j++)
                {
                    TextBlock cell = new TextBlock();
                    cell.Text = cols[j];
                    cell.FontSize = 11;
                    cell.VerticalAlignment = VerticalAlignment.Top;
                    Grid.SetRow(cell, i + 1);
                    Grid.SetColumn(cell, j);
                    myGrid.Children.Add(cell);
                    if (i == 0)
                    {
                        this.headers[j] = cell;
                    }
                }
            }

            // Update the number of students
            this.labelNumberOfStudents.Content = "Number of Students: " + this.rosterFile.Count.ToString();
        }

        /// <summary>
        /// The first row is a heading.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CheckBoxHeaders_Checked(object sender, RoutedEventArgs e)
        {
            this.labelNumberOfStudents.Content = "Number of Students: " + (this.rosterFile.Count - 1).ToString();
            if (this.rosterFile.Count > 0)
            {
                for (int i = 0; i < this.headers.Length; i++)
                {
                    this.headers[i].Background = Brushes.LightGray;
                }
            }
        }

        /// <summary>
        /// The first row is not a heading.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CheckBoxHeaders_Unchecked(object sender, RoutedEventArgs e)
        {
            this.labelNumberOfStudents.Content = "Number of Students: " + this.rosterFile.Count.ToString();
            if (this.rosterFile.Count > 0)
            {
                for (int i = 0; i < this.headers.Length; i++)
                {
                    this.headers[i].Background = Brushes.White;
                }
            }
        }

        /// <summary>
        /// If the columns are properly selected, add the students and close the window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonImport_Click(object sender, RoutedEventArgs e)
        {
            int start = 0;
            if (this.checkBoxHeaders.IsChecked.Value && this.rosterFile.Count > 0)
            {
                start++;
            }

            if (this.ValidateColumnHeaders())
            {
                for (int i = start; i < this.rosterFile.Count; i++)
                {
                    string[] cols = this.rosterFile.ParsedData[i];
                    string firstname = string.Empty;
                    string lastname = string.Empty;
                    string username = string.Empty;
                    string section = string.Empty;

                    for (int j = 0; j < cols.Length; j++)
                    {
                        if (this.columns[j].SelectedIndex == 0)
                        {
                            firstname = cols[j];
                        }
                        else if (this.columns[j].SelectedIndex == 1)
                        {
                            lastname = cols[j];
                        }
                        else if (this.columns[j].SelectedIndex == 2)
                        {
                            username = cols[j];
                        }
                        else if (this.columns[j].SelectedIndex == 3)
                        {
                            section = cols[j];
                        }
                    }

                    // Add the student to the roster, the controller will take care of the details
                    this.controller.AddStudentToRoster(firstname, lastname, username, section);
                }

                this.controller.RefreshStudents();
                this.controller.RefreshSections();
                this.Close();
            }
        }

        /// <summary>
        /// Performs a check to make sure each of the headers was selected only once.
        /// </summary>
        /// <returns>True if a valid column header configuration is selected.</returns>
        private bool ValidateColumnHeaders()
        {
            int[] test = new int[4];
            for (int i = 0; i < test.Length; i++)
            {
                test[i] = 0;
            }

            for (int i = 0; i < this.columns.Length; i++)
            {
                if (this.columns[i].SelectedIndex >= 0)
                {
                    test[this.columns[i].SelectedIndex]++;
                }
            }

            for (int i = 0; i < test.Length; i++)
            {
                if (test[i] != 1)
                {
                    MessageBox.Show("Invalid column selection.");
                    return false;
                }
            }

            return true;
        }
    }
}
