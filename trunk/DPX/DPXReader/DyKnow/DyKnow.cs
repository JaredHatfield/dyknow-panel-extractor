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
        /// The vrsn value.
        /// </summary>
        private string vrsn;

        /// <summary>
        /// The data list.
        /// </summary>
        private ArrayList data;

        /// <summary>
        /// The imgs list.
        /// </summary>
        private ArrayList imgs;

        /// <summary>
        /// The imgd list.
        /// </summary>
        private ArrayList imgd;

        /// <summary>
        /// The chat list of objects.
        /// </summary>
        private ArrayList chats;

        /// <summary>
        /// Initializes a new instance of the <see cref="DyKnow"/> class.
        /// </summary>
        public DyKnow()
        {
            this.data = new ArrayList();
            this.chats = new ArrayList();

            // this.imgs = new ArrayList();
            // this.imgd = new ArrayList();
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
        /// Gets or sets the DATA.
        /// </summary>
        /// <value>The DATA list.</value>
        [XmlArray("DATA")]
        [XmlArrayItem("PAGE", typeof(Page))]
        public ArrayList DATA
        {
            get { return this.data; }
            set { this.data = value; }
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
            get
            {
                /*
                if (this.imgs != null && this.imgs.Count == 0)
                {
                    return null;
                }
                 */

                return this.imgd;
            }

            set
            {
                this.imgd = value;
            }
        }

        /// <summary>
        /// Gets or sets the CHATS.
        /// </summary>
        /// <value>The CHATS list of objects.</value>
        [XmlArray("CHATS")]
        public ArrayList CHATS
        {
            get { return this.chats; }
            set { this.chats = value; }
        }
    }
}
