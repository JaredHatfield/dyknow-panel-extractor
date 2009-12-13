// <copyright file="DyKnowImage.cs" company="DPX">
// GNU General Public License v3
// </copyright>
// <summary>DyKnow Image object.</summary>
namespace QuickReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class DyKnowImage
    {
        /// <summary>
        /// 
        /// </summary>
        private int ut;
        
        /// <summary>
        /// 
        /// </summary>
        private string sp;
        
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
        private Guid uid;
        
        /// <summary>
        /// 
        /// </summary>
        private Guid id;
        
        /// <summary>
        /// 
        /// </summary>
        private int wid;

        /// <summary>
        /// 
        /// </summary>
        private int hei;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ut"></param>
        /// <param name="sp"></param>
        /// <param name="pw"></param>
        /// <param name="ph"></param>
        /// <param name="uid"></param>
        /// <param name="id"></param>
        /// <param name="wid"></param>
        /// <param name="hei"></param>
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
        /// 
        /// </summary>
        /// <param name="ut"></param>
        /// <param name="sp"></param>
        /// <param name="pw"></param>
        /// <param name="ph"></param>
        /// <param name="uid"></param>
        /// <param name="id"></param>
        /// <param name="wid"></param>
        /// <param name="hei"></param>
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
        /// 
        /// </summary>
        public Guid Id
        {
            get { return this.id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Pw
        {
            get { return this.pw; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Ph
        {
            get { return this.ph; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ut.ToString() + " - " + this.sp.ToString() + " - " + this.uid.ToString();
        }
    }
}
