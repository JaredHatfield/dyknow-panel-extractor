using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPXDatabase
{
    public class Classdate
    {
        private int id;
        private DateTime classDate;

        public int Id
        {
            get { return id; }
        }
        public DateTime ClassDate
        {
            get { return classDate; }
        }

        public Classdate(int id, DateTime classdate)
        {
            this.id = id;
            this.classDate = classdate;
        }

        public Classdate()
        {
            this.id = -1;
            this.classDate = DateTime.Today;
        }
    }
}
