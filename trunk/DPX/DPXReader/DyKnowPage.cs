// <copyright file="DyKnowPage.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>A representation of a DyKnow panel.</summary>
namespace QuickReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// A representation of a DyKnow panel.
    /// </summary>
    public class DyKnowPage
    {
        /// <summary>
        /// The panel number this represents in a file.
        /// </summary>
        private int pageNumber;

        /// <summary>
        /// The user name.
        /// </summary>
        private string userName;

        /// <summary>
        /// The full name.
        /// </summary>
        private string fullName;

        /// <summary>
        /// The number of strokes on the panel.
        /// </summary>
        private int strokes;

        /// <summary>
        /// The analysis of the panel.
        /// </summary>
        private string finished;

        /// <summary>
        /// The collection of pen strokes on the panel.
        /// </summary>
        private List<DyKnowPenStroke> pens;

        /// <summary>
        /// The collection of images on the panel.
        /// </summary>
        private List<DyKnowImage> images;

        /// <summary>
        /// Initializes a new instance of the <see cref="DyKnowPage"/> class.
        /// </summary>
        /// <param name="xmlFile">The XML file.</param>
        /// <param name="pageNum">The page num.</param>
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
        /// Gets the page number.
        /// </summary>
        /// <value>The page number.</value>
        public int PageNumber
        {
            get { return this.pageNumber; }
        }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get { return this.userName; }
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName
        {
            get { return this.fullName; }
        }

        /// <summary>
        /// Gets the net stroke count.
        /// </summary>
        /// <value>The net stroke count.</value>
        public int NetStrokeCount
        {
            get { return this.strokes; }
        }

        /// <summary>
        /// Gets the analysis of the panel.
        /// </summary>
        /// <value>The analysis of the panel.</value>
        public string Finished
        {
            get { return this.finished; }
        }

        /// <summary>
        /// Gets the net stroke distance.
        /// </summary>
        /// <value>The net stroke distance.</value>
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
        /// Gets the deleted stroke count.
        /// </summary>
        /// <value>The deleted stroke count.</value>
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
        /// Gets the deleted stroke distance.
        /// </summary>
        /// <value>The deleted stroke distance.</value>
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
        /// Gets the total stroke count.
        /// </summary>
        /// <value>The total stroke count.</value>
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
        /// Gets the total stroke distance.
        /// </summary>
        /// <value>The total stroke distance.</value>
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
        /// Gets a value indicating whether the panel is blank.
        /// </summary>
        /// <value><c>True</c> if the panel is blank; otherwise, <c>false</c>.</value>
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
        /// Gets the collection of pen strokes.
        /// </summary>
        /// <value>The collection of pens strokes.</value>
        public List<DyKnowPenStroke> Pens
        {
            get { return this.pens; }
        }

        /// <summary>
        /// Gets the collection of images.
        /// </summary>
        /// <value>The collection of images.</value>
        public List<DyKnowImage> Images
        {
            get { return this.images; }
        }

        /// <summary>
        /// Sets the finished.
        /// </summary>
        /// <param name="st">The st value.</param>
        public void SetFinished(string st)
        {
            this.finished = st;
        }

        /// <summary>
        /// Used to get the information form the page for display purposes.
        /// </summary>
        /// <returns>A string representation of the data on the page.</returns>
        public string[] GetData()
        {
            string[] data = { this.pageNumber.ToString(), this.userName, this.fullName, this.strokes.ToString(), this.finished };
            return data;
        }

        /// <summary>
        /// Gets the row data.
        /// </summary>
        /// <returns>The object array representation of the data.</returns>
        public object[] GetRowData()
        {
            object[] data = { this.pageNumber, this.userName, this.fullName, this.strokes, this.finished };
            return data;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.pageNumber.ToString() + ", " + this.userName + ", " + this.fullName + ", " +
                this.strokes.ToString() + ", " + this.finished + ", " + this.DeletedStrokeCount + ", " + this.NetStrokeDistance;
        }

        /// <summary>
        /// Used to delete strokes from the list of strokes.
        /// </summary>
        /// <param name="myStrokes">My strokes.</param>
        /// <param name="xmlFile">The XML file.</param>
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
