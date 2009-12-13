// <copyright file="StudentDate.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class StudentDate
    {
        private Classdate date;
        private Student student;
        private bool noCredit;
        private bool exception;
        private bool panelWithCredit;
        private bool panelWithoutCredit;

        public StudentDate(Student student, Classdate date)
        {
            this.date = date;
            this.student = student;
            this.noCredit = false;
            this.exception = false;
            this.panelWithCredit = false;
            this.panelWithoutCredit = false;
        }

        public bool NoCredit
        {
            get { return this.noCredit; }
            set { this.noCredit = value; }
        }

        public bool Exception
        {
            get { return this.exception; }
            set { this.exception = value; }
        }

        public bool PanelWithCredit
        {
            get { return this.panelWithCredit; }
            set { this.panelWithCredit = value; }
        }

        public bool PanelWithoutCredit
        {
            get { return this.panelWithoutCredit; }
            set { this.panelWithoutCredit = value; }
        }

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
