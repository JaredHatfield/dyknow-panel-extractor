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
    /// Interaction logic for AboutDPX.xaml
    /// </summary>
    public partial class AboutDPX : Window
    {
        public AboutDPX()
        {
            InitializeComponent();
            buttonOk.Focus();
        }

        private void button_ok(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
    }
}
