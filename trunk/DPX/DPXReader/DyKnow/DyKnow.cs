// <copyright file="DyKnow.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>A representation of a DyKnow file.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// A representation of a DyKnow file.
    /// </summary>
    [XmlRoot("DYKNOW_NB50")]
    public class DyKnow
    {
        /// <summary>
        /// The imgs list.
        /// </summary>
        private ArrayList imgs;

        /// <summary>
        /// The imgd list.
        /// </summary>
        private ArrayList imgd;

        /// <summary>
        /// Initializes a new instance of the <see cref="DyKnow"/> class.
        /// </summary>
        public DyKnow()
        {
            this.imgs = new ArrayList();
            this.imgd = new ArrayList();
            this.imgs.Add("TEST");
            this.imgd.Add(Guid.NewGuid());
        }

        /// <summary>
        /// Gets or sets the IMGS.
        /// </summary>
        /// <value>The IMGS list.</value>
        [XmlArray("IMGS")]
        [XmlArrayItem("IMG", typeof(string))]
        public ArrayList IMGS
        {
            get { return this.imgs; }
            set { this.imgs = value; }
        }

        /// <summary>
        /// Gets or sets the IMGD.
        /// </summary>
        /// <value>The IMGD list.</value>
        [XmlArray("IMGD")]
        [XmlArrayItem("ID", typeof(Guid))]
        public ArrayList IMGD
        {
            get { return this.imgd; }
            set { this.imgd = value; }
        }
    }
}
