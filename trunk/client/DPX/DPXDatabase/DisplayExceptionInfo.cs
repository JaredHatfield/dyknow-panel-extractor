// <copyright file="DisplayExceptionInfo.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DisplayExceptionInfo
    {
        private DateTime date;
        private bool credit;
        private string description;
        private string notes;

        public DisplayExceptionInfo(DateTime date, bool credit, string description, string notes)
        {
            this.date = date;
            this.credit = credit;
            this.description = description;
            this.notes = notes;
        }

        public DateTime Date
        {
            get { return this.date; }
        }

        public bool Credit
        {
            get { return this.credit; }
        }

        public string Description
        {
            get { return this.description; }
        }

        public string Notes
        {
            get { return this.notes; }
        }
    }
}
