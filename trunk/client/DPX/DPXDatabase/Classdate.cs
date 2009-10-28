namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Classdate
    {
        private int id;
        private DateTime classDate;

        public Classdate()
        {
            this.id = -1;
            this.classDate = DateTime.Today;
        }

        public Classdate(int id, DateTime classdate)
        {
            this.id = id;
            this.classDate = classdate;
        }

        public int Id
        {
            get { return this.id; }
        }

        public DateTime ClassDate
        {
            get { return this.classDate; }
        }

        public override string ToString()
        {
            return this.classDate.Date.ToShortDateString();
        }
    }
}
