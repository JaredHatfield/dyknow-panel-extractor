// <copyright file="DyKnowImage.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace QuickReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DyKnowImage
    {
        private int ut;
        
        private string sp;
        
        private int pw;
        
        private int ph;
        
        private Guid uid;
        
        private Guid id;
        
        private int wid;

        private int hei;

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

        public Guid Id
        {
            get { return this.id; }
        }

        public int Pw
        {
            get { return this.pw; }
        }

        public int Ph
        {
            get { return this.ph; }
        }

        public override string ToString()
        {
            return this.ut.ToString() + " - " + this.sp.ToString() + " - " + this.uid.ToString();
        }
    }
}
