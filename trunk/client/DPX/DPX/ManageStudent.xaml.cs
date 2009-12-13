// <copyright file="ManageStudent.xaml.cs" company="DPX">
// GNU General Public License v3
// </copyright>
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
    /// Interaction logic for ManageStudent.xaml
    /// </summary>
    public partial class ManageStudent : Page
    {
        /// <summary>
        /// 
        /// </summary>
        private Controller c = Controller.Instance();

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="sectionless"></param>
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
        /// 
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.FilterText(textBoxFilter.Text, checkBoxFilterNoSection.IsChecked == true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CheckBoxFilterNoSection_Checked(object sender, RoutedEventArgs e)
        {
            this.FilterText(textBoxFilter.Text, checkBoxFilterNoSection.IsChecked == true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CheckBoxFilterNoSection_Unchecked(object sender, RoutedEventArgs e)
        {
            this.FilterText(textBoxFilter.Text, checkBoxFilterNoSection.IsChecked == true);
        }
        
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="studentId"></param>
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

                Label date = new Label();
                date.Content = d.Date.Date.ToShortDateString();
                Grid.SetColumn(date, 0);
                Grid.SetRow(date, i);
                date.BorderThickness = new Thickness(1);
                date.BorderBrush = Brushes.Black;
                gridException.Children.Add(date);

                Label credit = new Label();
                credit.Content = d.Credit;
                Grid.SetColumn(credit, 1);
                Grid.SetRow(credit, i);
                credit.BorderThickness = new Thickness(1);
                credit.BorderBrush = Brushes.Black;
                gridException.Children.Add(credit);

                Label description = new Label();
                description.Content = d.Description;
                Grid.SetColumn(description, 2);
                Grid.SetRow(description, i);
                description.BorderThickness = new Thickness(1);
                description.BorderBrush = Brushes.Black;
                gridException.Children.Add(description);

                Label notes = new Label();
                notes.Content = d.Notes;
                Grid.SetColumn(notes, 3);
                Grid.SetRow(notes, i);
                notes.BorderThickness = new Thickness(1);
                notes.BorderBrush = Brushes.Black;
                gridException.Children.Add(notes);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
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

                Label date = new Label();
                date.Content = d.Date.Date.ToShortDateString();
                Grid.SetColumn(date, 0);
                Grid.SetRow(date, i);
                date.BorderThickness = new Thickness(1);
                date.BorderBrush = Brushes.Black;
                gridPanels.Children.Add(date);

                Label file = new Label();
                file.Content = d.Filename;
                Grid.SetColumn(file, 1);
                Grid.SetRow(file, i);
                file.BorderThickness = new Thickness(1);
                file.BorderBrush = Brushes.Black;
                gridPanels.Children.Add(file);

                Label slidenum = new Label();
                slidenum.Content = d.SlideNumber;
                Grid.SetColumn(slidenum, 2);
                Grid.SetRow(slidenum, i);
                slidenum.BorderThickness = new Thickness(1);
                slidenum.BorderBrush = Brushes.Black;
                gridPanels.Children.Add(slidenum);

                Label tstrokecount = new Label();
                tstrokecount.Content = d.TotalStrokeCount;
                Grid.SetColumn(tstrokecount, 3);
                Grid.SetRow(tstrokecount, i);
                tstrokecount.BorderThickness = new Thickness(1);
                tstrokecount.BorderBrush = Brushes.Black;
                gridPanels.Children.Add(tstrokecount);

                Label nstrokecount = new Label();
                nstrokecount.Content = d.NetStrokeCount;
                Grid.SetColumn(nstrokecount, 4);
                Grid.SetRow(nstrokecount, i);
                nstrokecount.BorderThickness = new Thickness(1);
                nstrokecount.BorderBrush = Brushes.Black;
                gridPanels.Children.Add(nstrokecount);

                Label isblank = new Label();
                isblank.Content = d.IsBlank.ToString();
                Grid.SetColumn(isblank, 5);
                Grid.SetRow(isblank, i);
                isblank.BorderThickness = new Thickness(1);
                isblank.BorderBrush = Brushes.Black;
                gridPanels.Children.Add(isblank);

                Label analysis = new Label();
                analysis.Content = d.Analysis;
                Grid.SetColumn(analysis, 6);
                Grid.SetRow(analysis, i);
                analysis.BorderThickness = new Thickness(1);
                analysis.BorderBrush = Brushes.Black;
                gridPanels.Children.Add(analysis);
            }
        }

        /// <summary>
        /// 
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
        /// 
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
        /// 
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
        /// 
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
                comboBoxReason.SelectedIndex = -1;
                textBoxNotes.Text = string.Empty;
            }

            this.DisplayExceptions(s.Id);
        }
    }
}
