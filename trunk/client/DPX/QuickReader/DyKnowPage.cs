// <copyright file="DyKnowPage.cs" company="DPX">
// GNU General Public License v3
// </copyright>
// <summary>DyKnow Page object.</summary>
namespace QuickReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// 
    /// </summary>
    public class DyKnowPage
    {
        /// <summary>
        /// 
        /// </summary>
        private int pageNumber;

        /// <summary>
        /// 
        /// </summary>
        private string userName;

        /// <summary>
        /// 
        /// </summary>
        private string fullName;

        /// <summary>
        /// 
        /// </summary>
        private int strokes;

        /// <summary>
        /// 
        /// </summary>
        private string finished;

        /// <summary>
        /// 
        /// </summary>
        private List<DyKnowPenStroke> pens;

        /// <summary>
        /// 
        /// </summary>
        private List<DyKnowImage> images;

        /// <summary>
        /// Constructor accepts the XML sub tree
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <param name="pageNum"></param>
        public DyKnowPage(XmlReader xmlFile, int pageNum)
        {
            // Set some default values if the xml parsing doesn't workout
            this.pageNumber = pageNum;
            this.userName = string.Empty;
            this.fullName = string.Empty;
            this.strokes = 0;
            this.finished = string.Empty;
            this.pens = new List<DyKnowPenStroke>();
            this.images = new List<DyKnowImage>();
            List<string> myStrokes = new List<string>();

            while (xmlFile.Read())
            {
                // Used to identify the user name and full name
                if (xmlFile.Name.ToString() == "PAGE")
                {
                    // xmlFile
                    while (xmlFile.MoveToNextAttribute())
                    {
                        if (xmlFile.Name.ToString() == "ONER")
                        {
                            // User Name
                            this.userName = xmlFile.Value.ToString();
                        }
                        else if (xmlFile.Name.ToString() == "ONERN")
                        {
                            // Full Name
                            this.fullName = xmlFile.Value.ToString();
                        }
                    }
                }
                else if (xmlFile.Name.ToString() == "PEN")
                {
                    // Used to count the pen strokes on the page
                    // Only count user pen strokes
                    if (xmlFile.GetAttribute("UT").ToString() == "0")
                    {
                        // Stroke taken
                        myStrokes.Add(xmlFile.GetAttribute("UID").ToString());
                    }

                    DyKnowPenStroke dps = new DyKnowPenStroke(
                        Int32.Parse(xmlFile.GetAttribute("UT").ToString()),
                        Int32.Parse(xmlFile.GetAttribute("PW").ToString()),
                        Int32.Parse(xmlFile.GetAttribute("PH").ToString()),
                        xmlFile.GetAttribute("UID").ToString(),
                        xmlFile.GetAttribute("DATA").ToString());
                    this.pens.Add(dps);
                }
                else if (xmlFile.Name.ToString() == "DEOB")
                {
                    // Stroke removed (for both the moderator and the user)
                    this.DeleteStrokes(myStrokes, xmlFile.ReadSubtree());
                }
                else if (xmlFile.Name.ToString() == "IMG" && xmlFile.NodeType == XmlNodeType.Element)
                {
                    DyKnowImage dki = new DyKnowImage(
                        Int32.Parse(xmlFile.GetAttribute("UT")),
                        xmlFile.GetAttribute("SP"), 
                        Int32.Parse(xmlFile.GetAttribute("PW")),
                        Int32.Parse(xmlFile.GetAttribute("PH")), 
                        xmlFile.GetAttribute("UID"),
                        xmlFile.GetAttribute("ID"), 
                        Int32.Parse(xmlFile.GetAttribute("WID")),
                        Int32.Parse(xmlFile.GetAttribute("HEI")));
                    this.images.Add(dki);
                }
            }

            // Net number of strokes on page
            this.strokes = myStrokes.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PageNumber
        {
            get { return this.pageNumber; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            get { return this.userName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FullName
        {
            get { return this.fullName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int NetStrokeCount
        {
            get { return this.strokes; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Finished
        {
            get { return this.finished; }
        }

        /// <summary>
        /// 
        /// </summary>
        public long NetStrokeDistance
        {
            get
            {
                long length = 0;
                for (int i = 0; i < this.pens.Count; i++)
                {
                    if (this.pens[i].UT == 0)
                    {
                        if (!this.pens[i].DELETED)
                        {
                            length += this.pens[i].DATA.Length;
                        }
                    }
                }

                return length;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DeletedStrokeCount
        {
            get
            {
                int total = 0;
                for (int i = 0; i < this.pens.Count; i++)
                {
                    if (this.pens[i].DELETED)
                    {
                        total++;
                    }
                }

                return total;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long DeletedStrokeDistance
        {
            get
            {
                long total = 0;
                for (int i = 0; i < this.pens.Count; i++)
                {
                    if (this.pens[i].DELETED)
                    {
                        total += this.pens[i].DATA.Length;
                    }
                }

                return total;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TotalStrokeCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < this.pens.Count; i++)
                {
                    if (this.pens[i].UT == 0)
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long TotalStrokeDistance
        {
            get
            {
                long length = 0;
                for (int i = 0; i < this.pens.Count; i++)
                {
                    if (this.pens[i].UT == 0)
                    {
                        length += this.pens[i].DATA.Length;
                    }
                }

                return length;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsBlank
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

        /// <summary>
        /// 
        /// </summary>
        public List<DyKnowPenStroke> Pens
        {
            get { return this.pens; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<DyKnowImage> Images
        {
            get { return this.images; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="st"></param>
        public void SetFinished(string st)
        {
            this.finished = st;
        }

        /// <summary>
        /// Used to get the information form the page for display purposes.
        /// </summary>
        /// <returns></returns>
        public string[] GetData()
        {
            string[] data = { this.pageNumber.ToString(), this.userName, this.fullName, this.strokes.ToString(), this.finished };
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object[] GetRowData()
        {
            object[] data = { this.pageNumber, this.userName, this.fullName, this.strokes, this.finished };
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.pageNumber.ToString() + ", " + this.userName + ", " + this.fullName + ", " +
                this.strokes.ToString() + ", " + this.finished + ", " + this.DeletedStrokeCount + ", " + this.NetStrokeDistance;
        }

        /// <summary>
        /// Used to delete strokes from the list of strokes
        /// </summary>
        /// <param name="myStrokes"></param>
        /// <param name="xmlFile"></param>
        private void DeleteStrokes(List<string> myStrokes, XmlReader xmlFile)
        {
            while (xmlFile.Read())
            {
                if (xmlFile.Name.ToString() == "EDDE")
                {
                    string objid = xmlFile.GetAttribute("OBJID").ToString();
                    myStrokes.Remove(objid);

                    for (int i = 0; i < this.pens.Count; i++)
                    {
                        if (objid.Equals(this.pens[i].UID))
                        {
                            this.pens[i].DeleteStroke();
                            break;
                        }
                    }
                }
            }
        }
    }
}
