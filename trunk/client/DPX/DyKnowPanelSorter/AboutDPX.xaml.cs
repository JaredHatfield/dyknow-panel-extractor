﻿// <copyright file="AboutDPX.xaml.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace DyKnowPanelSorter
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
        public AboutDPX()
        {
            InitializeComponent();
            buttonOk.Focus();
        }

        private void Button_ok(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
