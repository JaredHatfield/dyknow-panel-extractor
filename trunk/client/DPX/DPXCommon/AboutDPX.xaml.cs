// <copyright file="AboutDPX.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The about popup window for the application.</summary>
namespace DPXCommon
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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

    /// <summary>
    /// The about popup window for the application.
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
            this.AddHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(this.HyperlinkNavigate));
            this.VersionNumber.Text = "Version " + System.Reflection.Assembly.GetCallingAssembly().GetName().Version.ToString();
            this.ApplicationName.Text = System.Reflection.Assembly.GetCallingAssembly().GetName().Name;
        }

        /// <summary>
        /// Load the clicked hyperlink in a new web broswer window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void HyperlinkNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        /// <summary>
        /// Close the window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void Button_ok(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
