// <copyright file="DyKnowReader.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Reads in the contents of a DyKnow file.</summary>
namespace QuickReader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Text;
    using System.Windows.Controls;
    using System.Windows.Ink;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Xml;

    /// <summary>
    /// Reads in the contents of a DyKnow file.
    /// </summary>
    public class DyKnowReader
    {
        /// <summary>
        /// THe input file stream..
        /// </summary>
        private FileStream inputFile;

        /// <summary>
        /// The gzip file stream.
        /// </summary>
        private GZipStream gzipFile;

        /// <summary>
        /// The xml file stream.
        /// </summary>
        private XmlTextReader xmlFile;

        /// <summary>
        /// The file name.
        /// </summary>
        private string fileName;

        /// <summary>
        /// The collection of DyKnow pages.
        /// </summary>
        private List<DyKnowPage> dyknowPages;

        /// <summary>
        /// The collection of DyKnow background image data.
        /// </summary>
        private List<ImageData> imageInformation;

        /// <summary>
        /// The mean number of pen strokes on the panels.
        /// </summary>
        private double meanStrokes;

        /// <summary>
        /// The standard deviation of the pen strokes.
        /// </summary>
        private double stdDevStrokes;

        /// <summary>
        /// The mean storke data.
        /// </summary>
        private double meanStrokeDistance;

        /// <summary>
        /// The standard deviation of the stroke data.
        /// </summary>
        private double stdDevStrokeDistance;

        /// <summary>
        /// Initializes a new instance of the <see cref="DyKnowReader"/> class.
        /// </summary>
        /// <param name="name">The name of the file.</param>
        public DyKnowReader(string name)
        {
            // The name of the DyKnow file used
            this.fileName = name;

            // Open the file
            this.inputFile = new FileStream(this.fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            // Uncompress the file
            this.gzipFile = new GZipStream(this.inputFile, CompressionMode.Decompress, true);

            // Read the uncompressed file as XML
            this.xmlFile = new XmlTextReader(this.gzipFile);

            // The collection of pages
            this.dyknowPages = new List<DyKnowPage>();

            // The collection of inmageData
            this.imageInformation = new List<ImageData>();

            // Some more default values
            this.meanStrokes = 0;
            this.stdDevStrokes = 0;
            int myRow = 1;

            // Loop through the XML information
            while (this.xmlFile.Read())
            {
                if (this.xmlFile.NodeType == XmlNodeType.Element)
                {
                    // We only care about the PAGE nodes
                    if (this.xmlFile.Name.ToString() == "PAGE")
                    {
                        // Process the PAGE sub-tree and store the inforation
                        DyKnowPage panel = new DyKnowPage(this.xmlFile.ReadSubtree(), myRow++);

                        // Add the page information to the list of pages
                        this.dyknowPages.Add(panel);
                    }
                    else if (this.xmlFile.Name.ToString() == "IMGS")
                    {
                        this.ParseIMGS(this.xmlFile.ReadSubtree());
                    }
                    else if (this.xmlFile.Name.ToString() == "IMGD")
                    {
                        this.ParseIMGD(this.xmlFile.ReadSubtree());
                    }
                }
            }

            this.meanStrokes = this.CalcMeanStrokes();
            this.stdDevStrokes = this.CalcStdDevStrokes(this.meanStrokes);
            this.meanStrokeDistance = this.CalcMeanStrokeDistance();
            this.stdDevStrokeDistance = this.CalcStdDevStrokeDistance(this.meanStrokeDistance);
            this.FillInFinished();
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName
        {
            get { return this.fileName; }
        }

        /// <summary>
        /// Gets the mean pen strokes per panel.
        /// </summary>
        /// <value>The mean pen strokes per panel.</value>
        public double MeanStrokes
        {
            get { return this.meanStrokes; }
        }

        /// <summary>
        /// Gets the standard deviation of the pen strokes.
        /// </summary>
        /// <value>The standard deviation of the pen strokes.</value>
        public double StdDevStrokes
        {
            get
            {
                if (this.NumOfPages() < 2)
                {
                    return 0;
                }
                else
                {
                    return this.stdDevStrokes;
                }
            }
        }

        /// <summary>
        /// Gets the mean stroke distance.
        /// </summary>
        /// <value>The mean stroke distance.</value>
        public double MeanStrokeDistance
        {
            get { return this.meanStrokeDistance; }
        }

        /// <summary>
        /// Gets the standard deviation of the stroke distance.
        /// </summary>
        /// <value>The standard deviation of the stroke distance.</value>
        public double StdDevStrokeDistance
        {
            get
            {
                if (this.NumOfPages() < 2)
                {
                    return 0;
                }
                else
                {
                    return this.stdDevStrokeDistance;
                }
            }
        }

        /// <summary>
        /// Gets the minimum stroke count.
        /// </summary>
        /// <value>The minimum stroke count.</value>
        public int MinStrokeCount
        {
            get
            {
                if (this.dyknowPages.Count > 0)
                {
                    int min = this.dyknowPages[0].NetStrokeCount;
                    for (int i = 0; i < this.dyknowPages.Count; i++)
                    {
                        if (this.dyknowPages[i].NetStrokeCount < min)
                        {
                            min = this.dyknowPages[i].NetStrokeCount;
                        }
                    }

                    return min;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the maximum stroke count.
        /// </summary>
        /// <value>The maximum stroke count.</value>
        public int MaxStrokeCount
        {
            get
            {
                if (this.dyknowPages.Count > 0)
                {
                    int max = this.dyknowPages[0].NetStrokeCount;
                    for (int i = 0; i < this.dyknowPages.Count; i++)
                    {
                        if (this.dyknowPages[i].NetStrokeCount > max)
                        {
                            max = this.dyknowPages[i].NetStrokeCount;
                        }
                    }

                    return max;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the minimum stroke distance.
        /// </summary>
        /// <value>The minimum stroke distance.</value>
        public long MinStrokeDistance
        {
            get
            {
                if (this.dyknowPages.Count > 0)
                {
                    long min = this.dyknowPages[0].NetStrokeDistance;
                    for (int i = 0; i < this.dyknowPages.Count; i++)
                    {
                        if (this.dyknowPages[i].NetStrokeDistance < min)
                        {
                            min = this.dyknowPages[i].NetStrokeDistance;
                        }
                    }

                    return min;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the maximum stroke distance.
        /// </summary>
        /// <value>The maximum stroke distance.</value>
        public long MaxStrokeDistance
        {
            get
            {
                if (this.dyknowPages.Count > 0)
                {
                    long max = this.dyknowPages[0].NetStrokeDistance;
                    for (int i = 0; i < this.dyknowPages.Count; i++)
                    {
                        if (this.dyknowPages[i].NetStrokeDistance > max)
                        {
                            max = this.dyknowPages[i].NetStrokeDistance;
                        }
                    }

                    return max;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the collection of image information.
        /// </summary>
        /// <value>The collection of image information.</value>
        public List<ImageData> ImageInformation
        {
            get { return this.imageInformation; }
        }

        /// <summary>
        /// Gets the image data for a specific uid.
        /// </summary>
        /// <param name="uid">The uid to location image data for.</param>
        /// <returns>The image data.</returns>
        public ImageData GetImageData(Guid uid)
        {
            for (int i = 0; i < this.imageInformation.Count; i++)
            {
                if (this.imageInformation[i].Id.Equals(uid))
                {
                    return this.imageInformation[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            this.inputFile.Close();
            this.gzipFile.Close();
            this.xmlFile.Close();
        }

        /// <summary>
        /// Gets the DyKnow page.
        /// </summary>
        /// <param name="i">The page number.</param>
        /// <returns>The requiested DyKnow page.</returns>
        public DyKnowPage GetDyKnowPage(int i)
        {
            return this.dyknowPages[i];
        }

        /// <summary>
        /// Fills the ink canvas.
        /// </summary>
        /// <param name="inky">The InkCanvas to write on.</param>
        /// <param name="n">The panel number.</param>
        public void FillInkCanvas(InkCanvas inky, int n)
        {
            // Read in the panel
            DyKnowPage dp = this.GetDyKnowPage(n);

            // Display all of the images
            inky.Children.Clear();
            List<DyKnowImage> dki = dp.Images;

            // Add all of the images as children (there should only be 1, but this works for now)
            for (int i = 0; i < dki.Count; i++)
            {
                // Get the actual image
                ImageData id = this.GetImageData(dki[i].Id);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(System.Convert.FromBase64String(id.Img));
                bi.EndInit();

                // Resize the image if it is not the correct size
                TransformedBitmap tb = new TransformedBitmap();
                tb.BeginInit();
                tb.Source = bi;
                ScaleTransform sc = new ScaleTransform(inky.Width / bi.Width, inky.Height / bi.Height);
                tb.Transform = sc;
                tb.EndInit();

                // Add the image to the canvas
                Image im = new Image();
                im.Source = tb;
                inky.Children.Add(im);
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
                    if (pens[i].PH != inky.Height || pens[i].PW != inky.Width)
                    {
                        Matrix inkTransform = new Matrix();
                        inkTransform.Scale(inky.Width / (double)pens[i].PW, inky.Height / (double)pens[i].PH);
                        sc.Transform(inkTransform, true);
                    }

                    // Add the ink stroke to the canvas
                    inky.Strokes.Add(sc);
                }
            }
        }

        /// <summary>
        /// Gets the string representation of the requested page..
        /// </summary>
        /// <param name="i">The page number.</param>
        /// <returns>The string representation of the requested page.</returns>
        public string GetPagestring(int i)
        {
            return this.dyknowPages[i].ToString();
        }

        /// <summary>
        /// Gets the row data for the requested page..
        /// </summary>
        /// <param name="i">The requested page.</param>
        /// <returns>An object array for the requested page.</returns>
        public object[] GetRowData(int i)
        {
            return this.dyknowPages[i].GetRowData();
        }

        /// <summary>
        /// Gets the number of panels.
        /// </summary>
        /// <returns>The number of panels.</returns>
        public int NumOfPages()
        {
            return this.dyknowPages.Count;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Mean Number of Strokes: " + this.meanStrokes + "\n" +
                "Standard Deviation of Strokes: " + this.stdDevStrokes + "\n" +
                "Mean Stroke Distance: " + this.meanStrokeDistance + "\n" +
                "Standard Deviation of Stroke Distance " + this.stdDevStrokeDistance + "\n";
        }

        /// <summary>
        /// Parses the IMGD.
        /// </summary>
        /// <param name="subfile">The subfile.</param>
        private void ParseIMGD(XmlReader subfile)
        {
            int num = 0;
            while (subfile.Read())
            {
                if (subfile.Name.ToString() == "ID")
                {
                    ImageData id = this.imageInformation[num];
                    string s = subfile.ReadString();
                    id.Id = new Guid(s);
                    num++;
                }
            }
        }

        /// <summary>
        /// Fills the in finished flags.
        /// </summary>
        private void FillInFinished()
        {
            for (int i = 0; i < this.NumOfPages(); i++)
            {
                if (this.dyknowPages[i].NetStrokeCount == 0)
                {
                    this.dyknowPages[i].SetFinished("No");
                }
                else if (this.dyknowPages[i].NetStrokeCount < (this.meanStrokes - (2 * this.stdDevStrokes)))
                {
                    this.dyknowPages[i].SetFinished("Maybe");
                }
                else
                {
                    this.dyknowPages[i].SetFinished("Yes");
                }
            }
        }

        /// <summary>
        /// Performs the calculation to determine the mean number of pen strokes per page in this file.
        /// </summary>
        /// <returns>The mean number of pen strokes.</returns>
        private double CalcMeanStrokes()
        {
            long total = 0;
            for (int i = 0; i < this.NumOfPages(); i++)
            {
                total += this.dyknowPages[i].NetStrokeCount;
            }

            return (double)total / (double)this.NumOfPages();
        }

        /// <summary>
        /// Performs the calculation to determine the standard deviation of pen strokes per page in this file.
        /// </summary>
        /// <param name="mean">The mean number of pen strokes.</param>
        /// <returns>The standard deviation of the pen strokes.</returns>
        private double CalcStdDevStrokes(double mean)
        {
            double total = 0;
            for (int i = 0; i < this.NumOfPages(); i++)
            {
                total += Math.Pow((this.dyknowPages[i].NetStrokeCount - mean), 2);
            }

            return Math.Sqrt((double)total / (double)(this.NumOfPages() - 1));
        }

        /// <summary>
        /// Performs the calculation to determine the mean stroke data distance per page in this specific file.
        /// </summary>
        /// <returns>The mean stroke data length.</returns>
        private double CalcMeanStrokeDistance()
        {
            long total = 0;
            for (int i = 0; i < this.NumOfPages(); i++)
            {
                total += this.dyknowPages[i].NetStrokeDistance;
            }

            return (double)total / (double)this.NumOfPages();
        }

        /// <summary>
        /// Performs the calculation to determine the standard deviation of the stroke data distance per page in this specific file.
        /// </summary>
        /// <param name="mean">The mean stroke data length.</param>
        /// <returns>The standard deviation of the stroke data length.</returns>
        private double CalcStdDevStrokeDistance(double mean)
        {
            double total = 0;
            for (int i = 0; i < this.NumOfPages(); i++)
            {
                total += Math.Pow((this.dyknowPages[i].NetStrokeDistance - mean), 2);
            }

            return Math.Sqrt((double)total / (double)(this.NumOfPages() - 1));
        }

        /// <summary>
        /// Parses the IMGS.
        /// </summary>
        /// <param name="subfile">The data to parse.</param>
        private void ParseIMGS(XmlReader subfile)
        {
            while (subfile.Read())
            {
                if (subfile.Name.ToString() == "IMG")
                {
                    ImageData id = new ImageData(new Guid(), subfile.ReadString());
                    this.imageInformation.Add(id);
                }
            }
        }
    }
}
