﻿// <copyright file="UserInformation.xaml.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace Preview
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
    /// Interaction logic for UserInformation.xaml
    /// </summary>
    public partial class UserInformation : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public UserInformation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        public UserInformation(FlowDocument content)
        {
            InitializeComponent();
            textBoxContent.Document = content;
        }
    }
}