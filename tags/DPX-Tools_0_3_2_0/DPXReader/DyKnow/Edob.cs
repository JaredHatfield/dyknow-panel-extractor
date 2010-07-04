// <copyright file="Edob.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The edob object.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The edob object.
    /// </summary>
    [XmlRoot("EDOB")]
    public class Edob
    {
        /// <summary>
        /// The ut value.
        /// </summary>
        private int ut;

        /// <summary>
        /// The sp value.
        /// </summary>
        private string sp;

        /// <summary>
        /// The ep value.
        /// </summary>
        private string ep;

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
        /// The dpi value.
        /// </summary>
        private string dpi;

        /// <summary>
        /// The isrem value.
        /// </summary>
        private bool isrem;

        /// <summary>
        /// The sti value.
        /// </summary>
        private int sti;

        /// <summary>
        /// The edli list.
        /// </summary>
        private ArrayList edli;

        /// <summary>
        /// Initializes a new instance of the <see cref="Edob"/> class.
        /// </summary>
        public Edob()
        {
            this.edli = new ArrayList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Edob"/> class.
        /// </summary>
        /// <param name="ut">The ut value.</param>
        /// <param name="sp">The sp value.</param>
        /// <param name="ep">The ep value.</param>
        /// <param name="pw">The pw value.</param>
        /// <param name="ph">The ph value.</param>
        /// <param name="uid">The uid value.</param>
        /// <param name="isrem">if set to <c>true</c> [isrem].</param>
        /// <param name="sti">The sti value.</param>
        public Edob(int ut, string sp, string ep, int pw, int ph, Guid uid, bool isrem, int sti)
        {
            this.edli = new ArrayList();
            this.ut = ut;
            this.sp = sp;
            this.ep = ep;
            this.pw = pw;
            this.ph = ph;
            this.uid = uid;
            this.isrem = isrem;
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
        /// Gets or sets the SP.
        /// </summary>
        /// <value>The SP value.</value>
        [XmlAttribute("SP")]
        public string SP
        {
            get { return this.sp; }
            set { this.sp = value; }
        }

        /// <summary>
        /// Gets or sets the EP.
        /// </summary>
        /// <value>The EP value.</value>
        [XmlAttribute("EP")]
        public string EP
        {
            get { return this.ep; }
            set { this.ep = value; }
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
        /// Gets or sets the DPI.
        /// </summary>
        /// <value>The DPI value.</value>
        [XmlAttribute("DPI")]
        public string DPI
        {
            get { return this.dpi; }
            set { this.dpi = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Edob"/> is ISREM.
        /// </summary>
        /// <value><c>true</c> if ISREM; otherwise, <c>false</c>.</value>
        [XmlAttribute("ISREM")]
        public bool ISREM
        {
            get { return this.isrem; }
            set { this.isrem = value; }
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
        /// Gets or sets the DELI.
        /// </summary>
        /// <value>The DELI value.</value>
        [XmlArray("EDLI")]
        [XmlArrayItem("EDDE", typeof(Edde), IsNullable = true)]
        public ArrayList EDLI
        {
            get { return this.edli; }
            set { this.edli = value; }
        }
    }
}
