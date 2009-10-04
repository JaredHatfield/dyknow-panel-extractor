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
using System.Windows.Shapes;

namespace DPX
{
    /// <summary>
    /// Interaction logic for AddNewDate.xaml
    /// </summary>
    public partial class AddNewDate : Window
    {
        Controller c = Controller.Instance();

        public AddNewDate()
        {
            InitializeComponent();

            if (c.DB.isClassdate(dateTimePicker.Value.Date))
            {
                buttonAddDate.IsEnabled = false;
            }
            else
            {
                buttonAddDate.IsEnabled = true;
            }

        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (c.DB.isClassdate(dateTimePicker.Value.Date))
            {
                buttonAddDate.IsEnabled = false;
            }
            else
            {
                buttonAddDate.IsEnabled = true;
            }
        }

        private void buttonAddDate_Click(object sender, RoutedEventArgs e)
        {
            c.DB.addClassdate(dateTimePicker.Value.Date);
            c.refreshClassdate();
            this.Close();
        }
    }
}
