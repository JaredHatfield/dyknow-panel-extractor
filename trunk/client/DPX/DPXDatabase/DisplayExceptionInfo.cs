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
    /// Object for DisplayExceptionInfo.
    /// </summary>
    public class DisplayExceptionInfo
    {
        /// <summary>
        /// The date for the exception.
        /// </summary>
        private DateTime date;

        /// <summary>
        /// Whether the exception grants credit.
        /// </summary>
        private bool credit;

        /// <summary>
        /// The description for the exception.
        /// </summary>
        private string description;

        /// <summary>
        /// The notes associated with the exception.
        /// </summary>
        private string notes;

        /// <summary>
        /// Initializes a new instance of the DisplayExceptionInfo class.
        /// </summary>
        /// <param name="date">The date of the exception.</param>
        /// <param name="credit">If the exception provides credit.</param>
        /// <param name="description">The description for the excpetion.</param>
        /// <param name="notes">The notes for the exception.</param>
        public DisplayExceptionInfo(DateTime date, bool credit, string description, string notes)
        {
            this.date = date;
            this.credit = credit;
            this.description = description;
            this.notes = notes;
        }

        /// <summary>
        /// Gets the date of the exception.
        /// </summary>
        /// <value>The date of the exception.</value>
        public DateTime Date
        {
            get { return this.date; }
        }

        /// <summary>
        /// Gets a value indicating whether credit is granted for this exception.
        /// </summary>
        /// <value>True if the exception grants credit.</value>
        public bool Credit
        {
            get { return this.credit; }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get { return this.description; }
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes
        {
            get { return this.notes; }
        }
    }
}
