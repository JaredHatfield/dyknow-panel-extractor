using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickReader
{
    public class DyKnowPenStroke
    {
        private int ut;
        private int pw;
        private int ph;
        private String uid;
        private String data;
        private bool deleted;

        public int UT
        {
            get { return ut; }
        }
        public int PW
        {
            get { return pw; }
        }
        public int PH
        {
            get { return ph; }
        }
        public String UID
        {
            get { return uid; }
        }
        public String DATA
        {
            get { return data; }
        }
        public bool DELETED
        {
            get { return deleted; }
        }

        public DyKnowPenStroke(int ut, int pw, int ph, String uid, String data)
        {
            this.ut = ut;
            this.pw = pw;
            this.uid = uid;
            this.data = data;
            deleted = false;
        }

        public void deleteStroke()
        {
            deleted = true;
        }


        public String ToString()
        {
            return "UT=" + ut.ToString() + " PW=" + pw.ToString() + " UID=" + uid + 
                " DATA=" + data + " DEL=" + deleted.ToString();
        }
    }
}
