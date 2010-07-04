// <copyright file="Abox.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The abox object.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The abox object.
    /// </summary>
    [XmlRoot("ABOX")]
    public class Abox
    {
        /// <summary>
        /// The cr value.
        /// </summary>
        private string cr;

        /// <summary>
        /// THe ut value.
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
        /// Initializes a new instance of the <see cref="Abox"/> class.
        /// </summary>
        public Abox()
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
        /// Gets the left position for the box.
        /// </summary>
        /// <value>The left position.</value>
        [XmlIgnore]
        private double CustomPositionLeft
        {
            get
            {
                int start = 0;
                int end = this.sp.IndexOf(':');
                string val = this.sp.Substring(start, end);
                double num = int.Parse(val);
                return num;
            }
        }

        /// <summary>
        /// Gets the top position for the box.
        /// </summary>
        /// <value>The top position.</value>
        [XmlIgnore]
        private double CustomPositionTop
        {
            get
            {
                int start = this.sp.IndexOf(':') + 1;
                string val = this.sp.Substring(start);
                double num = int.Parse(val);
                return num;
            }
        }

        /// <summary>
        /// Gets the left position for the box.
        /// </summary>
        /// <value>The left position.</value>
        [XmlIgnore]
        private double CustomWidth
        {
            get
            {
                int start = 0;
                int end = this.ep.IndexOf(':');
                string val = this.ep.Substring(start, end);
                double num = int.Parse(val);
                return num;
            }
        }

        /// <summary>
        /// Gets the top position for the box.
        /// </summary>
        /// <value>The top position.</value>
        [XmlIgnore]
        private double CustomHeight
        {
            get
            {
                int start = this.ep.IndexOf(':') + 1;
                string val = this.ep.Substring(start);
                double num = int.Parse(val);
                return num;
            }
        }

        /// <summary>
        /// Actuals the top position.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <returns>The adjusted distance from the top.</returns>
        internal double ActualTopPosition(double height)
        {
            return this.CustomPositionTop * height / (double)this.PH;
        }

        /// <summary>
        /// Actuals the left position.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <returns>The adjusted distance from the left.</returns>
        internal double ActualLeftPosition(double width)
        {
            return this.CustomPositionLeft * width / (double)this.PW;
        }

        /// <summary>
        /// Actuals the height.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <returns>The adjusted width of the box.</returns>
        internal double ActualHeight(double height)
        {
            return (this.CustomHeight * height / (double)this.PH) - this.ActualTopPosition(height);
        }

        /// <summary>
        /// Actuals the width.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <returns>The adjusted height of the box.</returns>
        internal double ActualWidth(double width)
        {
            return (this.CustomWidth * width / (double)this.PW) - this.ActualLeftPosition(width);
        }
    }
}
