// <copyright file="Reason.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Object for a Reason as represented in the database.</summary>
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
        /// Indicates whether this reason grants credit.
        /// </summary>
        private bool credit;

        /// <summary>
        /// The description of the reason.
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
        /// <param name="id">The database identifier.</param>
        /// <param name="credit">Indicates whether the reason grants credit.</param>
        /// <param name="description">The description for the reason.</param>
        public Reason(int id, bool credit, string description)
        {
            this.id = id;
            this.credit = credit;
            this.description = description;
        }

        /// <summary>
        /// Gets the database identifier for the reason.
        /// </summary>
        /// <value>The database identifier for the reason.</value>
        public int Id
        {
            get { return this.id; }
        }

        /// <summary>
        /// Gets a value indicating whether the reason grants credit.
        /// </summary>
        /// <value>A value indicating whether the reason grants credit.</value>
        public bool Credit
        {
            get { return this.credit; }
        }

        /// <summary>
        /// Gets the description for the reason.
        /// </summary>
        /// <value>The description for the reason.</value>
        public string Description
        {
            get { return this.description; }
        }

        /// <summary>
        /// A string representation for the reason.
        /// </summary>
        /// <returns>The string representation for the reason.</returns>
        public override string ToString()
        {
            return this.description;
        } 
    }
}
