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

    public class Student
    {
        private int id;
        private string username;
        private string fullName;
        private string firstName;
        private string lastName;
        private int section;
        private bool isEnrolled;

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

        public int Id
        {
            get { return this.id; }
        }

        public string Username
        {
            get { return this.username; }
        }

        public string FullName
        {
            get { return this.fullName; }
        }

        public string FirstName
        {
            get { return this.firstName; }
        }

        public string LastName
        {
            get { return this.lastName; }
        }

        public int Section
        {
            get { return this.section; }
            set { this.section = value; }
        }

        public bool IsEnrolled
        {
            get { return this.isEnrolled; }
            set { this.isEnrolled = value; }
        }

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

        public override string ToString()
        {
            return this.fullName + " (" + this.username + ")";
        }
    }
}
