// <copyright file="Window1.xaml.cs" company="University of Louisville Speed School of Engineering">
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
    using QuickReader;

    /// <summary>
    /// The main window for Preview.
    /// </summary>
    public partial class Window1 : Window
    {
        /// <summary>
        /// Instance of DyKnowReader.
        /// </summary>
        private DyKnowReader dr;

        /// <summary>
        /// The current page being displayed.
        /// </summary>
        private int currentPanelNumber = 0;

        /// <summary>
        /// Initializes a new instance of the Window1 class.
        /// </summary>
        public Window1()
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
                this.dr = new DyKnowReader(openFileDialog1.FileName);
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
            if (this.dr != null)
            {
                if ((this.currentPanelNumber + 1) < this.dr.NumOfPages())
                {
                    Inky.Strokes.Clear();
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
            if (this.dr != null)
            {
                if (this.currentPanelNumber > 0)
                {
                    Inky.Strokes.Clear();
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
            labelPageNumber.Content = (this.currentPanelNumber + 1).ToString() + " of " + this.dr.NumOfPages().ToString();
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
            // Some error checking to make sure we don't crash
            if (this.dr != null && n >= 0 && n < this.dr.NumOfPages())
            {
                this.currentPanelNumber = n;

                // Read in the panel
                DyKnowPage dp = this.dr.GetDyKnowPage(n);

                // Display all of the images
                Inky.Children.Clear();
                List<DyKnowImage> dki = dp.Images;

                // Add all of the images as children (there should only be 1, but this works for now)
                for (int i = 0; i < dki.Count; i++)
                {
                    // Get the actual image
                    ImageData id = this.dr.GetImageData(dki[i].Id);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = new MemoryStream(System.Convert.FromBase64String(id.Img));
                    bi.EndInit();

                    // Resize the image if it is not the correct size
                    TransformedBitmap tb = new TransformedBitmap();
                    tb.BeginInit();
                    tb.Source = bi;
                    ScaleTransform sc = new ScaleTransform(Inky.Width / bi.Width, Inky.Height / bi.Height);
                    tb.Transform = sc;
                    tb.EndInit();

                    // Add the image to the canvas
                    Image im = new Image();
                    im.Source = tb;
                    Inky.Children.Add(im);
                }
                
                // Get that Panel's pen strokes
                List<DyKnowPenStroke> pens = dp.Pens;

                // Loop through all of the pen strokes
                for (int i = 0; i < pens.Count; i++)
                {
                    // Only display the ink if it wasn't deleted
                    if (!pens[i].DELETED)
                    {
                        // The data is encoded as a string
                        string data = pens[i].DATA;

                        // Truncate off the "base64:" from the beginning of the string
                        data = data.Substring(7);

                        // Decode the string
                        byte[] bufferData = Convert.FromBase64String(data);

                        // Turn the string into a stream
                        Stream s = new MemoryStream(bufferData);

                        // Convert the stream into an ink stroke
                        StrokeCollection sc = new StrokeCollection(s);

                        // Resize the panel if it is not the default resolution
                        if (pens[i].PH != Inky.Height || pens[i].PW != Inky.Width)
                        {
                            Matrix inkTransform = new Matrix();
                            inkTransform.Scale(Inky.Width / (double)pens[i].PW, Inky.Height / (double)pens[i].PH);
                            sc.Transform(inkTransform, true);
                        }

                        // Add the ink stroke to the canvas
                        Inky.Strokes.Add(sc);
                    }
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
        /// Display the UserInformation window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void DisplayUserInformationWindow(object sender, RoutedEventArgs e)
        {
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
        }

        /// <summary>
        /// Display the Statistics window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void DisplayStatisticsWindow(object sender, RoutedEventArgs e)
        {
            Statistics popupWindow = new Statistics(this.dr);
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
    }
}
