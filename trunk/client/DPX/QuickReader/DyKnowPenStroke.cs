// <copyright file="DyKnowPenStroke.cs" company="DPX">
// GNU General Public License v3
// </copyright>
// <summary>DyKnow Pen Stroke object.</summary>
namespace QuickReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class DyKnowPenStroke
    {
        /// <summary>
        /// 
        /// </summary>
        private int ut;

        /// <summary>
        /// 
        /// </summary>
        private int pw;

        /// <summary>
        /// 
        /// </summary>
        private int ph;

        /// <summary>
        /// 
        /// </summary>
        private string uid;

        /// <summary>
        /// 
        /// </summary>
        private string data;

        /// <summary>
        /// 
        /// </summary>
        private bool deleted;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ut"></param>
        /// <param name="pw"></param>
        /// <param name="ph"></param>
        /// <param name="uid"></param>
        /// <param name="data"></param>
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
        /// 
        /// </summary>
        public int UT
        {
            get { return this.ut; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PW
        {
            get { return this.pw; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PH
        {
            get { return this.ph; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UID
        {
            get { return this.uid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DATA
        {
            get { return this.data; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool DELETED
        {
            get { return this.deleted; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void DeleteStroke()
        {
            this.deleted = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "UT=" + this.ut.ToString() + " PW=" + this.pw.ToString() + " UID=" + this.uid + 
                " DATA=" + this.data + " DEL=" + this.deleted.ToString();
        }
    }
}
