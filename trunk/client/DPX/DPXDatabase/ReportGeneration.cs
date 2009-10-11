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
                for (int i = 0; i < exception.Count; i++)
                {

                    //if(exception[i].Date
                }

                for (int i = 0; i < panels.Count; i++)
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
