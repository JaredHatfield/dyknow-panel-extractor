// <copyright file="Epoll.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The representation of a poll.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The representation of a poll.
    /// </summary>
    public class Epoll
    {
        /// <summary>
        /// The cr value.
        /// </summary>
        private string cr;

        /// <summary>
        /// The ut value.
        /// </summary>
        private int ut;

        /// <summary>
        /// The tn value.
        /// </summary>
        private int tn;

        /// <summary>
        /// The sp value.
        /// </summary>
        private string sp;

        /// <summary>
        /// The ep value.
        /// </summary>
        private string ep;

        /// <summary>
        /// The pw value.
        /// </summary>
        private int pw;

        /// <summary>
        /// The ph value.
        /// </summary>
        private int ph;

        /// <summary>
        /// The uid value.
        /// </summary>
        private Guid uid;

        /// <summary>
        /// The ispri value;
        /// </summary>
        private string ispri;

        /// <summary>
        /// The q value.
        /// </summary>
        private string q;

        /// <summary>
        /// The an value.
        /// </summary>
        private string an;

        /// <summary>
        /// The alist value.
        /// </summary>
        private ArrayList alist;

        /// <summary>
        /// The wid value.
        /// </summary>
        private int wid;

        /// <summary>
        /// Initializes a new instance of the <see cref="Epoll"/> class.
        /// </summary>
        public Epoll()
        {
        }

        /// <summary>
        /// Gets or sets the CR.
        /// </summary>
        /// <value>The CR value.</value>
        [XmlAttribute("CR")]
        public string CR
        {
            get { return this.cr; }
            set { this.cr = value; }
        }

        /// <summary>
        /// Gets or sets the UT.
        /// </summary>
        /// <value>The UT value.</value>
        [XmlAttribute("UT")]
        public int UT
        {
            get { return this.ut; }
            set { this.ut = value; }
        }

        /// <summary>
        /// Gets or sets the TN.
        /// </summary>
        /// <value>The TN value.</value>
        [XmlAttribute("TN")]
        public int TN
        {
            get { return this.tn; }
            set { this.tn = value; }
        }

        /// <summary>
        /// Gets or sets the SP.
        /// </summary>
        /// <value>The SP value.</value>
        [XmlAttribute("SP")]
        public string SP
        {
            get { return this.sp; }
            set { this.sp = value; }
        }

        /// <summary>
        /// Gets or sets the EP.
        /// </summary>
        /// <value>The EP value.</value>
        [XmlAttribute("EP")]
        public string EP
        {
            get { return this.ep; }
            set { this.ep = value; }
        }

        /// <summary>
        /// Gets or sets the PW.
        /// </summary>
        /// <value>The PW value.</value>
        [XmlAttribute("PW")]
        public int PW
        {
            get { return this.pw; }
            set { this.pw = value; }
        }

        /// <summary>
        /// Gets or sets the PH.
        /// </summary>
        /// <value>The PH value.</value>
        [XmlAttribute("PH")]
        public int PH
        {
            get { return this.ph; }
            set { this.ph = value; }
        }

        /// <summary>
        /// Gets or sets the UID.
        /// </summary>
        /// <value>The UID value.</value>
        [XmlAttribute("UID")]
        public Guid UID
        {
            get { return this.uid; }
            set { this.uid = value; }
        }

        /// <summary>
        /// Gets or sets the ISPRI.
        /// </summary>
        /// <value>The ISPRI value.</value>
        [XmlAttribute("ISPRI")]
        public string ISPRI
        {
            get { return this.ispri; }
            set { this.ispri = value; }
        }

        /// <summary>
        /// Gets or sets the Q.
        /// </summary>
        /// <value>The Q value.</value>
        [XmlAttribute("Q")]
        public string Q
        {
            get { return this.q; }
            set { this.q = value; }
        }

        /// <summary>
        /// Gets or sets the AN.
        /// </summary>
        /// <value>The AN value.</value>
        [XmlAttribute("AN")]
        public string AN
        {
            get { return this.an; }
            set { this.an = value; }
        }

        /// <summary>
        /// Gets or sets the ALIST.
        /// </summary>
        /// <value>The ALIST list.</value>
        [XmlArray("ALIST")]
        [XmlArrayItem("A", typeof(Answer), IsNullable = true)]
        public ArrayList ALIST
        {
            get { return this.alist; }
            set { this.alist = value; }
        }

        /// <summary>
        /// Gets or sets the WID.
        /// </summary>
        /// <value>The WID value.</value>
        [XmlElement("WID")]
        public int WID
        {
            get { return this.wid; }
            set { this.wid = value; }
        }
    }
}
