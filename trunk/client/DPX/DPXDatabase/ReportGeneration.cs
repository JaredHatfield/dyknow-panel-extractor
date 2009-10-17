using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPXDatabase
{
    internal class ReportGeneration
    {
        Database db;
        List<Student> students;
        List<Section> sections;
        List<Classdate> dates;


        public ReportGeneration(Database db, List<Classdate> dates)
        {
            this.db = db;
            this.dates = dates;
            this.students = db.getAllStudents();
            this.sections = db.getSections();
        }

        public String getReport()
        {
            String report = "Report - ";
            for (int i = 0; i < dates.Count; i++)
            {
                report += dates[i] + " - ";
            }
            report += "\n";

            for (int i = 0; i < students.Count; i++)
            {
                StudentReport sr = new StudentReport(db, students[i], dates);
            }
            return report;
        }



    }

    
    
    class StudentReport
    {
        private Student student;
        private List<Classdate> dates;
        private List<DisplayExceptionInfo> exception;
        private List<DisplayPanelInfo> panels;

        public StudentReport(Database db, Student student, List<Classdate> dates)
        {
            this.student = student;
            this.dates = dates;
            exception = db.getExceptionsForStudent(student.Id);
            panels = db.getPanelsForStudent(student.Id);
        }

        public String getReport()
        {
            List<StudentDate> studentDate = new List<StudentDate>();
            for (int n = 0; n < dates.Count; n++)
            {
                StudentDate sd = new StudentDate(student, dates[n]);
                for (int i = 0; i < exception.Count; i++)
                {
                    // Matched a date for the students exception to a date in the report
                    if (exception[i].Date.Equals(dates[i].ClassDate))
                    {
                        // The student gets credit
                        if (exception[i].Credit)
                        {
                            sd.Exception = true;
                        }
                        // The student does not get credit
                        else
                        {
                            sd.NoCredit = true;
                        }
                    }
                }

                for (int i = 0; i < panels.Count; i++)
                {
                    // Matched a date for a panel to a date in the report
                    if (panels[i].Date.Equals(dates[i].ClassDate))
                    {
                        // Panel is blank, no credit
                        if (panels[i].IsBlank)
                        {
                            sd.PanelWithoutCredit = true;
                        }
                        // Panel is NOT blank, student gets credit
                        else
                        {
                            sd.PanelWithCredit = true;
                        }
                    }
                }
            }

            for (int i = 0; i < studentDate.Count; i++)
            {
                int count = 0;
                if (studentDate[i].Result)
                {
                    
                }
            }
            return "Not implemented";
        }
    }


    class StudentDate
    {
        private Classdate date;
        private Student student;
        private Boolean noCredit;
        private Boolean exception;
        private Boolean panelWithCredit;
        private Boolean panelWithoutCredit;

        public Boolean NoCredit { get; set; }
        public Boolean Exception { get; set; }
        public Boolean PanelWithCredit { get; set; }
        public Boolean PanelWithoutCredit { get; set; }

        public Boolean Result
        {
            get
            {
                if (noCredit)
                {
                    return false;
                }
                else if (exception)
                {
                    return true;
                }
                else if (panelWithCredit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        

        public StudentDate(Student student, Classdate date)
        {
            this.date = date;
            this.student = student;
            noCredit = false;
            exception = false;
            panelWithCredit = false;
            panelWithoutCredit = false;
        }

    
    }
}
