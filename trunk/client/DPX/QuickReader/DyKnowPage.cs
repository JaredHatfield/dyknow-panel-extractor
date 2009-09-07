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
        private List<DyKnowPenStroke> pens;


        public int PageNumber
        {
            get { return pageNumber; }
        }
        public String UserName
        {
            get { return userName; }
        }
        public String FullName
        {
            get { return fullName; }
        }
        public int StrokeCount
        {
            get { return strokes; }
        }
        public String Finished
        {
            get { return finished; }
        }

        //Constructor accepts the XML sub tree
        public DyKnowPage(XmlReader xmlFile, int pageNum)
        {
            //Set some default values if the xml parsing doesn't workout
            pageNumber = pageNum;
            userName = "";
            fullName = "";
            strokes = 0;
            finished = "";
            pens = new List<DyKnowPenStroke>();

            List<String> myStrokes = new List<string>();

            while (xmlFile.Read())
            {
                //Used to identify the user name and full name
                if (xmlFile.Name.ToString() == "PAGE")
                {
                    //xmlFile
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
                    DyKnowPenStroke dps = new DyKnowPenStroke(Int32.Parse(xmlFile.GetAttribute("UT").ToString()),
                        Int32.Parse(xmlFile.GetAttribute("PW").ToString()),
                        Int32.Parse(xmlFile.GetAttribute("PH").ToString()),
                        xmlFile.GetAttribute("UID").ToString(),
                        xmlFile.GetAttribute("DATA").ToString());
                    pens.Add(dps);
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


        public long getStrokeDistance()
        {
            long length = 0;
            for (int i = 0; i < pens.Count; i++)
            {
                if (pens[i].UT == 0)
                {
                    if (!pens[i].DELETED)
                    {
                        length += pens[i].DATA.Length;
                    }
                }
            }
            return length;
        }



        public int getDeletedStrokeCount()
        {
            int total = 0;
            for (int i = 0; i < pens.Count; i++)
            {
                if (pens[i].DELETED)
                {
                    total++;
                }
            }
            return total;
        }


        //Used to delete strokes from the list of strokes
        private void deleteStrokes(List<string> myStrokes, XmlReader xmlFile)
        {
            while (xmlFile.Read())
            {
                if (xmlFile.Name.ToString() == "EDDE")
                {
                    String objid = xmlFile.GetAttribute("OBJID").ToString();
                    myStrokes.Remove(objid);

                    for (int i = 0; i < pens.Count; i++)
                    {
                        if (objid.Equals(pens[i].UID))
                        {
                            pens[i].deleteStroke();
                            break;
                        }
                    }
                }
            }
        }

        public void setFinished(String st)
        {
            finished = st;
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
            return pageNumber.ToString() + ", " + userName + ", " + fullName + ", " +
                strokes.ToString() + ", " + finished + ", " + getDeletedStrokeCount() + ", " + getStrokeDistance();
        }
    }
}
