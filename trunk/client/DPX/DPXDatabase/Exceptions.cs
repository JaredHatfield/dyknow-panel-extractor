// <copyright file="Exceptions.cs" company="DPX">
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
    public class Exceptions
    {
        /// <summary>
        /// 
        /// </summary>
        private int classdate;
        
        /// <summary>
        /// 
        /// </summary>
        private int student;

        /// <summary>
        /// 
        /// </summary>
        private int reason;

        /// <summary>
        /// 
        /// </summary>
        private string notes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classdate"></param>
        /// <param name="student"></param>
        /// <param name="reason"></param>
        /// <param name="notes"></param>
        public Exceptions(int classdate, int student, int reason, string notes)
        {
            this.classdate = classdate;
            this.student = student;
            this.reason = reason;
            this.notes = notes;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Classdate
        {
            get { return this.classdate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Student
        {
            get { return this.student; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Reason
        {
            get { return this.reason; }
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
