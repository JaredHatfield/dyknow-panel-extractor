// <copyright file="AddNewDate.xaml.cs" company="DPX on Google Code">
// GNU General Public License v3
// </copyright>
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
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for AddNewDate.xaml
    /// </summary>
    public partial class AddNewDate : Window
    {
        /// <summary>
        /// An instance of the controller.
        /// </summary>
        private Controller c = Controller.Instance();

        /// <summary>
        /// Initializes a new instance of the AddNewDate class.
        /// </summary>
        public AddNewDate()
        {
            InitializeComponent();

            if (this.c.DB.IsClassdate(dateTimePicker.Value.Date))
            {
                buttonAddDate.IsEnabled = false;
            }
            else
            {
                buttonAddDate.IsEnabled = true;
            }
        }

        /// <summary>
        /// The selected date changed and the add button will toggel depending on if it is a valid date.
        /// </summary>
        /// <param name="sender">The object that triggered this method.</param>
        /// <param name="e">The parameters sent to this method.</param>
        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (this.c.DB.IsClassdate(dateTimePicker.Value.Date))
            {
                buttonAddDate.IsEnabled = false;
            }
            else
            {
                buttonAddDate.IsEnabled = true;
            }
        }

        /// <summary>
        /// Add the selected date to the list of dates.
        /// </summary>
        /// <param name="sender">The object that triggered this method.</param>
        /// <param name="e">The paremeters sent to this method.</param>
        private void ButtonAddDate_Click(object sender, RoutedEventArgs e)
        {
            this.c.DB.AddClassdate(dateTimePicker.Value.Date);
            this.c.refreshClassdate();
            this.Close();
        }
    }
}
