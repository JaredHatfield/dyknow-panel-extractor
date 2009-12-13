// <copyright file="Reason.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Reason
    {
        private int id;
        private bool credit;
        private string description;

        public Reason()
        {
            this.id = -1;
            this.credit = false;
            this.description = string.Empty;
        }

        public Reason(int id, bool credit, string description)
        {
            this.id = id;
            this.credit = credit;
            this.description = description;
        }

        public int Id
        {
            get { return this.id; }
        }

        public bool Credit
        {
            get { return this.credit; }
        }

        public string Description
        {
            get { return this.description; }
        }

        public override string ToString()
        {
            return this.description;
        } 
    }
}
