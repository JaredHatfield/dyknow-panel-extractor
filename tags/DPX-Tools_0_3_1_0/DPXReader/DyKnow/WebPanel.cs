// <copyright file="WebPanel.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The web panel object.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The web panel object.
    /// </summary>
    [XmlRoot("WEBPNL")]
    public class WebPanel
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
        /// The pos value.
        /// </summary>
        private int pos;

        /// <summary>
        /// The cap value.
        /// </summary>
        private bool cap;

        /// <summary>
        /// The url value.
        /// </summary>
        private string url;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebPanel"/> class.
        /// </summary>
        public WebPanel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebPanel"/> class.
        /// </summary>
        /// <param name="ut">The ut value.</param>
        /// <param name="pw">The pw value.</param>
        /// <param name="ph">The ph value.</param>
        /// <param name="uid">The uid value.</param>
        /// <param name="pos">The pos value.</param>
        /// <param name="cap">if set to <c>true</c> [cap].</param>
        /// <param name="url">The URL value.</param>
        public WebPanel(int ut, int pw, int ph, Guid uid, int pos, bool cap, string url)
        {
            this.ut = ut;
            this.pw = pw;
            this.ph = ph;
            this.uid = uid;
            this.pos = pos;
            this.cap = cap;
            this.url = url;
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
        /// Gets or sets the POS.
        /// </summary>
        /// <value>The POS value.</value>
        [XmlAttribute("POS")]
        public int POS
        {
            get { return this.pos; }
            set { this.pos = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WebPanel"/> is CAP.
        /// </summary>
        /// <value><c>true</c> if CAP; otherwise, <c>false</c>.</value>
        [XmlAttribute("CAP")]
        public bool CAP
        {
            get { return this.cap; }
            set { this.cap = value; }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL value.</value>
        [XmlAttribute("URL")]
        public string URL
        {
            get { return this.url; }
            set { this.url = value; }
        }
    }
}
