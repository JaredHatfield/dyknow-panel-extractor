// <copyright file="Rtext.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The rtext object.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The rtext object.
    /// </summary>
    [XmlRoot("RTEXT")]
    public class Rtext
    {
        /// <summary>
        /// The ut value.
        /// </summary>
        private int ut;

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
        /// The vrsn value.
        /// </summary>
        private string vrsn;

        /// <summary>
        /// The ani value.
        /// </summary>
        private string ani;

        /// <summary>
        /// The rtf value.
        /// </summary>
        private string rtf;

        /// <summary>
        /// The xaml value.
        /// </summary>
        private string xaml;

        /// <summary>
        /// The wid value.
        /// </summary>
        private int wid;

        /// <summary>
        /// The hei value.
        /// </summary>
        private int hei;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rtext"/> class.
        /// </summary>
        public Rtext()
        {
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
        /// Gets or sets the VRSN.
        /// </summary>
        /// <value>The VRSN value.</value>
        [XmlAttribute("VRSN")]
        public string VRSN
        {
            get { return this.vrsn; }
            set { this.vrsn = value; }
        }

        /// <summary>
        /// Gets or sets the ANI.
        /// </summary>
        /// <value>The ANI value.</value>
        [XmlAttribute("ANI")]
        public string ANI
        {
            get { return this.ani; }
            set { this.ani = value; }
        }

        /// <summary>
        /// Gets or sets the RTF.
        /// </summary>
        /// <value>The RTF value.</value>
        [XmlElement("RTF")]
        public string RTF
        {
            get { return this.rtf; }
            set { this.rtf = value; }
        }

        /// <summary>
        /// Gets or sets the XAML.
        /// </summary>
        /// <value>The XAML value.</value>
        [XmlElement("XAML")]
        public string XAML
        {
            get { return this.xaml; }
            set { this.xaml = value; }
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

        /// <summary>
        /// Gets or sets the HEI.
        /// </summary>
        /// <value>The HEI value.</value>
        [XmlElement("HEI")]
        public int HEI
        {
            get { return this.hei; }
            set { this.hei = value; }
        }
    }
}
