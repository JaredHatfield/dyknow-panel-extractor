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
    /// 
    /// </summary>
    public class Reason
    {
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public Reason()
        {
            this.id = -1;
            this.credit = false;
            this.description = string.Empty;
        }

        /// <summary>
        /// 
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
