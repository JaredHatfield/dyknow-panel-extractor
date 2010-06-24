// <copyright file="Message.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The representation of a chat message.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The representation of a chat message.
    /// </summary>
    [XmlRoot("Message")]
    public class Message
    {
        /// <summary>
        /// The snme value.
        /// </summary>
        private string snme;

        /// <summary>
        /// The sfnm value.
        /// </summary>
        private string sfnm;

        /// <summary>
        /// The mess value.
        /// </summary>
        private string mess;

        /// <summary>
        /// The rid value.
        /// </summary>
        private string rid;

        /// <summary>
        /// The scd value.
        /// </summary>
        private string scd;

        /// <summary>
        /// The sid value.
        /// </summary>
        private Guid sid;

        /// <summary>
        /// The mod value.
        /// </summary>
        private string mod;

        /// <summary>
        /// The ch value.
        /// </summary>
        private int ch;

        /// <summary>
        /// The hv value.
        /// </summary>
        private string hv;

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        public Message()
        {
        }

        /// <summary>
        /// Gets or sets the SNME.
        /// </summary>
        /// <value>The SNME value.</value>
        [XmlAttribute("SNME")]
        public string SNME
        {
            get { return this.snme; }
            set { this.snme = value; }
        }

        /// <summary>
        /// Gets or sets the SFNM.
        /// </summary>
        /// <value>The SFNM value.</value>
        [XmlAttribute("SFNM")]
        public string SFNM
        {
            get { return this.sfnm; }
            set { this.sfnm = value; }
        }

        /// <summary>
        /// Gets or sets the MESS.
        /// </summary>
        /// <value>The MESS value.</value>
        [XmlAttribute("MESS")]
        public string MESS
        {
            get { return this.mess; }
            set { this.mess = value; }
        }

        /// <summary>
        /// Gets or sets the RID.
        /// </summary>
        /// <value>The RID value.</value>
        [XmlAttribute("RID")]
        public string RID
        {
            get { return this.rid; }
            set { this.rid = value; }
        }

        /// <summary>
        /// Gets or sets the SCD.
        /// </summary>
        /// <value>The SCD value.</value>
        [XmlAttribute("SCD")]
        public string SCD
        {
            get { return this.scd; }
            set { this.scd = value; }
        }

        /// <summary>
        /// Gets or sets the SID.
        /// </summary>
        /// <value>The SID value.</value>
        [XmlAttribute("SID")]
        public Guid SID
        {
            get { return this.sid; }
            set { this.sid = value; }
        }

        /// <summary>
        /// Gets or sets the MOD.
        /// </summary>
        /// <value>The MOD value.</value>
        [XmlAttribute("MOD")]
        public string MOD
        {
            get { return this.mod; }
            set { this.mod = value; }
        }

        /// <summary>
        /// Gets or sets the CH.
        /// </summary>
        /// <value>The CH value.</value>
        [XmlAttribute("CH")]
        public int CH
        {
            get { return this.ch; }
            set { this.ch = value; }
        }

        /// <summary>
        /// Gets or sets the HV.
        /// </summary>
        /// <value>The HV value.</value>
        [XmlAttribute("HV")]
        public string HV
        {
            get { return this.hv; }
            set { this.hv = value; }
        }
    }
}
