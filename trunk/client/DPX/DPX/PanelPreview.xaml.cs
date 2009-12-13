// <copyright file="PanelPreview.xaml.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace DPX
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
    using QuickReader;

    /// <summary>
    /// Interaction logic for PanelPreview.xaml
    /// </summary>
    public partial class PanelPreview : Window
    {
        private DyKnowReader dr;

        public PanelPreview(DyKnowReader dr)
        {
            InitializeComponent();
            this.dr = dr;
        }

        public void DisplayPanel(int n)
        {
            // Some error checking to make sure we don't crash
            if (this.dr != null && n >= 0 && n < this.dr.NumOfPages())
            {
                // Read in the panel
                DyKnowPage dp = this.dr.getDyKnowPage(n);

                // Display all of the images
                Inky.Children.Clear();
                List<DyKnowImage> dki = dp.Images;

                // Add all of the images as children (there should only be 1, but this works for now)
                for (int i = 0; i < dki.Count; i++)
                {
                    // Get the actual image
                    ImageData id = this.dr.getImageData(dki[i].Id);
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
    }
}
