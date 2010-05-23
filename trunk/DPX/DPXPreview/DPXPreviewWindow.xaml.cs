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
            menuUserInformation.IsEnabled = false;
            menuStatistics.IsEnabled = false;
        }

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
                // Open the DyKnow file
                this.dyknow = DyKnow.DeserializeFromFile(openFileDialog1.FileName);
                this.currentPanelNumber = 0;
                Inky.Strokes.Clear();
                this.DisplayPanel(this.currentPanelNumber);
                this.UpdatePageNumber();
                menuUserInformation.IsEnabled = true;
                menuStatistics.IsEnabled = true;
            }
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
                    this.DisplayPanel(++this.currentPanelNumber);
                    this.UpdatePageNumber();
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
                    this.DisplayPanel(--this.currentPanelNumber);
                    this.UpdatePageNumber();
                }
            }
        }

        /// <summary>
        /// Update the page number.
        /// </summary>
        private void UpdatePageNumber()
        {
            labelPageNumber.Content = (this.currentPanelNumber + 1).ToString() + " of " + this.dyknow.DATA.Count;
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
        /// Display the UserInformation window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void DisplayUserInformationWindow(object sender, RoutedEventArgs e)
        {
            /*
            FlowDocument fd = new FlowDocument();
            fd.Blocks.Add(new Paragraph(new Run("Students")));
            if (fd != null)
            {
                for (int i = 0; i < this.dr.NumOfPages(); i++)
                {
                    DyKnowPage d = this.dr.GetDyKnowPage(i);
                    string s = (i + 1).ToString() + ") " + d.FullName;
                    Paragraph p = new Paragraph(new Run(s));
                    p.LineHeight = 5.0;
                    fd.Blocks.Add(p);
                }
            }

            UserInformation popupWindow = new UserInformation(fd);
            popupWindow.Owner = this;
            popupWindow.ShowDialog();
             */
        }

        /// <summary>
        /// Display the Statistics window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void DisplayStatisticsWindow(object sender, RoutedEventArgs e)
        {
            /*
            Statistics popupWindow = new Statistics(this.dr);
            popupWindow.Owner = this;
            popupWindow.ShowDialog();
             */
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
    }
}
