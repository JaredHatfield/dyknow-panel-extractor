// <copyright file="Deob.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The deob object.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The deob object.
    /// </summary>
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
        /// Initializes a new instance of the <see cref="Deob"/> class.
        /// </summary>
        public Deob()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Deob"/> class.
        /// </summary>
        /// <param name="ut">The ut value.</param>
        /// <param name="uid">The uid value.</param>
        /// <param name="ig">if set to <c>true</c> [ig].</param>
        public Deob(int ut, Guid uid, bool ig)
        {
            this.ut = ut;
            this.uid = uid;
            this.ig = ig;
        }

        /// <summary>
        /// Gets or sets the UT.
        /// </summary>
        /// <value>The UT value.</value>
        public int UT
        {
            get { return this.ut; }
            set { this.ut = value; }
        }

        /// <summary>
        /// Gets or sets the UID.
        /// </summary>
        /// <value>The UID value.</value>
        public Guid UID
        {
            get { return this.uid; }
            set { this.uid = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Deob"/> is IG.
        /// </summary>
        /// <value><c>true</c> if IG; otherwise, <c>false</c>.</value>
        public bool IG
        {
            get { return this.ig; }
            set { this.ig = value; }
        }
    }
}
