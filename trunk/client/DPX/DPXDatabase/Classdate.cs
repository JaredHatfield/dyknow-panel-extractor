// <copyright file="Classdate.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Object for a Classdate as represented in the database.</summary>
namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Object for a Classdate as represented in the database.
    /// </summary>
    public class Classdate
    {
        /// <summary>
        /// The database identifier.
        /// </summary>
        private int id;

        /// <summary>
        /// The date object.
        /// </summary>
        private DateTime classDate;

        /// <summary>
        /// Initializes a new instance of the Classdate class.
        /// </summary>
        public Classdate()
        {
            this.id = -1;
            this.classDate = DateTime.Today;
        }

        /// <summary>
        /// Initializes a new instance of the Classdate class.
        /// </summary>
        /// <param name="id">The database identifier.</param>
        /// <param name="classdate">The date for the class.</param>
        public Classdate(int id, DateTime classdate)
        {
            this.id = id;
            this.classDate = classdate;
        }

        /// <summary>
        /// Gets the database identifier.
        /// </summary>
        /// <value>The database identifier.</value>
        public int Id
        {
            get { return this.id; }
        }

        /// <summary>
        /// Gets the date of the class.
        /// </summary>
        /// <value>The date for the class.</value>
        public DateTime ClassDate
        {
            get { return this.classDate; }
        }

        /// <summary>
        /// Gets a string representation of the class date.
        /// </summary>
        /// <returns>String representation of the class date.</returns>
        public override string ToString()
        {
            return this.classDate.Date.ToShortDateString();
        }
    }
}
