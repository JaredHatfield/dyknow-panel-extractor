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
    /// Generate a report for a student.
    /// </summary>
    internal class StudentReport
    {
        /// <summary>
        /// The student.
        /// </summary>
        private Student student;

        /// <summary>
        /// The dates to include in the report.
        /// </summary>
        private List<Classdate> dates;

        /// <summary>
        /// The list of exceptions.
        /// </summary>
        private List<DisplayExceptionInfo> exception;

        /// <summary>
        /// The list of panels.
        /// </summary>
        private List<DisplayPanelInfo> panels;

        /// <summary>
        /// The list of studentDates.
        /// </summary>
        private List<StudentDate> studentDate;

        /// <summary>
        /// Initializes a new instance of the StudentReport class.
        /// </summary>
        /// <param name="db">Database connections.</param>
        /// <param name="student">The student.</param>
        /// <param name="dates">The dates for the report.</param>
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
        /// Gets the student.
        /// </summary>
        /// <value>The student.</value>
        public Student Student
        {
            get { return this.student; }
        }

        /// <summary>
        /// Gets the number of credits.
        /// </summary>
        /// <returns>The number of credits.</returns>
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
        /// A string representation of the student report.
        /// </summary>
        /// <returns>A string representation.</returns>
        public override string ToString()
        {
            return this.Credit() + "\t" + this.student.FullName;
        }
    }
}
