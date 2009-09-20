using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickReader;

namespace DPXDatabase
{
    public class Student
    {
        private int id;
        private String username;
        private String fullName;
        private String firstName;
        private String lastName;
        private int section;
        private Boolean isEnrolled;

        public int Id
        {
            get { return id; }
        }
        public String Username
        {
            get { return username; }
        }
        public String FullName
        {
            get { return fullName; }
        }
        public String FirstName
        {
            get { return firstName; }
        }
        public String LastName
        {
            get { return lastName; }
        }
        public int Section
        {
            get { return section; }
        }
        public Boolean IsEnrolled
        {
            get { return isEnrolled; }
        }

        public Student(DyKnowPage dp)
        {
            this.id = -1;
            this.username = dp.UserName;
            this.fullName = dp.FullName;
            //First name
            if (dp.FullName.IndexOf(',') > 0)
            {
                this.firstName = dp.FullName.Substring(0, dp.FullName.IndexOf(','));
                this.lastName = dp.FullName.Substring(dp.FullName.IndexOf(',')+2);
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


        public Student(int id, String username, String fullName, String firstName, 
            String lastName, int section, Boolean isEnrolled)
        {
            this.id = id;
            this.username = username;
            this.fullName = fullName;
            this.firstName = firstName;
            this.lastName = lastName;
            this.section = section;
            this.isEnrolled = isEnrolled;
        }


        public Student(String username, String fullName, String firstName,
            String lastName, int section, Boolean isEnrolled)
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
            this.username = "";
            this.fullName = "";
            this.firstName = "";
            this.lastName = "";
            this.section = -1;
            this.isEnrolled = false;
        }

        public override string ToString()
        {
            //return id.ToString() + ", " + username + ", " + fullName + ", " + firstName
            //     + ", " + lastName + ", " + section.ToString() + ", " + isEnrolled.ToString();
            return fullName + " (" + username + ")";
        }
    }
}
