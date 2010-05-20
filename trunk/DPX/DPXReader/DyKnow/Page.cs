// <copyright file="Page.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The page that contains all of the panel information.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The page that contains all of the panel information.
    /// </summary>
    [XmlRoot("PAGE")]
    public class Page
    {
        /// <summary>
        /// The page version number.
        /// </summary>
        private string vrsn;

        /// <summary>
        /// The uid value.
        /// </summary>
        private Guid uid;

        /// <summary>
        /// The oner value;
        /// </summary>
        private string oner;

        /// <summary>
        /// The onern value.
        /// </summary>
        private string onern;

        /// <summary>
        /// The olst list of objects.
        /// </summary>
        private ArrayList olst;

        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        public Page()
        {
            this.olst = new ArrayList();
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
        /// Gets or sets the ONER.
        /// </summary>
        /// <value>The ONER value.</value>
        [XmlAttribute("ONER")]
        public string ONER
        {
            get { return this.oner; }
            set { this.oner = value; }
        }

        /// <summary>
        /// Gets or sets the ONERN.
        /// </summary>
        /// <value>The ONERN value.</value>
        [XmlAttribute("ONERN")]
        public string ONERN
        {
            get { return this.onern; }
            set { this.onern = value; }
        }

        /// <summary>
        /// Gets or sets the OLST.
        /// </summary>
        /// <value>The OLST list of objects.</value>
        [XmlArray("OLST")]
        [XmlArrayItem("PEN", typeof(Pen), IsNullable = true)]
        [XmlArrayItem("IMG", typeof(Img), IsNullable = true)]
        [XmlArrayItem("GRP", typeof(Group), IsNullable = true)]
        [XmlArrayItem("DEOB", typeof(Deob), IsNullable = true)]
        public ArrayList OLST
        {
            get { return this.olst; }
            set { this.olst = value; }
        }
    }
}
