// <copyright file="Student.cs" company="DPX">
// GNU General Public License v3
// </copyright>
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
        /// 
        /// </summary>
        private string username;

        /// <summary>
        /// 
        /// </summary>
        private string fullName;

        /// <summary>
        /// 
        /// </summary>
        private string firstName;

        /// <summary>
        /// 
        /// </summary>
        private string lastName;

        /// <summary>
        /// 
        /// </summary>
        private int section;

        /// <summary>
        /// 
        /// </summary>
        private bool isEnrolled;

        /// <summary>
        /// Initializes a new instance of the Student class.
        /// </summary>
        /// <param name="dp"></param>
        public Student(DyKnowPage dp)
        {
            this.id = -1;
            this.username = dp.UserName;
            this.fullName = dp.FullName;

            // First name
            if (dp.FullName.IndexOf(',') > 0)
            {
                this.firstName = dp.FullName.Substring(0, dp.FullName.IndexOf(','));
                this.lastName = dp.FullName.Substring(dp.FullName.IndexOf(',') + 2);
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="fullName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="section"></param>
        /// <param name="isEnrolled"></param>
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
        /// <param name="username"></param>
        /// <param name="fullName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="section"></param>
        /// <param name="isEnrolled"></param>
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
        /// 
        /// </summary>
        public int Id
        {
            get { return this.id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Username
        {
            get { return this.username; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FullName
        {
            get { return this.fullName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName
        {
            get { return this.firstName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LastName
        {
            get { return this.lastName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Section
        {
            get { return this.section; }
            set { this.section = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEnrolled
        {
            get { return this.isEnrolled; }
            set { this.isEnrolled = value; }
        }

        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.fullName + " (" + this.username + ")";
        }
    }
}
