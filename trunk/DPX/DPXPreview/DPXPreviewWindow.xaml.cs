// <copyright file="DPXPreviewWindow.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The main window for Preview.</summary>
namespace DPXPreview
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using DPXCommon;
    using DPXReader;
    using DPXReader.DyKnow;

    /// <summary>
    /// The main window for Preview.
    /// </summary>
    public partial class DPXPreviewWindow : Window
    {
        /// <summary>
        /// Instance of DyKnowReader.
        /// </summary>
        private DyKnow dyknow;

        /// <summary>
        /// The current page being displayed.
        /// </summary>
        private int currentPanelNumber = 0;

        /// <summary>
        /// Initializes a new instance of the DPXPreviewWindow class.
        /// </summary>
        public DPXPreviewWindow()
        {
            InitializeComponent();
            this.currentPanelNumber = 0;
        }

        /// <summary>
        /// The delegate used for opening a file.
        /// </summary>
        /// <param name="filename">The name of the file to open.</param>
        private delegate void OpenDyKnowFileDelegate(string filename);

        /// <summary>
        /// The delegate used for displaying a panel.
        /// </summary>
        /// <param name="id">The panel number.</param>
        private delegate void DisplayPanelDelegate(int id);

        /// <summary>
        /// Load a new DyKnow file.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonLoadClick(object sender, RoutedEventArgs e)
        {
            // Let the user choose which file to open
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.Filter = "DyKnow files (*.dyz)|*.dyz";
            if (openFileDialog1.ShowDialog() == true)
            {
                string filename = openFileDialog1.FileName;
                Dispatcher.BeginInvoke(new OpenDyKnowFileDelegate(this.OpenDyKnowFile), DispatcherPriority.Input, filename);
            }
        }

        /// <summary>
        /// Opens the dy know file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        private void OpenDyKnowFile(string filename)
        {
            // Open the DyKnow file
            this.dyknow = DyKnow.DeserializeFromFile(filename);
            this.currentPanelNumber = 0;
            Inky.Strokes.Clear();
            Dispatcher.BeginInvoke(new DisplayPanelDelegate(this.DisplayPanel), DispatcherPriority.Input, this.currentPanelNumber);
        }

        /// <summary>
        /// Display the next slide.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonNextClick(object sender, RoutedEventArgs e)
        {
            if (this.dyknow != null)
            {
                if ((this.currentPanelNumber + 1) < this.dyknow.DATA.Count)
                {
                    Dispatcher.BeginInvoke(new DisplayPanelDelegate(this.DisplayPanel), DispatcherPriority.Input, (this.currentPanelNumber + 1));
                }
            }
        }

        /// <summary>
        /// Display the previous slide.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonPreviousClick(object sender, RoutedEventArgs e)
        {
            if (this.dyknow != null)
            {
                if (this.currentPanelNumber > 0)
                {
                    Dispatcher.BeginInvoke(new DisplayPanelDelegate(this.DisplayPanel), DispatcherPriority.Input, (this.currentPanelNumber - 1));
                }
            }
        }

        /// <summary>
        /// Export the image of the current slide.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonExportImageClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog1.Filter = "JPEG (*.jpg)|*.jpg";
            if (saveFileDialog1.ShowDialog() == true)
            {
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                RenderTargetBitmap rtb = new RenderTargetBitmap(Convert.ToInt32(Inky.Width / 2.0), Convert.ToInt32(Inky.Height / 2.0), 96d, 96d, PixelFormats.Default);
               
                rtb.Render(Inky);
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                encoder.Save(fs);
                fs.Close();
            }
        }

        /// <summary>
        /// Display the specified panel.
        /// </summary>
        /// <param name="n">The panel number.</param>
        private void DisplayPanel(int n)
        {
            if (this.dyknow != null && n >= 0 && n < this.dyknow.DATA.Count)
            {
                this.currentPanelNumber = n;
                this.dyknow.Render(Inky, n);
                labelPageNumber.Content = (n + 1).ToString() + " of " + this.dyknow.DATA.Count;

                // Enable or disable the previous button as necessary
                if (n == 0)
                {
                    this.buttonPrevious.IsEnabled = false;
                }
                else if (!this.buttonPrevious.IsEnabled)
                {
                    this.buttonPrevious.IsEnabled = true;
                }

                // Enable or disable the next button as necessary
                if (n + 1 == this.dyknow.DATA.Count)
                {
                    this.buttonNext.IsEnabled = false;
                }
                else if (!this.buttonNext.IsEnabled)
                {
                    this.buttonNext.IsEnabled = true;
                }
            }
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
        /// Change the size of the panel being displayed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void SliderPanelSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Nothing here yet.
        }

        /// <summary>
        /// Parsers the validator window.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ParserValidatorWindow(object sender, RoutedEventArgs e)
        {
            // Display the window that is able to validate a DyKnow file.
            ParserValidatorWindow pvw = new ParserValidatorWindow();
            pvw.ShowDialog();
        }
    }
}
