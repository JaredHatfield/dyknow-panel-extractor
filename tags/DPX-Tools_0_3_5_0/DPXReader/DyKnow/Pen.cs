// <copyright file="Pen.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The representation of a pen stroke.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The representation of a pen stroke.
    /// </summary>
    [XmlRoot("PEN")]
    public class Pen
    {
        /// <summary>
        /// The ut value.
        /// </summary>
        private int ut;

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
        /// The ati value.
        /// </summary>
        private string ati;

        /// <summary>
        /// The dpi value.
        /// </summary>
        private string dpi;

        /// <summary>
        /// The ispri value.
        /// </summary>
        private string ispri;

        /// <summary>
        /// The data value.
        /// </summary>
        private string data;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pen"/> class.
        /// </summary>
        public Pen()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pen"/> class.
        /// </summary>
        /// <param name="ut">The ut value.</param>
        /// <param name="pw">The pw value.</param>
        /// <param name="ph">The ph value.</param>
        /// <param name="uid">The uid value.</param>
        /// <param name="data">The data value.</param>
        public Pen(int ut, int pw, int ph, Guid uid, string data)
        {
            this.ut = ut;
            this.pw = pw;
            this.ph = ph;
            this.uid = uid;
            this.data = data;
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
        /// Gets or sets the ATI.
        /// </summary>
        /// <value>The ATI value.</value>
        [XmlAttribute("ATI")]
        public string ATI
        {
            get { return this.ati; }
            set { this.ati = value; }
        }

        /// <summary>
        /// Gets or sets the DPI.
        /// </summary>
        /// <value>The DPI value.</value>
        [XmlAttribute("DPI")]
        public string DPI
        {
            get { return this.dpi; }
            set { this.dpi = value; }
        }

        /// <summary>
        /// Gets or sets the ISPRI.
        /// </summary>
        /// <value>The ISPRI.</value>
        [XmlAttribute("ISPRI")]
        public string ISPRI
        {
            get { return this.ispri; }
            set { this.ispri = value; }
        }

        /// <summary>
        /// Gets or sets the DATA.
        /// </summary>
        /// <value>The DATA value.</value>
        [XmlAttribute("DATA")]
        public string DATA
        {
            get { return this.data; }
            set { this.data = value; }
        }
    }
}
