// <copyright file="Reason.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Object for a Reason as represented in the database.
    /// </summary>
    public class Reason
    {
        /// <summary>
        /// The database identifier.
        /// </summary>
        private int id;

        /// <summary>
        /// 
        /// </summary>
        private bool credit;

        /// <summary>
        /// 
        /// </summary>
        private string description;

        /// <summary>
        /// Initializes a new instance of the Reason class.
        /// </summary>
        public Reason()
        {
            this.id = -1;
            this.credit = false;
            this.description = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the Reason class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="credit"></param>
        /// <param name="description"></param>
        public Reason(int id, bool credit, string description)
        {
            this.id = id;
            this.credit = credit;
            this.description = description;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get { return this.id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Credit
        {
            get { return this.credit; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get { return this.description; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.description;
        } 
    }
}
