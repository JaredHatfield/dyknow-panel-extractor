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
        private List<DyKnowImage> images;


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
        public int NetStrokeCount
        {
            get { return strokes; }
        }
        public String Finished
        {
            get { return finished; }
        }
        public long NetStrokeDistance
        {
            get
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
        }
        public int DeletedStrokeCount
        {
            get
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
        }
        public long DeletedStrokeDistance
        {
            get
            {
                long total = 0;
                for (int i = 0; i < pens.Count; i++)
                {
                    if (pens[i].DELETED)
                    {
                        total += pens[i].DATA.Length;
                    }
                }
                return total;
            }
        }
        public int TotalStrokeCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < pens.Count; i++)
                {
                    if (pens[i].UT == 0)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
        public long TotalStrokeDistance
        {
            get
            {
                long length = 0;
                for (int i = 0; i < pens.Count; i++)
                {
                    if (pens[i].UT == 0)
                    {
                        length += pens[i].DATA.Length;
                    }
                }
                return length;
            }
        }
        public Boolean IsBlank
        {
            get
            {
                if (this.NetStrokeCount == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public List<DyKnowPenStroke> Pens
        {
            get { return pens; }
        }
        public List<DyKnowImage> Images
        {
            get { return images; }
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
            images = new List<DyKnowImage>();

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
                    //Stroke removed (for both the moderator and the user)
                    deleteStrokes(myStrokes, xmlFile.ReadSubtree());
                }
                else if (xmlFile.Name.ToString() == "IMG")
                {
                    DyKnowImage dki = new DyKnowImage(Int32.Parse(xmlFile.GetAttribute("UT")),
                        xmlFile.GetAttribute("SP"), Int32.Parse(xmlFile.GetAttribute("PW")),
                        Int32.Parse(xmlFile.GetAttribute("PH")), xmlFile.GetAttribute("UID"),
                        xmlFile.GetAttribute("ID"), Int32.Parse(xmlFile.GetAttribute("WID")),
                        Int32.Parse(xmlFile.GetAttribute("HEI")));
                    images.Add(dki);
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
                strokes.ToString() + ", " + finished + ", " + this.DeletedStrokeCount + ", " + this.NetStrokeDistance;
        }
    }
}
