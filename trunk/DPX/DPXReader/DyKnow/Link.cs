// <copyright file="Link.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The link object.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The link object.
    /// </summary>
    [XmlRoot("LINK")]
    public class Link
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
        /// The path value.
        /// </summary>
        private string path;

        /// <summary>
        /// The alias value.
        /// </summary>
        private string alias;

        /// <summary>
        /// The usset value.
        /// </summary>
        private bool usett;

        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        public Link()
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
        /// Gets or sets the PATH.
        /// </summary>
        /// <value>The PATH value.</value>
        [XmlElement("PATH")]
        public string PATH
        {
            get { return this.path; }
            set { this.path = value; }
        }

        /// <summary>
        /// Gets or sets the ALIAS.
        /// </summary>
        /// <value>The ALIAS value.</value>
        [XmlElement("ALIAS")]
        public string ALIAS
        {
            get { return this.alias; }
            set { this.alias = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Link"/> is USETT.
        /// </summary>
        /// <value><c>true</c> if USETT; otherwise, <c>false</c>.</value>
        [XmlElement("USETT")]
        public bool USETT
        {
            get { return this.usett; }
            set { this.usett = value; }
        }
    }
}
