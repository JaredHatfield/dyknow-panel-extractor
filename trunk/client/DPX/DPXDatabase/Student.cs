using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPXDatabase
{
    public class Student
    {
        int id;
        String username;
        String fullName;
        String firstName;
        String lastName;
        int section;
        Boolean isEnrolled;

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
            return id.ToString() + ", " + username + ", " + fullName + ", " + firstName
                 + ", " + lastName + ", " + section.ToString() + ", " + isEnrolled.ToString();
        }
    }
}
