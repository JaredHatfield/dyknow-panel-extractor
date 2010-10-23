// <copyright file="Animation.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The animation object.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The animation object.
    /// </summary>
    [XmlRoot("A")]
    public class Animation
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
        /// The tb value.
        /// </summary>
        private Guid tb;

        /// <summary>
        /// The typ value.
        /// </summary>
        private int typ;

        /// <summary>
        /// The par value.
        /// </summary>
        private int par;

        /// <summary>
        /// The lvl value.
        /// </summary>
        private int lvl;

        /// <summary>
        /// The xaml value.
        /// </summary>
        private string xaml;

        /// <summary>
        /// Initializes a new instance of the <see cref="Animation"/> class.
        /// </summary>
        public Animation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animation"/> class.
        /// </summary>
        /// <param name="ut">The ut value.</param>
        /// <param name="pw">The pw value.</param>
        /// <param name="ph">The ph value.</param>
        /// <param name="uid">The uid value.</param>
        /// <param name="tb">The tb value.</param>
        /// <param name="typ">The typ value.</param>
        /// <param name="par">The par value.</param>
        /// <param name="lvl">The LVL value.</param>
        public Animation(int ut, int pw, int ph, Guid uid, Guid tb, int typ, int par, int lvl)
        {
            this.ut = ut;
            this.pw = pw;
            this.ph = ph;
            this.uid = uid;
            this.tb = tb;
            this.typ = typ;
            this.par = par;
            this.lvl = lvl;
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
        /// Gets or sets the TB.
        /// </summary>
        /// <value>The TB value.</value>
        [XmlAttribute("TB")]
        public Guid TB
        {
            get { return this.tb; }
            set { this.tb = value; }
        }

        /// <summary>
        /// Gets or sets the TYP.
        /// </summary>
        /// <value>The TYP value.</value>
        [XmlAttribute("TYP")]
        public int TYP
        {
            get { return this.typ; }
            set { this.typ = value; }
        }

        /// <summary>
        /// Gets or sets the PAR.
        /// </summary>
        /// <value>The PAR value.</value>
        [XmlAttribute("PAR")]
        public int PAR
        {
            get { return this.par; }
            set { this.par = value; }
        }

        /// <summary>
        /// Gets or sets the LVL.
        /// </summary>
        /// <value>The LVL value.</value>
        [XmlAttribute("LVL")]
        public int LVL
        {
            get { return this.lvl; }
            set { this.lvl = value; }
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
