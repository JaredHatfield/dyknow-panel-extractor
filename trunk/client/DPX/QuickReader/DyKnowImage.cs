using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickReader
{
    public class DyKnowImage
    {
        private int ut;
        private String sp;
        private int pw;
        private int ph;
        private Guid uid;
        private Guid id;
        private int wid;
        private int hei;


        public Guid Id
        {
            get { return id; }
        }

        public DyKnowImage(int ut, String sp, int pw, int ph, String uid, String id, int wid, int hei)
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

        public DyKnowImage(int ut, String sp, int pw, int ph, Guid uid, Guid id, int wid, int hei)
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

        public override string ToString()
        {
            return ut.ToString() + " - " + sp.ToString() + " - " + uid.ToString();
        }
    }
}
