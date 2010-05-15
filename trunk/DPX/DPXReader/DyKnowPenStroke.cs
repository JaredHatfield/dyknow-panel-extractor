// <copyright file="DyKnowPenStroke.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>A representation of a single pen stroke.</summary>
namespace DPXReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A representation of a single pen stroke.
    /// </summary>
    public class DyKnowPenStroke
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
        private string uid;

        /// <summary>
        /// The data value.
        /// </summary>
        private string data;

        /// <summary>
        /// The deleted value.
        /// </summary>
        private bool deleted;

        /// <summary>
        /// Initializes a new instance of the <see cref="DyKnowPenStroke"/> class.
        /// </summary>
        /// <param name="ut">The ut value.</param>
        /// <param name="pw">The pw value.</param>
        /// <param name="ph">The ph value.</param>
        /// <param name="uid">The uid value.</param>
        /// <param name="data">The data value.</param>
        public DyKnowPenStroke(int ut, int pw, int ph, string uid, string data)
        {
            this.ut = ut;
            this.pw = pw;
            this.ph = ph;
            this.uid = uid;
            this.data = data;
            this.deleted = false;
        }

        /// <summary>
        /// Gets the UT.
        /// </summary>
        /// <value>The UT value.</value>
        public int UT
        {
            get { return this.ut; }
        }

        /// <summary>
        /// Gets the PW.
        /// </summary>
        /// <value>The PW value.</value>
        public int PW
        {
            get { return this.pw; }
        }

        /// <summary>
        /// Gets the PH.
        /// </summary>
        /// <value>The PH value.</value>
        public int PH
        {
            get { return this.ph; }
        }

        /// <summary>
        /// Gets the UID.
        /// </summary>
        /// <value>The UID value.</value>
        public string UID
        {
            get { return this.uid; }
        }

        /// <summary>
        /// Gets the DATA.
        /// </summary>
        /// <value>The DATA value.</value>
        public string DATA
        {
            get { return this.data; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="DyKnowPenStroke"/> is DELETED.
        /// </summary>
        /// <value><c>True</c> if DELETED; otherwise, <c>false</c>.</value>
        public bool DELETED
        {
            get { return this.deleted; }
        }

        /// <summary>
        /// Deletes the stroke.
        /// </summary>
        public void DeleteStroke()
        {
            this.deleted = true;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "UT=" + this.ut.ToString() + " PW=" + this.pw.ToString() + " UID=" + this.uid + 
                " DATA=" + this.data + " DEL=" + this.deleted.ToString();
        }
    }
}
