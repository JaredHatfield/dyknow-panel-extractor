// <copyright file="StudentDate.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    internal class StudentDate
    {
        /// <summary>
        /// 
        /// </summary>
        private Classdate date;

        /// <summary>
        /// 
        /// </summary>
        private Student student;

        /// <summary>
        /// 
        /// </summary>
        private bool noCredit;

        /// <summary>
        /// 
        /// </summary>
        private bool exception;

        /// <summary>
        /// 
        /// </summary>
        private bool panelWithCredit;

        /// <summary>
        /// 
        /// </summary>
        private bool panelWithoutCredit;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <param name="date"></param>
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
        /// 
        /// </summary>
        public bool NoCredit
        {
            get { return this.noCredit; }
            set { this.noCredit = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Exception
        {
            get { return this.exception; }
            set { this.exception = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool PanelWithCredit
        {
            get { return this.panelWithCredit; }
            set { this.panelWithCredit = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public bool PanelWithoutCredit
        {
            get { return this.panelWithoutCredit; }
            set { this.panelWithoutCredit = value; }
        }

        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        /// <returns></returns>
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
