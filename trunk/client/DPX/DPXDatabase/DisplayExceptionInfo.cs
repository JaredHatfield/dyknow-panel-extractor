// <copyright file="DisplayExceptionInfo.cs" company="DPX">
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
    public class DisplayExceptionInfo
    {
        /// <summary>
        /// 
        /// </summary>
        private DateTime date;

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
        private string notes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="credit"></param>
        /// <param name="description"></param>
        /// <param name="notes"></param>
        public DisplayExceptionInfo(DateTime date, bool credit, string description, string notes)
        {
            this.date = date;
            this.credit = credit;
            this.description = description;
            this.notes = notes;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date
        {
            get { return this.date; }
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
        public string Notes
        {
            get { return this.notes; }
        }
    }
}
