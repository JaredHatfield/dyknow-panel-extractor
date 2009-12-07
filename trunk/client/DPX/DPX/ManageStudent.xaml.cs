// <copyright file="ManageStudent.xaml.cs" company="DPX on Google Code">
// GNU General Public License v3
// </copyright>
namespace DPX
{
    using DPXDatabase;
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
    using System.Data.OleDb;

    /// <summary>
    /// Interaction logic for ManageStudent.xaml
    /// </summary>
    public partial class ManageStudent : Page
    {
        Controller c = Controller.Instance();

        public ManageStudent()
        {
            InitializeComponent();
            c.setListBoxStudents(listBoxStudents);
            c.setComboBoxSections(comboBoxSection);
            c.setComboBoxDateException(comboBoxDate);
            c.setComboBoxReason(comboBoxReason);
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterText(textBoxFilter.Text, checkBoxFilterNoSection.IsChecked == true);
        }

        private void checkBoxFilterNoSection_Checked(object sender, RoutedEventArgs e)
        {
            FilterText(textBoxFilter.Text, checkBoxFilterNoSection.IsChecked == true);
        }

        private void checkBoxFilterNoSection_Unchecked(object sender, RoutedEventArgs e)
        {
            FilterText(textBoxFilter.Text, checkBoxFilterNoSection.IsChecked == true);
        }

        public void FilterText(string searchText, bool sectionless)
        {
            listBoxStudents.Items.Filter = delegate(object obj)
            {
                Student s = (Student)obj;
                if (s == null) return false;
                else if (sectionless && s.IsInSection) return false;
                else if (s.FirstName.ToLower().IndexOf(searchText.ToLower(), 0) > -1) return true;
                else if (s.LastName.ToLower().IndexOf(searchText.ToLower(), 0) > -1) return true;
                else if (s.Username.ToLower().IndexOf(searchText.ToLower(), 0) > -1) return true;
                else return false;
            };
        }


        private void listBoxStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Student s = listBoxStudents.SelectedValue as Student;
            if (s != null)
            {
                //Display the student information
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

                //Display the panel information
                displayExceptions(s.Id);
                displayPanels(s.Id);
            }
            else
            {
                textBoxStudentFirstName.Text = "";
                textBoxStudentFullName.Text = "";
                textBoxStudentLastName.Text = "";
                textBoxStudentUserName.Text = "";
                comboBoxSection.SelectedIndex = 0;
                checkBoxStudentIsEnrolled.IsChecked = false;

                gridException.Children.Clear();
                gridException.RowDefinitions.Clear();
                gridPanels.Children.Clear();
                gridPanels.RowDefinitions.Clear();
            }
        }


        private void displayExceptions(int studentId)
        {
            gridException.Children.Clear();
            gridException.RowDefinitions.Clear();

            // Display the exceptions for this user
            List<DisplayExceptionInfo> dei = c.DB.GetExceptionsForStudent(studentId);
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
        private void displayPanels(int studentId)
        {
            gridPanels.Children.Clear();
            gridPanels.RowDefinitions.Clear();

            // Display the panels for this user
            List<DisplayPanelInfo> dpi = c.DB.GetPanelsForStudent(studentId);
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


        private void comboBoxSection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Student s = listBoxStudents.SelectedValue as Student;
            if (s != null && c.isDatabaseOpen())
            {
                DPXDatabase.Section section = comboBoxSection.SelectedValue as DPXDatabase.Section;
                c.DB.UpdateStudentSection(section.Id, s.Id);
                s.Section = section.Id;
            }
        }

        private void checkBoxStudentIsEnrolled_Checked(object sender, RoutedEventArgs e)
        {
            Student s = listBoxStudents.SelectedValue as Student;
            if (s != null && c.isDatabaseOpen())
            {
                c.DB.UpdateStudentSetEnrolled(true, s.Id);
                s.IsEnrolled = true;
            }
        }

        private void checkBoxStudentIsEnrolled_Unchecked(object sender, RoutedEventArgs e)
        {
            Student s = listBoxStudents.SelectedValue as Student;
            if (s != null && c.isDatabaseOpen())
            {
                c.DB.UpdateStudentSetEnrolled(false, s.Id);
                s.IsEnrolled = false;
            }
        }

        private void buttonAddException_Click(object sender, RoutedEventArgs e)
        {
            Student s = listBoxStudents.SelectedValue as Student;
            Classdate d = comboBoxDate.SelectedValue as Classdate;
            Reason r = comboBoxReason.SelectedValue as Reason;
            if (!c.isDatabaseOpen())
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
                c.DB.AddException(ex);
                //comboBoxDate.SelectedIndex = -1;
                comboBoxReason.SelectedIndex = -1;
                textBoxNotes.Text = "";
            }

            displayExceptions(s.Id);
        }


    }
}
