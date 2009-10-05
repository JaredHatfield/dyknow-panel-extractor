using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPXDatabase
{
    public class DisplayExceptionInfo
    {
        private DateTime date;
        private Boolean credit;
        private string description;
        private string notes;

        public DateTime Date
        {
            get { return date; }
        }
        public Boolean Credit
        {
            get { return credit; }
        }
        public String Description
        {
            get { return description; }
        }
        public String Notes
        {
            get { return notes; }
        }

        public DisplayExceptionInfo(DateTime date, Boolean credit, string description, string notes)
        {
            this.date = date;
            this.credit = credit;
            this.description = description;
            this.notes = notes;
        }


    }
}
