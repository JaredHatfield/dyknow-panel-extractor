// <copyright file="AboutDPX.xaml.cs" company="DPX on Google Code">
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
    /// Interaction logic for AboutDPX.xaml
    /// </summary>
    public partial class AboutDPX : Window
    {
        /// <summary>
        /// Initializes a new instance of the AboutDPX class.
        /// </summary>
        public AboutDPX()
        {
            InitializeComponent();
            buttonOk.Focus();
        }

        /// <summary>
        /// Close the window.
        /// </summary>
        /// <param name="sender">Who called this function.</param>
        /// <param name="e">The parameters for this call.</param>
        private void Button_ok(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
