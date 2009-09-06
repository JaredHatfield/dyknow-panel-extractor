using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QuickReader
{
    public class DyKnowPage
    {
        private int pageNumber;
        private String userName;
        private String fullName;
        private int strokes;
        private String finished;

        //Constructor accepts the XML sub tree
        public DyKnowPage(XmlReader xmlFile, int pageNum)
        {
            //Set some default values if the xml parsing doesn't workout
            pageNumber = pageNum;
            userName = "";
            fullName = "";
            strokes = 0;
            finished = "";

            List<String> myStrokes = new List<string>();

            while (xmlFile.Read())
            {
                //Used to identify the user name and full name
                if (xmlFile.Name.ToString() == "PAGE")
                {
                    while (xmlFile.MoveToNextAttribute())
                    {
                        //User Name
                        if (xmlFile.Name.ToString() == "ONER")
                        {
                            userName = xmlFile.Value.ToString();
                        }
                        //Full Name
                        else if (xmlFile.Name.ToString() == "ONERN")
                        {
                            fullName = xmlFile.Value.ToString();
                        }
                    }

                }
                //Used to count the pen strokes on the page
                else if (xmlFile.Name.ToString() == "PEN")
                {
                    //Only count user pen strokes
                    if (xmlFile.GetAttribute("UT").ToString() == "0")
                    {
                        //Stroke taken
                        myStrokes.Add(xmlFile.GetAttribute("UID").ToString());
                    }
                }
                else if (xmlFile.Name.ToString() == "DEOB")
                {
                    //Stroke removed
                    if (xmlFile.GetAttribute("UT") == "0")
                    {
                        deleteStrokes(myStrokes, xmlFile.ReadSubtree());
                    }
                }
            }
            //Net number of strokes on page
            strokes = myStrokes.Count;
        }

        //Used to delete strokes from the list of strokes
        private void deleteStrokes(List<string> myStrokes, XmlReader xmlFile)
        {
            while (xmlFile.Read())
            {
                if (xmlFile.Name.ToString() == "EDDE")
                {
                    myStrokes.Remove(xmlFile.GetAttribute("OBJID").ToString());
                }
            }
        }

        public void setFinished(String st)
        {
            finished = st;
        }

        public int getPageNumber()
        {
            return pageNumber;
        }

        public string getUserName()
        {
            return userName;
        }

        public string getFullName()
        {
            return fullName;
        }

        public int getStrokeCount()
        {
            return strokes;
        }

        public String getFinished()
        {
            return finished;
        }

        //Used to get the information form the page for display purposes.
        public String[] getData()
        {

            string[] data = { pageNumber.ToString(), userName, fullName, strokes.ToString(), finished };
            return data;
        }

        public object[] getRowData()
        {
            object[] data = { pageNumber, userName, fullName, strokes, finished };
            return data;
        }

        //Used for testing
        public override string ToString()
        {
            //return base.ToString();
            return pageNumber.ToString() + ", " + userName + ", " + fullName + ", " + strokes.ToString() + ", " + finished;
        }
    }
}
