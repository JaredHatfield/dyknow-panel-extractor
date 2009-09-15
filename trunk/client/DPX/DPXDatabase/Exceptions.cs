using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPXDatabase
{
    public class Exceptions
    {
        int classdate;
        int student;
        int reason;
        String notes;

        public int Classdate
        {
            get { return classdate; }
        }
        public int Student
        {
            get { return student; }
        }
        public int Reason
        {
            get { return reason; }
        }
        public String Notes
        {
            get { return notes; }
        }

        public Exceptions(int classdate, int student, int reason, String notes)
        {
            this.classdate = classdate;
            this.student = student;
            this.reason = reason;
            this.notes = notes;
        }
    }
}
