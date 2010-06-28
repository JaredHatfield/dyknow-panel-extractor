// <copyright file="Pgnav.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The representation of a pgnav.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The representation of a pgnav.
    /// </summary>
    public class Pgnav
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
        /// The pgguid.
        /// </summary>
        private Guid pgguid;

        /// <summary>
        /// The type value.
        /// </summary>
        private int type;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pgnav"/> class.
        /// </summary>
        public Pgnav()
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
        /// Gets or sets the PGGUID.
        /// </summary>
        /// <value>The PGGUID value.</value>
        [XmlElement("PGGUID")]
        public Guid PGGUID
        {
            get { return this.pgguid; }
            set { this.pgguid = value; }
        }

        /// <summary>
        /// Gets or sets the TYPE.
        /// </summary>
        /// <value>The TYPE value.</value>
        [XmlElement("TYPE")]
        public int TYPE
        {
            get { return this.type; }
            set { this.type = value; }
        }
    }
}
