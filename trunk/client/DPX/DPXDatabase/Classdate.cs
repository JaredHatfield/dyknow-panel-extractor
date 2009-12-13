// <copyright file="Classdate.cs" company="DPX">
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
    public class Classdate
    {
        /// <summary>
        /// 
        /// </summary>
        private int id;

        /// <summary>
        /// 
        /// </summary>
        private DateTime classDate;

        /// <summary>
        /// 
        /// </summary>
        public Classdate()
        {
            this.id = -1;
            this.classDate = DateTime.Today;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="classdate"></param>
        public Classdate(int id, DateTime classdate)
        {
            this.id = id;
            this.classDate = classdate;
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
        public DateTime ClassDate
        {
            get { return this.classDate; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.classDate.Date.ToShortDateString();
        }
    }
}
