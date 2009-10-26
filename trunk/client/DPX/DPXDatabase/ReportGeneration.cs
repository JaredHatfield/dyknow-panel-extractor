using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPXDatabase
{
    internal class ReportGeneration
    {
        private Database db;
        private List<Student> students;
        private List<Section> sections;
        private List<Classdate> dates;

        List<StudentReport> studentReport;

        public ReportGeneration(Database db, List<Classdate> dates)
        {
            this.db = db;
            this.dates = dates;
            this.students = db.getAllStudents();
            this.sections = db.getSections();
            
            // Generate the report... this will take a while
            studentReport = new List<StudentReport>();
            for (int i = 0; i < students.Count; i++)
            {
                studentReport.Add(new StudentReport(db, students[i], dates));
            }
        }

        public String getReport()
        {
            String report = "Report for - ";
            for (int i = 0; i < dates.Count; i++)
            {
                report += dates[i] + " - ";
            }
            report += "\n";


            // Not the most elegant solution to break student's out by section, but it works.
            List<Section> sections = db.getSections();
            for (int n = 0; n < sections.Count; n++)
            {
                report += "\n----------------------------------------------------\n";
                report += sections[n].SectionName + ":\n";
                for (int i = 0; i < studentReport.Count; i++)
                {
                    if (sections[n].Id == studentReport[i].Student.Section)
                    {
                        StudentReport sr = studentReport[i];
                        report += sr.ToString() + "\n";
                    }
                }
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

        private List<StudentDate> studentDate;


        public Student Student
        {
            get { return student; }
        }

        public StudentReport(Database db, Student student, List<Classdate> dates)
        {
            this.student = student;
            this.dates = dates;
            exception = db.getExceptionsForStudent(student.Id);
            panels = db.getPanelsForStudent(student.Id);

            // GENERATE THE REPORT
            studentDate = new List<StudentDate>();
            for (int n = 0; n < dates.Count; n++)
            {
                StudentDate sd = new StudentDate(student, dates[n]);
                for (int i = 0; i < exception.Count; i++)
                {
                    // Matched a date for the students exception to a date in the report
                    if (exception[i].Date.Equals(dates[n].ClassDate))
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
                    if (panels[i].Date.Equals(dates[n].ClassDate))
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
                studentDate.Add(sd);
            }
        }

        public int credit()
        {
            int num = 0;
            for (int i = 0; i < studentDate.Count; i++)
            {
                if (studentDate[i].Result)
                {
                    num++;
                }
            }
            return num;
        }

        public override string ToString()
        {
            return this.credit() + "\t" + student.FullName;
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

        public Boolean NoCredit
        {
            get { return noCredit; }
            set { noCredit = value; }
        }
        public Boolean Exception
        {
            get { return exception; }
            set { exception = value; }
        }
        public Boolean PanelWithCredit
        {
            get { return panelWithCredit; }
            set { panelWithCredit = value; }
        }
        public Boolean PanelWithoutCredit
        {
            get { return panelWithoutCredit; }
            set { panelWithoutCredit = value; }
        }

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

        public override string ToString()
        {
            return student.FullName + " " + date.ToString() + "\n" +
                "No Credit: " + noCredit.ToString() + 
                " - Exception: " + exception.ToString() +
                " - Panel With Credit: " + panelWithCredit.ToString() +
                " - Panel WIthout Credit: " + panelWithoutCredit.ToString();
        }
    
    }
}
