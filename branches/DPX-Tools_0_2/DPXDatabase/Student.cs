// <copyright file="Student.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Object for a Student as represented in the database.</summary>
namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using QuickReader;

    /// <summary>
    /// Object for a Student as represented in the database.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// The database identifier.
        /// </summary>
        private int id;

        /// <summary>
        /// The student's user name.
        /// </summary>
        private string username;

        /// <summary>
        /// The student's full name.
        /// </summary>
        private string fullName;

        /// <summary>
        /// The student's first name.
        /// </summary>
        private string firstName;

        /// <summary>
        /// The student's last name.
        /// </summary>
        private string lastName;

        /// <summary>
        /// The student's section.
        /// </summary>
        private int section;

        /// <summary>
        /// Determines whether the student is enrolled.
        /// </summary>
        private bool isEnrolled;

        /// <summary>
        /// Initializes a new instance of the Student class.
        /// </summary>
        /// <param name="dp">A DyKnow page with user information embedded in it.</param>
        public Student(DyKnowPage dp)
        {
            this.id = -1;
            this.username = dp.UserName;
            this.fullName = dp.FullName;

            // First name
            if (dp.FullName.IndexOf(',') > 0)
            {
                this.lastName = dp.FullName.Substring(0, dp.FullName.IndexOf(','));
                this.firstName = dp.FullName.Substring(dp.FullName.IndexOf(',') + 2);
                if (this.lastName.IndexOf(' ') > 0)
                {
                    this.lastName = this.lastName.Substring(0, this.lastName.IndexOf(' '));
                }
            }
            else
            {
                this.firstName = dp.FullName;
                this.lastName = dp.FullName;
            }

            this.section = -1;
            this.isEnrolled = true;
        }

        /// <summary>
        /// Initializes a new instance of the Student class.
        /// </summary>
        /// <param name="id">The database identifier.</param>
        /// <param name="username">Student's user name.</param>
        /// <param name="fullName">Student's full name.</param>
        /// <param name="firstName">Student's first name.</param>
        /// <param name="lastName">Student's last name.</param>
        /// <param name="section">Student's section.</param>
        /// <param name="isEnrolled">Indicates whether student is enrolled.</param>
        public Student(
            int id, 
            string username, 
            string fullName, 
            string firstName,
            string lastName, 
            int section, 
            bool isEnrolled)
        {
            this.id = id;
            this.username = username;
            this.fullName = fullName;
            this.firstName = firstName;
            this.lastName = lastName;
            this.section = section;
            this.isEnrolled = isEnrolled;
        }

        /// <summary>
        /// Initializes a new instance of the Student class.
        /// </summary>
        /// <param name="username">Student's user name.</param>
        /// <param name="fullName">Student's full name.</param>
        /// <param name="firstName">Student's first name.</param>
        /// <param name="lastName">Student's last name.</param>
        /// <param name="section">Student's section.</param>
        /// <param name="isEnrolled">Indicates whether student is enrolled.</param>
        public Student(
            string username, 
            string fullName, 
            string firstName,
            string lastName, 
            int section, 
            bool isEnrolled)
        {
            this.id = -1;
            this.username = username;
            this.fullName = fullName;
            this.firstName = firstName;
            this.lastName = lastName;
            this.section = section;
            this.isEnrolled = isEnrolled;
        }

        /// <summary>
        /// Initializes a new instance of the Student class.
        /// </summary>
        public Student()
        {
            this.id = -1;
            this.username = string.Empty;
            this.fullName = string.Empty;
            this.firstName = string.Empty;
            this.lastName = string.Empty;
            this.section = -1;
            this.isEnrolled = false;
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
        /// Gets the student's username.
        /// </summary>
        /// <value>The student's username.</value>
        public string Username
        {
            get { return this.username; }
        }

        /// <summary>
        /// Gets the student's full name.
        /// </summary>
        /// <value>The student's full name.</value>
        public string FullName
        {
            get { return this.fullName; }
        }

        /// <summary>
        /// Gets the student's first name.
        /// </summary>
        /// <value>The student's first name.</value>
        public string FirstName
        {
            get { return this.firstName; }
        }

        /// <summary>
        /// Gets the student's last name.
        /// </summary>
        /// <value>The student's last name.</value>
        public string LastName
        {
            get { return this.lastName; }
        }

        /// <summary>
        /// Gets or sets the student's section.
        /// </summary>
        /// <value>The student's section.</value>
        public int Section
        {
            get { return this.section; }
            set { this.section = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the student is enrolled.
        /// </summary>
        /// <value>Value indicating whether the student is enrolled.</value>
        public bool IsEnrolled
        {
            get { return this.isEnrolled; }
            set { this.isEnrolled = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the student is in a section.
        /// </summary>
        /// <value>Value indicating whether the student is in a section.</value>
        public bool IsInSection
        {
            get
            {
                if (this.section == -1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Gets a string representation of the student.
        /// </summary>
        /// <returns>A string representation of the student.</returns>
        public override string ToString()
        {
            return this.fullName + " (" + this.username + ")";
        }
    }
}
