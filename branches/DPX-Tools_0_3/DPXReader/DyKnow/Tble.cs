// <copyright file="Tble.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The representation of a tble.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The representation of a tble.
    /// </summary>
    public class Tble
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
        /// The nrow value.
        /// </summary>
        private int nrow;

        /// <summary>
        /// The ncol value.
        /// </summary>
        private int ncol;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tble"/> class.
        /// </summary>
        public Tble()
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
        /// Gets or sets the NROW.
        /// </summary>
        /// <value>The NROW value.</value>
        [XmlAttribute("NROW")]
        public int NROW
        {
            get { return this.nrow; }
            set { this.nrow = value; }
        }

        /// <summary>
        /// Gets or sets the NCOL.
        /// </summary>
        /// <value>The NCOL value.</value>
        [XmlAttribute("NCOL")]
        public int NCOL
        {
            get { return this.ncol; }
            set { this.ncol = value; }
        }
    }
}
