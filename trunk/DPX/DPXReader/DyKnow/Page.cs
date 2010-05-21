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
        /// The animlist list of objects.
        /// </summary>
        private ArrayList animlist;

        /// <summary>
        /// The txtmodecontentsmod value.
        /// </summary>
        private string txtmodecontentsmod;

        /// <summary>
        /// The txtmodecontentspart value.
        /// </summary>
        private string txtmodecontentspart;

        /// <summary>
        /// The txtmodemodxaml value.
        /// </summary>
        private string txtmodemodxaml;

        /// <summary>
        /// The txtmodepartxaml value.
        /// </summary>
        private string txtmodepartxaml;

        /// <summary>
        /// The olst list of objects.
        /// </summary>
        private ArrayList olst;

        /// <summary>
        /// The mrgn object.
        /// </summary>
        private Mrgn mrgn;

        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        public Page()
        {
            this.animlist = new ArrayList();
            this.txtmodecontentsmod = string.Empty;
            this.txtmodecontentspart = string.Empty;
            this.txtmodemodxaml = string.Empty;
            this.txtmodepartxaml = string.Empty;
            this.olst = new ArrayList();
            this.mrgn = new Mrgn();
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
        /// Gets or sets the ANIMLIST.
        /// </summary>
        /// <value>The ANIMLIST value.</value>
        [XmlArray("ANIMLIST")]
        [XmlArrayItem("A", typeof(Animation), IsNullable = true)]
        public ArrayList ANIMLIST
        {
            get { return this.animlist; }
            set { this.animlist = value; }
        }

        /// <summary>
        /// Gets or sets the TXTMODECONTENTSMOD.
        /// </summary>
        /// <value>The TXTMODECONTENTSMOD value.</value>
        [XmlElement("TXTMODECONTENTSMOD")]
        public string TXTMODECONTENTSMOD
        {
            get { return this.txtmodecontentsmod; }
            set { this.txtmodecontentsmod = value; }
        }

        /// <summary>
        /// Gets or sets the TXTMODECONTENTSPART.
        /// </summary>
        /// <value>The TXTMODECONTENTSPART value.</value>
        [XmlElement("TXTMODECONTENTSPART")]
        public string TXTMODECONTENTSPART
        {
            get { return this.txtmodecontentspart; }
            set { this.txtmodecontentspart = value; }
        }

        /// <summary>
        /// Gets or sets the TXTMODEMODXAML.
        /// </summary>
        /// <value>The TXTMODEMODXAML value.</value>
        [XmlElement("TXTMODEMODXAML")]
        public string TXTMODEMODXAML
        {
            get { return this.txtmodemodxaml; }
            set { this.txtmodemodxaml = value; }
        }

        /// <summary>
        /// Gets or sets the TXTMODEPARTXAML.
        /// </summary>
        /// <value>The TXTMODEPARTXAML value.</value>
        [XmlElement("TXTMODEPARTXAML")]
        public string TXTMODEPARTXAML
        {
            get { return this.txtmodepartxaml; }
            set { this.txtmodepartxaml = value; }
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
        [XmlArrayItem("EDOB", typeof(Edob), IsNullable = true)]
        [XmlArrayItem("WEBPNL", typeof(WebPanel), IsNullable = true)]
        [XmlArrayItem("RTEXT", typeof(Rtext), IsNullable = true)]
        [XmlArrayItem("LINK", typeof(Link), IsNullable = true)]
        public ArrayList OLST
        {
            get { return this.olst; }
            set { this.olst = value; }
        }

        /// <summary>
        /// Gets or sets the MRGN.
        /// </summary>
        /// <value>The MRGN value.</value>
        [XmlElement("MRGN")]
        public Mrgn MRGN
        {
            get { return this.mrgn; }
            set { this.mrgn = value; }
        }
    }
}
