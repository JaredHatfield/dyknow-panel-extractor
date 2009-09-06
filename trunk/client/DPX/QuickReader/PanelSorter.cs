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

        public PanelSorter(String input, String output)
        {
            //File Input
            FileStream originalFile = new FileStream(input, FileMode.Open, FileAccess.Read, FileShare.Read);
            GZipStream unzipedFile = new GZipStream(originalFile, CompressionMode.Decompress);
            XmlTextReader xmlFile = new XmlTextReader(unzipedFile);

            //File Output
            FileStream newFile = new FileStream(output, FileMode.Create, FileAccess.Write, FileShare.Write);
            GZipStream zippedFile = new GZipStream(newFile, CompressionMode.Compress);
            XmlTextWriter newXmlFile = new XmlTextWriter(zippedFile, Encoding.ASCII);


            while (xmlFile.Read())
            {
                if (xmlFile.NodeType == XmlNodeType.Element)
                {
                    if (xmlFile.Name.ToString() == "PAGE")
                    {
                        //xmlFile.ReadSubtree();
                        newXmlFile.WriteNode(xmlFile.ReadSubtree(), false);

                        while (!(xmlFile.NodeType == XmlNodeType.EndElement && xmlFile.Name.ToString() == "PAGE"))
                        {
                            xmlFile.Read();
                        }
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
    }
}
