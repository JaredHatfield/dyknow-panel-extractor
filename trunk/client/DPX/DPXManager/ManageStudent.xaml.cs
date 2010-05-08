// <copyright file="ManageStudent.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The tab that displays the interface for managing students.</summary>
namespace DPX
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
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

    /// <summary>
    /// The tab that displays the interface for managing students.
    /// </summary>
    public partial class ManageStudent : Page
    {
        /// <summary>
        /// Creates an instance of the singleton controller.
        /// </summary>
        private Controller c = Controller.Instance();

        /// <summary>
        /// Initializes a new instance of the ManageStudent class.
        /// </summary>
        public ManageStudent()
        {
            InitializeComponent();
            this.c.SetListBoxStudents(listBoxStudents);
            this.c.SetComboBoxSections(comboBoxSection);
            this.c.SetComboBoxDateException(comboBoxDate);
            this.c.SetComboBoxReason(comboBoxReason);
        }

        /// <summary>
        /// Filter the list of students based on input.
        /// </summary>
        /// <param name="searchText">The string to search based on first name, last name, or username.</param>
        /// <param name="sectionless">Only display students who are not in a section.</param>
        public void FilterText(string searchText, bool sectionless)
        {
            listBoxStudents.Items.Filter = delegate(object obj)
            {
                Student s = (Student)obj;
                if (s == null)
                {
                    return false;
                }
                else if (sectionless && s.IsInSection)
                {
                    return false;
                }
                else if (s.FirstName.ToLower().IndexOf(searchText.ToLower(), 0) > -1)
                {
                    return true;
                }
                else if (s.LastName.ToLower().IndexOf(searchText.ToLower(), 0) > -1)
                {
                    return true;
                }
                else if (s.Username.ToLower().IndexOf(searchText.ToLower(), 0) > -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }

        /// <summary>
        /// The text box for searching changed.  The filter needs to be updated.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.FilterText(textBoxFilter.Text, checkBoxFilterNoSection.IsChecked == true);
        }

        /// <summary>
        /// The check box for filtering changed.  The filter needs to be updated.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CheckBoxFilterNoSection_Checked(object sender, RoutedEventArgs e)
        {
            this.FilterText(textBoxFilter.Text, checkBoxFilterNoSection.IsChecked == true);
        }

        /// <summary>
        /// The check box for filtering changed.  The filter needs to be updated.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CheckBoxFilterNoSection_Unchecked(object sender, RoutedEventArgs e)
        {
            this.FilterText(textBoxFilter.Text, checkBoxFilterNoSection.IsChecked == true);
        }
        
        /// <summary>
        /// The selected student changed so the display information needs to be updated.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ListBoxStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Student s = listBoxStudents.SelectedValue as Student;
            if (s != null)
            {
                // Display the student information
                textBoxStudentFirstName.Text = s.FirstName;
                textBoxStudentFullName.Text = s.FullName;
                textBoxStudentLastName.Text = s.LastName;
                textBoxStudentUserName.Text = s.Username;
                if (s.Section == -1)
                {
                    comboBoxSection.SelectedIndex = 0;
                }
                else
                {
                    for (int i = 0; i < comboBoxSection.Items.Count; i++)
                    {
                        DPXDatabase.Section section = comboBoxSection.Items[i] as DPXDatabase.Section;
                        if (section.Id == s.Section)
                        {
                            comboBoxSection.SelectedIndex = i;
                            break;
                        }
                    }
                }

                if (s.IsEnrolled)
                {
                    checkBoxStudentIsEnrolled.IsChecked = true;
                }
                else
                {
                    checkBoxStudentIsEnrolled.IsChecked = false;
                }

                // Display the panel information
                this.DisplayExceptions(s.Id);
                this.DisplayPanels(s.Id);
            }
            else
            {
                textBoxStudentFirstName.Text = string.Empty;
                textBoxStudentFullName.Text = string.Empty;
                textBoxStudentLastName.Text = string.Empty;
                textBoxStudentUserName.Text = string.Empty;
                comboBoxSection.SelectedIndex = 0;
                checkBoxStudentIsEnrolled.IsChecked = false;

                gridException.Children.Clear();
                gridException.RowDefinitions.Clear();
                gridPanels.Children.Clear();
                gridPanels.RowDefinitions.Clear();
            }
        }

        /// <summary>
        /// Display all of the exceptions for a specific student.
        /// </summary>
        /// <param name="studentId">The ID for the student.</param>
        private void DisplayExceptions(int studentId)
        {
            gridException.Children.Clear();
            gridException.RowDefinitions.Clear();

            // Display the exceptions for this user
            List<DisplayExceptionInfo> dei = this.c.DB.GetExceptionsForStudent(studentId);
            for (int i = 0; i < dei.Count; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(30);
                gridException.RowDefinitions.Add(rd);

                DisplayExceptionInfo d = dei[i];

                // Determine if the exception is for today
                bool today = false;
                if (d.Date.Date.Equals(DateTime.Today.Date))
                {
                    today = true;
                }

                // Date column
                Label date = new Label();
                date.Content = d.Date.Date.ToShortDateString();
                if (today)
                {
                    date.Background = Brushes.LightBlue;
                }

                Grid.SetColumn(date, 0);
                Grid.SetRow(date, i);
                date.BorderThickness = new Thickness(1);
                date.BorderBrush = Brushes.Black;
                gridException.Children.Add(date);

                // Credit column
                Label credit = new Label();
                credit.Content = d.Credit;
                if (d.Credit)
                {
                    credit.Background = Brushes.LightGreen;
                }
                else
                {
                    credit.Background = Brushes.LightPink;
                }

                Grid.SetColumn(credit, 1);
                Grid.SetRow(credit, i);
                credit.BorderThickness = new Thickness(1);
                credit.BorderBrush = Brushes.Black;
                gridException.Children.Add(credit);

                // Description column
                Label description = new Label();
                description.Content = d.Description;
                if (today)
                {
                    description.Background = Brushes.LightBlue;
                }

                Grid.SetColumn(description, 2);
                Grid.SetRow(description, i);
                description.BorderThickness = new Thickness(1);
                description.BorderBrush = Brushes.Black;
                gridException.Children.Add(description);

                // Notes column
                Label notes = new Label();
                notes.Content = d.Notes;
                if (today)
                {
                    notes.Background = Brushes.LightBlue;
                }

                Grid.SetColumn(notes, 3);
                Grid.SetRow(notes, i);
                notes.BorderThickness = new Thickness(1);
                notes.BorderBrush = Brushes.Black;
                gridException.Children.Add(notes);
            }
        }

        /// <summary>
        /// Display all of the panel information for a specific student.
        /// </summary>
        /// <param name="studentId">The ID for the student.</param>
        private void DisplayPanels(int studentId)
        {
            gridPanels.Children.Clear();
            gridPanels.RowDefinitions.Clear();

            // Display the panels for this user
            List<DisplayPanelInfo> dpi = this.c.DB.GetPanelsForStudent(studentId);
            for (int i = 0; i < dpi.Count; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(30);
                gridPanels.RowDefinitions.Add(rd);

                DisplayPanelInfo d = dpi[i];

                // Determine if the panel is for today
                bool today = false;
                if (d.Date.Date.Equals(DateTime.Today.Date))
                {
                    today = true;
                }

                // Date column
                Label date = new Label();
                date.Content = d.Date.Date.ToShortDateString();
                if (today)
                {
                    date.Background = Brushes.LightBlue;
                }

                Grid.SetColumn(date, 0);
                Grid.SetRow(date, i);
                date.BorderThickness = new Thickness(1);
                date.BorderBrush = Brushes.Black;
                gridPanels.Children.Add(date);

                // File column
                Label file = new Label();
                file.Content = d.Filename;
                if (today)
                {
                    file.Background = Brushes.LightBlue;
                }

                Grid.SetColumn(file, 1);
                Grid.SetRow(file, i);
                file.BorderThickness = new Thickness(1);
                file.BorderBrush = Brushes.Black;
                gridPanels.Children.Add(file);

                // Slide number column
                Label slidenum = new Label();
                slidenum.Content = d.SlideNumber;
                if (today)
                {
                    slidenum.Background = Brushes.LightBlue;
                }

                Grid.SetColumn(slidenum, 2);
                Grid.SetRow(slidenum, i);
                slidenum.BorderThickness = new Thickness(1);
                slidenum.BorderBrush = Brushes.Black;
                gridPanels.Children.Add(slidenum);

                // Total stroke count column
                Label tstrokecount = new Label();
                tstrokecount.Content = d.TotalStrokeCount;
                if (today)
                {
                    tstrokecount.Background = Brushes.LightBlue;
                }

                Grid.SetColumn(tstrokecount, 3);
                Grid.SetRow(tstrokecount, i);
                tstrokecount.BorderThickness = new Thickness(1);
                tstrokecount.BorderBrush = Brushes.Black;
                gridPanels.Children.Add(tstrokecount);

                // Net stroke count column
                Label nstrokecount = new Label();
                nstrokecount.Content = d.NetStrokeCount;
                if (today)
                {
                    nstrokecount.Background = Brushes.LightBlue;
                }

                Grid.SetColumn(nstrokecount, 4);
                Grid.SetRow(nstrokecount, i);
                nstrokecount.BorderThickness = new Thickness(1);
                nstrokecount.BorderBrush = Brushes.Black;
                gridPanels.Children.Add(nstrokecount);

                // Is blank column
                Label isblank = new Label();
                isblank.Content = d.IsBlank.ToString();
                if (today)
                {
                    isblank.Background = Brushes.LightBlue;
                }

                Grid.SetColumn(isblank, 5);
                Grid.SetRow(isblank, i);
                isblank.BorderThickness = new Thickness(1);
                isblank.BorderBrush = Brushes.Black;
                gridPanels.Children.Add(isblank);

                // Analysis column
                Label analysis = new Label();
                analysis.Content = d.Analysis;
                if (d.Analysis.Equals("Yes"))
                {
                    analysis.Background = Brushes.LightGreen;
                }
                else if (d.Analysis.Equals("Maybe"))
                {
                    analysis.Background = Brushes.LightYellow;
                }
                else if (d.Analysis.Equals("No"))
                {
                    analysis.Background = Brushes.LightPink;
                }

                Grid.SetColumn(analysis, 6);
                Grid.SetRow(analysis, i);
                analysis.BorderThickness = new Thickness(1);
                analysis.BorderBrush = Brushes.Black;
                gridPanels.Children.Add(analysis);
            }
        }

        /// <summary>
        /// Change the section which a student is enrolled in.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ComboBoxSection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Student s = listBoxStudents.SelectedValue as Student;
            if (s != null && this.c.IsDatabaseOpen())
            {
                DPXDatabase.Section section = comboBoxSection.SelectedValue as DPXDatabase.Section;
                this.c.DB.UpdateStudentSection(section.Id, s.Id);
                s.Section = section.Id;
            }
        }

        /// <summary>
        /// Change a students status so they are enrolled.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CheckBoxStudentIsEnrolled_Checked(object sender, RoutedEventArgs e)
        {
            Student s = listBoxStudents.SelectedValue as Student;
            if (s != null && this.c.IsDatabaseOpen())
            {
                this.c.DB.UpdateStudentSetEnrolled(true, s.Id);
                s.IsEnrolled = true;
            }
        }

        /// <summary>
        /// Change a students status so they are no longer enrolled.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CheckBoxStudentIsEnrolled_Unchecked(object sender, RoutedEventArgs e)
        {
            Student s = listBoxStudents.SelectedValue as Student;
            if (s != null && this.c.IsDatabaseOpen())
            {
                this.c.DB.UpdateStudentSetEnrolled(false, s.Id);
                s.IsEnrolled = false;
            }
        }

        /// <summary>
        /// Add an exception for a student.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonAddException_Click(object sender, RoutedEventArgs e)
        {
            Student s = listBoxStudents.SelectedValue as Student;
            Classdate d = comboBoxDate.SelectedValue as Classdate;
            Reason r = comboBoxReason.SelectedValue as Reason;
            if (!this.c.IsDatabaseOpen())
            {
                MessageBox.Show("No file is open.", "Error");
            }
            else if (s == null)
            {
                MessageBox.Show("Select a student before adding an exception.", "Error");
            }
            else if (d == null)
            {
                MessageBox.Show("Select a date before adding an exception.", "Error");
            }
            else if (r == null)
            {
                MessageBox.Show("Select a reason before adding an exception.", "Error");
            }
            else
            {
                Exceptions ex = new Exceptions(d.Id, s.Id, r.Id, textBoxNotes.Text);
                this.c.DB.AddException(ex);
                textBoxNotes.Text = string.Empty;
            }

            this.DisplayExceptions(s.Id);
        }
    }
}
