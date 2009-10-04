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
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (c.isDatabaseOpen())
            {
                List<Student> allStudents = c.DB.getAllStudents();
                listBoxStudents.Items.Clear();
                for (int i = 0; i < allStudents.Count; i++)
                {
                    listBoxStudents.Items.Add(allStudents[i]);
                }
            }
        }


        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterText(textBoxFilter.Text);
        }

        public void FilterText(string searchText)
        {
            listBoxStudents.Items.Filter = delegate(object obj)
            {
                Student s = (Student)obj;
                if (s == null) return false;
                else if (s.FirstName.ToLower().IndexOf(searchText.ToLower(), 0) > -1) return true;
                else if (s.LastName.ToLower().IndexOf(searchText.ToLower(), 0) > -1) return true;
                else if (s.Username.ToLower().IndexOf(searchText.ToLower(), 0) > -1) return true;
                else return false;
            };
        }
    }
}
