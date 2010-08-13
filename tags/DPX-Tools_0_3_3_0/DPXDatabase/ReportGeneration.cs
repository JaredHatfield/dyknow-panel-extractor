// <copyright file="ReportGeneration.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Generates a report for a given set of dates.</summary>
namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Generates a report for a given set of dates.
    /// </summary>
    internal class ReportGeneration
    {
        /// <summary>
        /// A database object.
        /// </summary>
        private Database db;

        /// <summary>
        /// The list of students.
        /// </summary>
        private List<Student> students;

        /// <summary>
        /// The list of sections.
        /// </summary>
        private List<Section> sections;

        /// <summary>
        /// The list of dates.
        /// </summary>
        private List<Classdate> dates;

        /// <summary>
        /// The student report objects.
        /// </summary>
        private List<StudentReport> studentReport;

        /// <summary>
        /// Initializes a new instance of the ReportGeneration class.
        /// </summary>
        /// <param name="db">A database instance.</param>
        /// <param name="dates">The list of dates to generate the report for.</param>
        public ReportGeneration(Database db, List<Classdate> dates)
        {
            this.db = db;
            this.dates = dates;
            this.students = db.GetAllStudents();
            this.sections = db.GetSections();
            
            // Generate the report... this will take a while
            this.studentReport = new List<StudentReport>();
            for (int i = 0; i < this.students.Count; i++)
            {
                this.studentReport.Add(new StudentReport(db, this.students[i], dates));
            }
        }

        /// <summary>
        /// Gets the report for the given dates broken down by section and then by students.
        /// </summary>
        /// <returns>The string representation of the report.</returns>
        public string GetReport()
        {
            string report = "Report for - ";
            for (int i = 0; i < this.dates.Count; i++)
            {
                report += this.dates[i] + " - ";
            }

            report += "\n";

            // Not the most elegant solution to break student's out by section, but it works.
            List<Section> sections = this.db.GetSections();
            for (int n = 0; n < sections.Count; n++)
            {
                report += "\n----------------------------------------------------\n";
                report += sections[n].SectionName + ":\n";
                for (int i = 0; i < this.studentReport.Count; i++)
                {
                    if (sections[n].Id == this.studentReport[i].Student.Section)
                    {
                        StudentReport sr = this.studentReport[i];
                        report += sr.ToString() + "\n";
                    }
                }
            }

            return report;
        }
    }
}
