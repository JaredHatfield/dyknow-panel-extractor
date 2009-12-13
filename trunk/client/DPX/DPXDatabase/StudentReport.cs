// <copyright file="StudentReport.cs" company="DPX">
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
    internal class StudentReport
    {
        /// <summary>
        /// 
        /// </summary>
        private Student student;

        /// <summary>
        /// 
        /// </summary>
        private List<Classdate> dates;

        /// <summary>
        /// 
        /// </summary>
        private List<DisplayExceptionInfo> exception;

        /// <summary>
        /// 
        /// </summary>
        private List<DisplayPanelInfo> panels;

        /// <summary>
        /// 
        /// </summary>
        private List<StudentDate> studentDate;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="student"></param>
        /// <param name="dates"></param>
        public StudentReport(Database db, Student student, List<Classdate> dates)
        {
            this.student = student;
            this.dates = dates;
            this.exception = db.GetExceptionsForStudent(student.Id);
            this.panels = db.GetPanelsForStudent(student.Id);

            // GENERATE THE REPORT
            this.studentDate = new List<StudentDate>();
            for (int n = 0; n < dates.Count; n++)
            {
                StudentDate sd = new StudentDate(student, dates[n]);
                for (int i = 0; i < this.exception.Count; i++)
                {
                    // Matched a date for the students exception to a date in the report
                    if (this.exception[i].Date.Equals(dates[n].ClassDate))
                    {
                        if (this.exception[i].Credit)
                        {
                            // The student gets Credit
                            sd.Exception = true;
                        }
                        else
                        {
                            // The student does not get Credit
                            sd.NoCredit = true;
                        }
                    }
                }

                for (int i = 0; i < this.panels.Count; i++)
                {
                    // Matched a date for a panel to a date in the report
                    if (this.panels[i].Date.Equals(dates[n].ClassDate))
                    {
                        if (this.panels[i].IsBlank)
                        {
                            // Panel is blank, no Credit
                            sd.PanelWithoutCredit = true;
                        }
                        else
                        {
                            // Panel is NOT blank, student gets Credit
                            sd.PanelWithCredit = true;
                        }
                    }
                }

                this.studentDate.Add(sd);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Student Student
        {
            get { return this.student; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Credit()
        {
            int num = 0;
            for (int i = 0; i < this.studentDate.Count; i++)
            {
                if (this.studentDate[i].Result)
                {
                    num++;
                }
            }

            return num;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Credit() + "\t" + this.student.FullName;
        }
    }
}
