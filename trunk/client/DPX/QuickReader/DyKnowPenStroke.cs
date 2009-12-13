// <copyright file="DyKnowPenStroke.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace QuickReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DyKnowPenStroke
    {
        private int ut;

        private int pw;

        private int ph;

        private string uid;

        private string data;

        private bool deleted;

        public DyKnowPenStroke(int ut, int pw, int ph, string uid, string data)
        {
            this.ut = ut;
            this.pw = pw;
            this.ph = ph;
            this.uid = uid;
            this.data = data;
            this.deleted = false;
        }

        public int UT
        {
            get { return this.ut; }
        }

        public int PW
        {
            get { return this.pw; }
        }

        public int PH
        {
            get { return this.ph; }
        }

        public string UID
        {
            get { return this.uid; }
        }

        public string DATA
        {
            get { return this.data; }
        }

        public bool DELETED
        {
            get { return this.deleted; }
        }

        public void DeleteStroke()
        {
            this.deleted = true;
        }

        public override string ToString()
        {
            return "UT=" + this.ut.ToString() + " PW=" + this.pw.ToString() + " UID=" + this.uid + 
                " DATA=" + this.data + " DEL=" + this.deleted.ToString();
        }
    }
}
