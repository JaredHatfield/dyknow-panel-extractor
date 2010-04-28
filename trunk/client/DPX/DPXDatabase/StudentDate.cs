// <copyright file="StudentDate.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Object for a StudentDate.</summary>
namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Object for a StudentDate.
    /// </summary>
    internal class StudentDate
    {
        /// <summary>
        /// The date of student.
        /// </summary>
        private Classdate date;

        /// <summary>
        /// The student.
        /// </summary>
        private Student student;

        /// <summary>
        /// Flag to indicate no credit should be given.
        /// </summary>
        private bool noCredit;

        /// <summary>
        /// Flag to indicate an exception with credit.
        /// </summary>
        private bool exception;

        /// <summary>
        /// Flag to indicate a panel with credit.
        /// </summary>
        private bool panelWithCredit;

        /// <summary>
        /// Flag to indicate a panel without credit.
        /// </summary>
        private bool panelWithoutCredit;

        /// <summary>
        /// Initializes a new instance of the StudentDate class.
        /// </summary>
        /// <param name="student">The student.</param>
        /// <param name="date">The dates.</param>
        public StudentDate(Student student, Classdate date)
        {
            this.date = date;
            this.student = student;
            this.noCredit = false;
            this.exception = false;
            this.panelWithCredit = false;
            this.panelWithoutCredit = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the students gets no credit.
        /// </summary>
        /// <value>Indicates whether the students gets no credit.</value>
        public bool NoCredit
        {
            get { return this.noCredit; }
            set { this.noCredit = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the student gets an exception.
        /// </summary>
        /// <value>Indicates whether there is an exception.</value>
        public bool Exception
        {
            get { return this.exception; }
            set { this.exception = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether there is a panel with credit.
        /// </summary>
        /// <value>Indicates whether there is a panel with credit.</value>
        public bool PanelWithCredit
        {
            get { return this.panelWithCredit; }
            set { this.panelWithCredit = value; }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether there is a panel without credit.
        /// </summary>
        /// <value>Indicates whether there is a panel without credit.</value>
        public bool PanelWithoutCredit
        {
            get { return this.panelWithoutCredit; }
            set { this.panelWithoutCredit = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the students gets credit.
        /// </summary>
        /// <value>Indicates whether the student gets credit.</value>
        public bool Result
        {
            get
            {
                if (this.noCredit)
                {
                    return false;
                }
                else if (this.exception)
                {
                    return true;
                }
                else if (this.panelWithCredit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets a string representation of student date.
        /// </summary>
        /// <returns>A string representation.</returns>
        public override string ToString()
        {
            return this.student.FullName + " " + this.date.ToString() + "\n" +
                "No Credit: " + this.noCredit.ToString() +
                " - Exception: " + this.exception.ToString() +
                " - Panel With Credit: " + this.panelWithCredit.ToString() +
                " - Panel WIthout Credit: " + this.panelWithoutCredit.ToString();
        }
    }
}
