// <copyright file="UserInformation.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The user information window.</summary>
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
    /// The user information window.
    /// </summary>
    public partial class UserInformation : Window
    {
        /// <summary>
        /// Initializes a new instance of the UserInformation class.
        /// </summary>
        public UserInformation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the UserInformation class.
        /// </summary>
        /// <param name="content">The information to be displayed.</param>
        public UserInformation(FlowDocument content)
        {
            InitializeComponent();
            textBoxContent.Document = content;
        }
    }
}
