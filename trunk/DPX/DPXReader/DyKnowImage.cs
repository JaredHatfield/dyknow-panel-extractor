// <copyright file="DyKnowImage.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>A representation of an image embedded in a DyKnow panel.</summary>
namespace QuickReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A representation of an image embedded in a DyKnow panel.
    /// </summary>
    public class DyKnowImage
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
        /// The unique id.
        /// </summary>
        private Guid id;

        /// <summary>
        /// The wid value.
        /// </summary>
        private int wid;

        /// <summary>
        /// The hei value.
        /// </summary>
        private int hei;

        /// <summary>
        /// Initializes a new instance of the <see cref="DyKnowImage"/> class.
        /// </summary>
        /// <param name="ut">The ut value.</param>
        /// <param name="sp">The sp value.</param>
        /// <param name="pw">The pw value.</param>
        /// <param name="ph">The ph value.</param>
        /// <param name="uid">The uid value.</param>
        /// <param name="id">The unique id.</param>
        /// <param name="wid">The wid value.</param>
        /// <param name="hei">The hei value.</param>
        public DyKnowImage(int ut, string sp, int pw, int ph, string uid, string id, int wid, int hei)
        {
            this.ut = ut;
            this.sp = sp;
            this.pw = pw;
            this.ph = ph;
            this.uid = new Guid(uid);
            this.id = new Guid(id);
            this.wid = wid;
            this.hei = hei;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DyKnowImage"/> class.
        /// </summary>
        /// <param name="ut">The ut value.</param>
        /// <param name="sp">The sp value.</param>
        /// <param name="pw">The pw value.</param>
        /// <param name="ph">The ph value.</param>
        /// <param name="uid">The uid value.</param>
        /// <param name="id">The unique id.</param>
        /// <param name="wid">The wid value.</param>
        /// <param name="hei">The hei value.</param>
        public DyKnowImage(int ut, string sp, int pw, int ph, Guid uid, Guid id, int wid, int hei)
        {
            this.ut = ut;
            this.sp = sp;
            this.pw = pw;
            this.ph = ph;
            this.uid = uid;
            this.id = id;
            this.wid = wid;
            this.hei = hei;
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The unique id.</value>
        public Guid Id
        {
            get { return this.id; }
        }

        /// <summary>
        /// Gets the pw.
        /// </summary>
        /// <value>The pw value.</value>
        public int Pw
        {
            get { return this.pw; }
        }

        /// <summary>
        /// Gets the ph.
        /// </summary>
        /// <value>The ph value.</value>
        public int Ph
        {
            get { return this.ph; }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.ut.ToString() + " - " + this.sp.ToString() + " - " + this.uid.ToString();
        }
    }
}
