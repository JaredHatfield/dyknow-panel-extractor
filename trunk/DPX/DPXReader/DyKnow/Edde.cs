// <copyright file="Edde.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The edde object.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The edde object.
    /// </summary>
    [XmlRoot("EDDE")]
    public class Edde
    {
        /// <summary>
        /// The sti value.
        /// </summary>
        private int sti;

        /// <summary>
        /// The objid value.
        /// </summary>
        private Guid objid;

        /// <summary>
        /// The rtf value.
        /// </summary>
        private string rtf;

        /// <summary>
        /// The vrsn value;
        /// </summary>
        private string vrsn;

        /// <summary>
        /// The xaml value.
        /// </summary>
        private string xaml;

        /// <summary>
        /// Initializes a new instance of the <see cref="Edde"/> class.
        /// </summary>
        public Edde()
        {
            this.vrsn = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Edde"/> class.
        /// </summary>
        /// <param name="sti">The sti value.</param>
        /// <param name="objid">The objid value.</param>
        public Edde(int sti, Guid objid)
        {
            this.sti = sti;
            this.objid = objid;
        }

        /// <summary>
        /// Gets or sets the STI.
        /// </summary>
        /// <value>The STI value.</value>
        [System.ComponentModel.DefaultValue(0)]
        [XmlAttribute("STI")]
        public int STI
        {
            get { return this.sti; }
            set { this.sti = value; }
        }

        /// <summary>
        /// Gets or sets the OBJID.
        /// </summary>
        /// <value>The OBJID value.</value>
        [XmlAttribute("OBJID")]
        public Guid OBJID
        {
            get { return this.objid; }
            set { this.objid = value; }
        }

        /// <summary>
        /// Gets or sets the RTF.
        /// </summary>
        /// <value>The RTF value.</value>
        [XmlAttribute("RTF")]
        public string RTF
        {
            get { return this.rtf; }
            set { this.rtf = value; }
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
        /// Gets or sets the XAML.
        /// </summary>
        /// <value>The XAML value.</value>
        [XmlElement("XAML")]
        public string XAML
        {
            get { return this.xaml; }
            set { this.xaml = value; }
        }
    }
}
