using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO.Compression;
using System.IO;

namespace QuickReader
{
    public class PanelSorter
    {
        String inputfile;
        String outputfile;
        Boolean sorttype; //True sorts by name, false sorts by username

        public PanelSorter(string input, String output)
        {
            inputfile = input;
            outputfile = output;
            sorttype = true;
        }

        public void setSortByUsername()
        {
            sorttype = false;
        }

        public void setSortByFullName()
        {
            sorttype = true;
        }

        public void processSort()
        {
            //File Input
            FileStream originalFile = new FileStream(inputfile, FileMode.Open, FileAccess.Read, FileShare.Read);
            GZipStream unzipedFile = new GZipStream(originalFile, CompressionMode.Decompress);
            XmlTextReader xmlFile = new XmlTextReader(unzipedFile);
            
            //File Output
            FileStream newFile = new FileStream(outputfile, FileMode.Create, FileAccess.Write, FileShare.Write);
            GZipStream zippedFile = new GZipStream(newFile, CompressionMode.Compress);
            XmlTextWriter newXmlFile = new XmlTextWriter(zippedFile, Encoding.ASCII);

            //A collection to keep all of the pages in
            List<XmlDocument> pages = new List<XmlDocument>();

            while (xmlFile.Read())
            {
                if (xmlFile.NodeType == XmlNodeType.Element)
                {
                    if (xmlFile.Name.ToString() == "PAGE")
                    {
                        //Store all of the pages in memory so they can be sorted
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
                        //We are dealing with a page as an entire subtree
                    }
                    else if (xmlFile.Name.ToString() == "IMG")
                    {
                        //Previously closed
                    }
                    else if (xmlFile.Name.ToString() == "ID")
                    {
                        //Previously closed
                    }
                    else if (xmlFile.Name.ToString() == "DATA")
                    {
                        //Sort the pages
                        sortpages(pages);
                        //Write the pages
                        for (int i = 0; i < pages.Count; i++)
                        {
                            newXmlFile.WriteNode(new XmlTextReader(new StringReader(pages[i].OuterXml)), false);
                        }
                        //Close the data tag
                        newXmlFile.WriteEndElement();
                    }
                    else
                    {
                        newXmlFile.WriteEndElement();
                    }
                }
                     
            }

            //Close the input file
            xmlFile.Close();
            unzipedFile.Close();
            originalFile.Close();

            //Close the output file
            newXmlFile.Close();
            zippedFile.Close();
            newFile.Close();

        }

        private void sortpages(List<XmlDocument> pages)
        {
            //Bubble sort algorithm to put all of the pages in the correct order
            for (int i = 0; i < pages.Count; i++)
            {
                for (int j = 0; j < pages.Count; j++)
                {
                    //Sorts by name (ONERN)
                    if (sorttype)
                    {
                        if (pages[i]["PAGE"].Attributes["ONERN"].Value.ToString().CompareTo(
                            pages[j]["PAGE"].Attributes["ONERN"].Value.ToString()) < 0)
                        {
                            XmlDocument d = pages[i];
                            pages[i] = pages[j];
                            pages[j] = d;
                        }
                    }
                    //Sorts by username (ONER)
                    else
                    {
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
