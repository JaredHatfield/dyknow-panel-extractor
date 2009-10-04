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
using System.Data.OleDb;


namespace DPX
{
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
                textBoxStudentFirstName.Text = s.FirstName;
                textBoxStudentFullName.Text = s.FullName;
                textBoxStudentLastName.Text = s.LastName;
                textBoxStudentUserName.Text = s.Username;
                //textBoxStudentSection;
                //textBoxStudentIsEnrolled;
            }
            else
            {
                textBoxStudentFirstName.Text = "";
                textBoxStudentFullName.Text = "";
                textBoxStudentLastName.Text = "";
                textBoxStudentUserName.Text = "";
            }
        }


    }
}
