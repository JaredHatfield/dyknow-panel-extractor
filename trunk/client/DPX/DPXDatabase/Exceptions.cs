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
    /// Object for an Exceptions as represented in the database.
    /// </summary>
    public class Exceptions
    {
        /// <summary>
        /// The date for the exception.
        /// </summary>
        private int classdate;
        
        /// <summary>
        /// The identifier for the student.
        /// </summary>
        private int student;

        /// <summary>
        /// The identifier for the reason.
        /// </summary>
        private int reason;

        /// <summary>
        /// The notes explaining the exception.
        /// </summary>
        private string notes;

        /// <summary>
        /// Initializes a new instance of the Exceptions class.
        /// </summary>
        /// <param name="classdate">The date for the exception.</param>
        /// <param name="student">The identifier for a student.</param>
        /// <param name="reason">The identifier for a reason.</param>
        /// <param name="notes">The notes explaining the exception.</param>
        public Exceptions(int classdate, int student, int reason, string notes)
        {
            this.classdate = classdate;
            this.student = student;
            this.reason = reason;
            this.notes = notes;
        }

        /// <summary>
        /// Gets the date for the exception.
        /// </summary>
        /// <value>The date for the exception.</value>
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
