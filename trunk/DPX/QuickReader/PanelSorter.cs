// <copyright file="PanelSorter.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>A parser that is able to sort the panels in a DyKnow file and produce a new file.</summary>
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
    /// A parser that is able to sort the panels in a DyKnow file and produce a new file.
    /// </summary>
    public class PanelSorter
    {
        /// <summary>
        /// The location of the input file.
        /// </summary>
        private string inputfile;

        /// <summary>
        /// The location of the new output file.
        /// </summary>
        private string outputfile;

        /// <summary>
        /// The way the file will be sorted (true for by name, false for by username).
        /// </summary>
        private bool sorttype;

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelSorter"/> class.
        /// </summary>
        /// <param name="input">The input file name.</param>
        /// <param name="output">The output file name.</param>
        public PanelSorter(string input, string output)
        {
            this.inputfile = input;
            this.outputfile = output;
            this.sorttype = true;
        }

        /// <summary>
        /// Sets sort by user name.
        /// </summary>
        public void SetSortByUsername()
        {
            this.sorttype = false;
        }

        /// <summary>
        /// Sets sort by full name.
        /// </summary>
        public void SetSortByFullName()
        {
            this.sorttype = true;
        }

        /// <summary>
        /// Processes the sort.
        /// </summary>
        public void ProcessSort()
        {
            // File Input
            FileStream originalFile = new FileStream(this.inputfile, FileMode.Open, FileAccess.Read, FileShare.Read);
            GZipStream unzipedFile = new GZipStream(originalFile, CompressionMode.Decompress);
            XmlTextReader xmlFile = new XmlTextReader(unzipedFile);
            
            // File Output
            FileStream newFile = new FileStream(this.outputfile, FileMode.Create, FileAccess.Write, FileShare.Write);
            GZipStream zippedFile = new GZipStream(newFile, CompressionMode.Compress);
            XmlTextWriter newXmlFile = new XmlTextWriter(zippedFile, Encoding.ASCII);

            // A collection to keep all of the pages in
            List<XmlDocument> pages = new List<XmlDocument>();

            while (xmlFile.Read())
            {
                if (xmlFile.NodeType == XmlNodeType.Element)
                {
                    if (xmlFile.Name.ToString() == "PAGE")
                    {
                        // Store all of the pages in memory so they can be sorted
                        XmlDocument d = new XmlDocument();
                        d.Load(xmlFile.ReadSubtree());
                        pages.Add(d);
                    }
                    else if (xmlFile.Name.ToString() == "IMG")
                    {
                        newXmlFile.WriteStartElement(xmlFile.Name);
                        newXmlFile.WriteValue(xmlFile.ReadString());
                        newXmlFile.WriteEndElement();
                    }
                    else if (xmlFile.Name.ToString() == "ID")
                    {
                        newXmlFile.WriteStartElement(xmlFile.Name);
                        newXmlFile.WriteValue(xmlFile.ReadString());
                        newXmlFile.WriteEndElement();
                    }
                    else
                    {
                        newXmlFile.WriteStartElement(xmlFile.Name);
                        while (xmlFile.MoveToNextAttribute())
                        {
                            newXmlFile.WriteAttributeString(xmlFile.Name.ToString(), xmlFile.Value.ToString());
                        }
                    }
                } 
                else if (xmlFile.NodeType == XmlNodeType.EndElement)
                {
                    if (xmlFile.Name.ToString() == "PAGE")
                    {
                        // We are dealing with a page as an entire subtree
                    }
                    else if (xmlFile.Name.ToString() == "IMG")
                    {
                        // Previously closed
                    }
                    else if (xmlFile.Name.ToString() == "ID")
                    {
                        // Previously closed
                    }
                    else if (xmlFile.Name.ToString() == "DATA")
                    {
                        // Sort the pages
                        this.SortPages(pages);

                        // Write the pages
                        for (int i = 0; i < pages.Count; i++)
                        {
                            newXmlFile.WriteNode(new XmlTextReader(new StringReader(pages[i].OuterXml)), false);
                        }

                        // Close the data tag
                        newXmlFile.WriteEndElement();
                    }
                    else
                    {
                        newXmlFile.WriteEndElement();
                    }
                }
            }

            // Close the input file
            xmlFile.Close();
            unzipedFile.Close();
            originalFile.Close();

            // Close the output file
            newXmlFile.Close();
            zippedFile.Close();
            newFile.Close();
        }

        /// <summary>
        /// Sorts the pages.
        /// </summary>
        /// <param name="pages">The pages to be sorted.</param>
        private void SortPages(List<XmlDocument> pages)
        {
            // Bubble sort algorithm to put all of the pages in the correct order
            for (int i = 0; i < pages.Count; i++)
            {
                for (int j = 0; j < pages.Count; j++)
                {
                    if (this.sorttype)
                    {
                        // Sorts by name (ONERN)
                        if (pages[i]["PAGE"].Attributes["ONERN"].Value.ToString().CompareTo(
                            pages[j]["PAGE"].Attributes["ONERN"].Value.ToString()) < 0)
                        {
                            XmlDocument d = pages[i];
                            pages[i] = pages[j];
                            pages[j] = d;
                        }
                    }
                    else
                    {
                        // Sorts by username (ONER)
                        if (pages[i]["PAGE"].Attributes["ONER"].Value.ToString().CompareTo(
                            pages[j]["PAGE"].Attributes["ONER"].Value.ToString()) < 0)
                        {
                            XmlDocument d = pages[i];
                            pages[i] = pages[j];
                            pages[j] = d;
                        }
                    }
                }
            }
        }
    }
}
