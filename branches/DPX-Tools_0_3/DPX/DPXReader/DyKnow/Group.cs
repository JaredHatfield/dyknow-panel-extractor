// <copyright file="Group.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The group information for the page data.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The group information for the page data.
    /// </summary>
    [XmlRoot("GRP")]
    public class Group
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
        /// The gtyp value.
        /// </summary>
        private string gtyp;

        /// <summary>
        /// Initializes a new instance of the <see cref="Group"/> class.
        /// </summary>
        public Group()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Group"/> class.
        /// </summary>
        /// <param name="ut">The ut value.</param>
        /// <param name="pw">The pw value.</param>
        /// <param name="ph">The ph value.</param>
        /// <param name="gtyp">The gtyp value.</param>
        public Group(int ut, int pw, int ph, string gtyp)
        {
            this.ut = ut;
            this.pw = pw;
            this.ph = ph;
            this.gtyp = gtyp;
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
        /// Gets or sets the GTYP.
        /// </summary>
        /// <value>The GTYP value.</value>
        [XmlAttribute("GTYP")]
        public string GTYP
        {
            get { return this.gtyp; }
            set { this.gtyp = value; }
        }
    }
}
