﻿// <copyright file="PanelPreview.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The window that displays the preview for a DyKnow panel.</summary>
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
    /// The window that displays the preview for a DyKnow panel.
    /// </summary>
    public partial class PanelPreview : Window
    {
        /// <summary>
        /// DyKnow reader instance which contains the information about the file.
        /// </summary>
        private DyKnowReader dr;

        /// <summary>
        /// Initializes a new instance of the PanelPreview class.
        /// </summary>
        /// <param name="dr">Already opened instance of the DyKnow reader class.</param>
        public PanelPreview(DyKnowReader dr)
        {
            InitializeComponent();
            this.dr = dr;
        }

        /// <summary>
        /// Display the panel specified by an index.
        /// </summary>
        /// <param name="n">The panel number to display.</param>
        public void DisplayPanel(int n)
        {
            // Some error checking to make sure we don't crash
            if (this.dr != null && n >= 0 && n < this.dr.NumOfPages())
            {
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
    }
}
