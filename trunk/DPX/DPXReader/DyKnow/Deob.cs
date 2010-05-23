// <copyright file="Deob.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The deob object.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The deob object.
    /// </summary>
    [XmlRoot("DEOB")]
    public class Deob
    {
        /// <summary>
        /// The ut value.
        /// </summary>
        private int ut;

        /// <summary>
        /// The uid value.
        /// </summary>
        private Guid uid;

        /// <summary>
        /// The ig value.
        /// </summary>
        private bool ig;

        /// <summary>
        /// The deli list.
        /// </summary>
        private ArrayList deli;

        /// <summary>
        /// Initializes a new instance of the <see cref="Deob"/> class.
        /// </summary>
        public Deob()
        {
            this.deli = new ArrayList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Deob"/> class.
        /// </summary>
        /// <param name="ut">The ut value.</param>
        /// <param name="uid">The uid value.</param>
        /// <param name="ig">if set to <c>true</c> [ig].</param>
        public Deob(int ut, Guid uid, bool ig)
        {
            this.deli = new ArrayList();
            this.ut = ut;
            this.uid = uid;
            this.ig = ig;
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
        /// Gets or sets a value indicating whether this <see cref="Deob"/> is IG.
        /// </summary>
        /// <value><c>true</c> if IG; otherwise, <c>false</c>.</value>
        [XmlAttribute("IG")]
        public bool IG
        {
            get { return this.ig; }
            set { this.ig = value; }
        }

        /// <summary>
        /// Gets or sets the DELI.
        /// </summary>
        /// <value>The DELI value.</value>
        [XmlArray("DELI")]
        [XmlArrayItem("EDDE", typeof(Edde), IsNullable = true)]
        public ArrayList DELI
        {
            get { return this.deli; }
            set { this.deli = value; }
        }

        /// <summary>
        /// Determines whether the object was deleted.
        /// </summary>
        /// <param name="id">The object identifier.</param>
        /// <returns><c>true</c> if [contains] the object was deleted; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid id)
        {
            for (int i = 0; i < this.deli.Count; i++)
            {
                Edde e = this.deli[i] as Edde;
                if (e.OBJID.Equals(id))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
