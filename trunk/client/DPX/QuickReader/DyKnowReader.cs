// <copyright file="DyKnowReader.cs" company="DPX">
// GNU General Public License v3
// </copyright>
// <summary>DyKnow Reader class.</summary>
namespace QuickReader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// 
    /// </summary>
    public class DyKnowReader
    {
        /// <summary>
        /// 
        /// </summary>
        private FileStream inputFile;

        /// <summary>
        /// 
        /// </summary>
        private GZipStream gzipFile;

        /// <summary>
        /// 
        /// </summary>
        private XmlTextReader xmlFile;

        /// <summary>
        /// 
        /// </summary>
        private string fileName;

        /// <summary>
        /// 
        /// </summary>
        private List<DyKnowPage> dyKnowPages;

        /// <summary>
        /// 
        /// </summary>
        private List<ImageData> imageInformation;

        /// <summary>
        /// 
        /// </summary>
        private double meanStrokes;

        /// <summary>
        /// 
        /// </summary>
        private double stdDevStrokes;

        /// <summary>
        /// 
        /// </summary>
        private double meanStrokeDistance;

        /// <summary>
        /// 
        /// </summary>
        private double stdDevStrokeDistance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
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
            this.dyKnowPages = new List<DyKnowPage>();

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
                        this.dyKnowPages.Add(panel);
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
        /// 
        /// </summary>
        public string FileName
        {
            get { return this.fileName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double MeanStrokes
        {
            get { return this.meanStrokes; }
        }

        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        public double MeanStrokeDistance
        {
            get { return this.meanStrokeDistance; }
        }

        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        public int MinStrokeCount
        {
            get
            {
                if (this.dyKnowPages.Count > 0)
                {
                    int min = this.dyKnowPages[0].NetStrokeCount;
                    for (int i = 0; i < this.dyKnowPages.Count; i++)
                    {
                        if (this.dyKnowPages[i].NetStrokeCount < min)
                        {
                            min = this.dyKnowPages[i].NetStrokeCount;
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
        /// 
        /// </summary>
        public int MaxStrokeCount
        {
            get
            {
                if (this.dyKnowPages.Count > 0)
                {
                    int max = this.dyKnowPages[0].NetStrokeCount;
                    for (int i = 0; i < this.dyKnowPages.Count; i++)
                    {
                        if (this.dyKnowPages[i].NetStrokeCount > max)
                        {
                            max = this.dyKnowPages[i].NetStrokeCount;
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
        /// 
        /// </summary>
        public long MinStrokeDistance
        {
            get
            {
                if (this.dyKnowPages.Count > 0)
                {
                    long min = this.dyKnowPages[0].NetStrokeDistance;
                    for (int i = 0; i < this.dyKnowPages.Count; i++)
                    {
                        if (this.dyKnowPages[i].NetStrokeDistance < min)
                        {
                            min = this.dyKnowPages[i].NetStrokeDistance;
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
        /// 
        /// </summary>
        public long MaxStrokeDistance
        {
            get
            {
                if (this.dyKnowPages.Count > 0)
                {
                    long max = this.dyKnowPages[0].NetStrokeDistance;
                    for (int i = 0; i < this.dyKnowPages.Count; i++)
                    {
                        if (this.dyKnowPages[i].NetStrokeDistance > max)
                        {
                            max = this.dyKnowPages[i].NetStrokeDistance;
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
        /// 
        /// </summary>
        public List<ImageData> ImageInformation
        {
            get { return this.imageInformation; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        public void Close()
        {
            this.inputFile.Close();
            this.gzipFile.Close();
            this.xmlFile.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public DyKnowPage GetDyKnowPage(int i)
        {
            return this.dyKnowPages[i];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetPagestring(int i)
        {
            return this.dyKnowPages[i].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public object[] GetRowData(int i)
        {
            return this.dyKnowPages[i].GetRowData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int NumOfPages()
        {
            return this.dyKnowPages.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Mean Number of Strokes: " + this.meanStrokes + "\n" +
                "Standard Deviation of Strokes: " + this.stdDevStrokes + "\n" +
                "Mean Stroke Distance: " + this.meanStrokeDistance + "\n" +
                "Standard Deviation of Stroke Distance " + this.stdDevStrokeDistance + "\n";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subfile"></param>
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
        /// 
        /// </summary>
        private void FillInFinished()
        {
            for (int i = 0; i < this.NumOfPages(); i++)
            {
                if (this.dyKnowPages[i].NetStrokeCount == 0)
                {
                    this.dyKnowPages[i].SetFinished("No");
                }
                else if (this.dyKnowPages[i].NetStrokeCount < (this.meanStrokes - (2 * this.stdDevStrokes)))
                {
                    this.dyKnowPages[i].SetFinished("Maybe");
                }
                else
                {
                    this.dyKnowPages[i].SetFinished("Yes");
                }
            }
        }

        /// <summary>
        /// Performs the calculation to determine the mean number of pen strokes per page in this file.
        /// </summary>
        /// <returns></returns>
        private double CalcMeanStrokes()
        {
            long total = 0;
            for (int i = 0; i < this.NumOfPages(); i++)
            {
                total += this.dyKnowPages[i].NetStrokeCount;
            }

            return (double)total / (double)this.NumOfPages();
        }

        /// <summary>
        /// Performs the calculation to determine the standard deviation of pen strokes per page in this file.
        /// </summary>
        /// <param name="mean"></param>
        /// <returns></returns>
        private double CalcStdDevStrokes(double mean)
        {
            double total = 0;
            for (int i = 0; i < this.NumOfPages(); i++)
            {
                total += Math.Pow((this.dyKnowPages[i].NetStrokeCount - mean), 2);
            }

            return Math.Sqrt((double)total / (double)(this.NumOfPages() - 1));
        }

        /// <summary>
        /// Performs the calculation to determine the mean stroke data distance per page in this specific file.
        /// </summary>
        /// <returns></returns>
        private double CalcMeanStrokeDistance()
        {
            long total = 0;
            for (int i = 0; i < this.NumOfPages(); i++)
            {
                total += this.dyKnowPages[i].NetStrokeDistance;
            }

            return (double)total / (double)this.NumOfPages();
        }

        /// <summary>
        /// Performs the calculation to determine the standard deviation of the stroke data distance per page in this specific file.
        /// </summary>
        /// <param name="mean"></param>
        /// <returns></returns>
        private double CalcStdDevStrokeDistance(double mean)
        {
            double total = 0;
            for (int i = 0; i < this.NumOfPages(); i++)
            {
                total += Math.Pow((this.dyKnowPages[i].NetStrokeDistance - mean), 2);
            }

            return Math.Sqrt((double)total / (double)(this.NumOfPages() - 1));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subfile"></param>
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
