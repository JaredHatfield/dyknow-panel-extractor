// <copyright file="Img.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The reference to an image contained on a panel.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The reference to an image contained on a panel.
    /// </summary>
    [XmlRoot("IMG")]
    public class Img
    {
        /// <summary>
        /// The reference width used to transform the image dimensions.
        /// </summary>
        private const int ReferenceWidth = 27100;

        /// <summary>
        /// The reference height used to transform the image dimensions.
        /// </summary>
        private const int ReferenceHeight = 20325;

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
        /// The id value.
        /// </summary>
        private Guid id;

        /// <summary>
        /// The wid value.
        /// </summary>
        private int wid;

        /// <summary>
        /// The hei value.
        /// </summary>
        private int hei;

        /// <summary>
        /// The slidetxt value;
        /// </summary>
        private string slidetxt;

        /// <summary>
        /// Initializes a new instance of the <see cref="Img"/> class.
        /// </summary>
        public Img()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Img"/> class.
        /// </summary>
        /// <param name="ut">The ut value.</param>
        /// <param name="sp">The sp value.</param>
        /// <param name="pw">The pw value.</param>
        /// <param name="ph">The ph value.</param>
        /// <param name="uid">The uid value.</param>
        /// <param name="id">The id value.</param>
        /// <param name="wid">The wid value.</param>
        /// <param name="hei">The hei value.</param>
        public Img(int ut, string sp, int pw, int ph, Guid uid, Guid id, int wid, int hei)
        {
            this.ut = ut;
            this.sp = sp;
            this.pw = pw;
            this.ph = ph;
            this.uid = uid;
            this.id = id;
            this.wid = wid;
            this.hei = hei;
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
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID value.</value>
        [XmlAttribute("ID")]
        public Guid ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// Gets or sets the WID.
        /// </summary>
        /// <value>The WID value.</value>
        [XmlAttribute("WID")]
        public int WID
        {
            get { return this.wid; }
            set { this.wid = value; }
        }

        /// <summary>
        /// Gets or sets the HEI.
        /// </summary>
        /// <value>The HEI value.</value>
        [XmlAttribute("HEI")]
        public int HEI
        {
            get { return this.hei; }
            set { this.hei = value; }
        }

        /// <summary>
        /// Gets or sets the SLIDETXT.
        /// </summary>
        /// <value>The SLIDETXT.</value>
        [XmlElement("SLIDETXT")]
        public string SLIDETXT
        {
            get { return this.slidetxt; }
            set { this.slidetxt = value; }
        }

        // Custom, non serialized attributes

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        /// <value>The width of the image.</value>
        [XmlIgnore]
        internal double CustomImageWidth
        {
            get { return (double)this.wid / (double)Img.ReferenceWidth; }
        }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        /// <value>The height of the image.</value>
        [XmlIgnore]
        internal double CustomImageHeight
        {
            get { return (double)this.hei / (double)Img.ReferenceHeight; }
        }

        /// <summary>
        /// Gets the left position for the image.
        /// </summary>
        /// <value>The left position.</value>
        [XmlIgnore]
        internal double CustomPositionLeft
        {
            get
            {
                int start = 0;
                int end = this.sp.IndexOf(':');
                string val = this.sp.Substring(start, end);
                double num = int.Parse(val);
                return num / Img.ReferenceWidth;
            }
        }

        /// <summary>
        /// Gets the top position for the image.
        /// </summary>
        /// <value>The top position.</value>
        [XmlIgnore]
        internal double CustomPositionTop
        {
            get
            {
                int start = this.sp.IndexOf(':') + 1;
                string val = this.sp.Substring(start);
                double num = int.Parse(val);
                return num / Img.ReferenceHeight;
            }
        }
    }
}
