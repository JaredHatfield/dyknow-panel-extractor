// <copyright file="DPXGraderWindow.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The window for DPXGrader.</summary>
namespace DPXGrader
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using DPXCommon;
    using DPXGrader.Accuracy;
    using DPXReader.DyKnow;

    /// <summary>
    /// Interaction logic for DPXGraderWindow.xaml
    /// </summary>
    public partial class DPXGraderWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DPXGraderWindow"/> class.
        /// </summary>
        public DPXGraderWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Display the AboutDPX window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void DisplayAboutWindow(object sender, RoutedEventArgs e)
        {
            AboutDPX popupWindow = new AboutDPX();
            popupWindow.Owner = this;
            popupWindow.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the ButtonAccuracy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonAccuracy_Click(object sender, RoutedEventArgs e)
        {
            AccuracyWindow aw = new AccuracyWindow();
            aw.ShowDialog();
        }
    }
}
