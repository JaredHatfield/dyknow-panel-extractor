namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Exceptions
    {
        private int classdate;
        private int student;
        private int reason;
        private string notes;

        public Exceptions(int classdate, int student, int reason, string notes)
        {
            this.classdate = classdate;
            this.student = student;
            this.reason = reason;
            this.notes = notes;
        }

        public int Classdate
        {
            get { return this.classdate; }
        }

        public int Student
        {
            get { return this.student; }
        }

        public int Reason
        {
            get { return this.reason; }
        }

        public string Notes
        {
            get { return this.notes; }
        }
    }
}
