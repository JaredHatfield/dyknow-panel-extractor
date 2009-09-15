using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO.Compression;
using System.IO;

namespace QuickReader
{
    public class DyKnowReader
    {
        private FileStream inputFile;
        private GZipStream gzipFile;
        private XmlTextReader xmlFile;
        private String fileName;
        private List<DyKnowPage> dyKnowPages;

        private double meanStrokes;
        private double stdDevStrokes;
        private double meanStrokeDistance;
        private double stdDevStrokeDistance;


        public String FileName
        {
            get { return fileName; }
        }
        public double MeanStrokes
        {
            get { return meanStrokes; }
        }
        public double StdDevStrokes
        {
            get { return stdDevStrokes; }
        }
        public double MeanStrokeDistance
        {
            get { return meanStrokeDistance; }
        }
        public double StdDevStrokeDistance
        {
            get { return stdDevStrokeDistance; }
        }
        public int MinStrokeCount
        {
            get
            {
                if (dyKnowPages.Count > 0)
                {
                    int min = dyKnowPages[0].NetStrokeCount;
                    for (int i = 0; i < dyKnowPages.Count; i++)
                    {
                        if (dyKnowPages[i].NetStrokeCount < min)
                        {
                            min = dyKnowPages[i].NetStrokeCount;
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
        public int MaxStrokeCount
        {
            get
            {
                if (dyKnowPages.Count > 0)
                {
                    int max = dyKnowPages[0].NetStrokeCount;
                    for (int i = 0; i < dyKnowPages.Count; i++)
                    {
                        if (dyKnowPages[i].NetStrokeCount > max)
                        {
                            max = dyKnowPages[i].NetStrokeCount;
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
        public long MinStrokeDistance
        {
            get
            {
                if (dyKnowPages.Count > 0)
                {
                    long min = dyKnowPages[0].NetStrokeDistance;
                    for (int i = 0; i < dyKnowPages.Count; i++)
                    {
                        if (dyKnowPages[i].NetStrokeDistance < min)
                        {
                            min = dyKnowPages[i].NetStrokeDistance;
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
        public long MaxStrokeDistance
        {
            get
            {
                if (dyKnowPages.Count > 0)
                {
                    long max = dyKnowPages[0].NetStrokeDistance;
                    for (int i = 0; i < dyKnowPages.Count; i++)
                    {
                        if (dyKnowPages[i].NetStrokeDistance > max)
                        {
                            max = dyKnowPages[i].NetStrokeDistance;
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



        public DyKnowReader(string name)
        {
            //The name of the DyKnow file used
            fileName = name;
            //Open the file
            inputFile = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            //Uncompress the file
            gzipFile = new GZipStream(inputFile, CompressionMode.Decompress, true);
            //Read the uncompressed file as XML
            xmlFile = new XmlTextReader(gzipFile);
            //The collection of pages
            dyKnowPages = new List<DyKnowPage>();
            //Some more default values
            meanStrokes = 0;
            stdDevStrokes = 0;

            int myRow = 1;
            //Loop through the XML information
            while (xmlFile.Read())
            {
                if (xmlFile.NodeType == XmlNodeType.Element)
                {
                    //We only care about the PAGE nodes
                    if (xmlFile.Name.ToString() == "PAGE")
                    {

                        //Process the PAGE sub-tree and store the inforation
                        DyKnowPage panel = new DyKnowPage(xmlFile.ReadSubtree(), myRow++);
                        
                        //Add the page information to the list of pages
                        dyKnowPages.Add(panel);
                    }
                }
            }

            meanStrokes = CalcMeanStrokes();
            stdDevStrokes = CalcStdDevStrokes(meanStrokes);

            meanStrokeDistance = CalcMeanStrokeDistance();
            stdDevStrokeDistance = CalcStdDevStrokeDistance(meanStrokeDistance);

            fillInFinished();
        }

        private void fillInFinished()
        {
            for (int i = 0; i < NumOfPages(); i++)
            {
                if (dyKnowPages[i].NetStrokeCount == 0)
                {
                    dyKnowPages[i].setFinished("No");
                }
                else if (dyKnowPages[i].NetStrokeCount < (meanStrokes - (2 * stdDevStrokes)))
                {
                    dyKnowPages[i].setFinished("Maybe");
                }
                else
                {
                    dyKnowPages[i].setFinished("Yes");
                }
            }
        }
        //Performs the calculation to determine the mean number of pen strokes per page in this file
        private double CalcMeanStrokes()
        {
            long total = 0;
            for (int i = 0; i < NumOfPages(); i++)
            {
                total += dyKnowPages[i].NetStrokeCount;
            }
            return (double)total / (double)NumOfPages();
        }
        //Performs the calculation to determine the standard deviation of pen strokes per page in this file
        private double CalcStdDevStrokes(double mean)
        {
            double total = 0;
            for (int i = 0; i < NumOfPages(); i++)
            {
                total += Math.Pow((dyKnowPages[i].NetStrokeCount - mean), 2);
            }
            return Math.Sqrt((double)total / (double)(NumOfPages() - 1));
        }
        //Performs the calculation to determine the mean stroke data distance per page in this specific file
        private double CalcMeanStrokeDistance()
        {
            long total = 0;
            for (int i = 0; i < NumOfPages(); i++)
            {
                total += dyKnowPages[i].NetStrokeDistance;
            }
            return (double)total / (double)NumOfPages();
        }
        //Performs the calculation to determine the standard deviation of the stroke data distance per page in this specific file
        private double CalcStdDevStrokeDistance(double mean)
        {
            double total = 0;
            for (int i = 0; i < NumOfPages(); i++)
            {
                total += Math.Pow((dyKnowPages[i].NetStrokeDistance - mean), 2);
            }
            return Math.Sqrt((double)total / (double)(NumOfPages() - 1));
        }

        public void Close()
        {
            inputFile.Close();
            gzipFile.Close();
            xmlFile.Close();
        }

        public DyKnowPage getDyKnowPage(int i)
        {
            return dyKnowPages[i];
        }

        //Used in testing
        public String GetPageString(int i)
        {
            return dyKnowPages[i].ToString();
        }


        public object[] GetRowData(int i)
        {
            return dyKnowPages[i].getRowData();
        }


        public int NumOfPages()
        {
            return dyKnowPages.Count;
        }


        override public String ToString()
        {
            return "Mean Number of Strokes: " + meanStrokes + "\n" +
                "Standard Deviation of Strokes: " + stdDevStrokes + "\n" +
                "Mean Stroke Distance: " + meanStrokeDistance + "\n" +
                "Standard Deviation of Stroke Distance " + stdDevStrokeDistance + "\n";
        }

    }
}
