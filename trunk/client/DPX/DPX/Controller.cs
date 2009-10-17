using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using DPXDatabase;
using QuickReader;

namespace DPX
{
    class Controller
    {
        //Singleton instance of the class
        private static Controller c;

        //Instance of the database
        private Database db;

        // Collections of data kept in the interface
        private ComboBox comboBoxDateImport;
        private ComboBox comboBoxDateException;
        private ComboBox comboBoxSections;
        private ComboBox comboBoxReasons;
        private ListBox listBoxStudents;
        private ListBox listBoxReportDates;
        private ProgressBar progressBarMaster;

        private Controller()
        {
            db = null;
        }

        public static Controller Instance()
        {
            if (c == null)
            {
                c = new Controller();
            }
            return c;
        }

        public Database DB
        {
            get { return db; }
        }


        public void setComboBoxDateImport(ComboBox cbd)
        {
            if (comboBoxDateImport == null)
            {
                comboBoxDateImport = cbd;
            }
        }

        public void setComboBoxDateException(ComboBox cbd)
        {
            if (comboBoxDateException == null)
            {
                comboBoxDateException = cbd;
            }
        }

        public void setComboBoxSections(ComboBox cbs)
        {
            if (comboBoxSections == null)
            {
                comboBoxSections = cbs;
            }
        }

        public void setComboBoxReason(ComboBox cbr)
        {
            if (comboBoxReasons == null)
            {
                comboBoxReasons = cbr;
            }
        }

        public void setListBoxStudents(ListBox stu)
        {
            if (listBoxStudents == null)
            {
                listBoxStudents = stu;
            }
        }

        public void setListBoxReportDates(ListBox r)
        {
            if (listBoxReportDates == null)
            {
                listBoxReportDates = r;
            }
        }

        public void setProgressBarMaster(ProgressBar pb)
        {
            if (progressBarMaster == null)
            {
                progressBarMaster = pb;
            }
        }


        public Boolean isDatabaseOpen()
        {
            if (db == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void openDatabaseFile(String filename)
        {
            db = new Database(filename);

            refreshClassdate();
            refreshStudents();
            refreshSections();
            refreshReasons();
        }

        public void refreshClassdate()
        {
            comboBoxDateImport.Items.Clear();
            comboBoxDateException.Items.Clear();
            listBoxReportDates.Items.Clear();
            // Fill in all of the currently available dates
            List<Classdate> cdl = db.getClassdates();
            for (int i = 0; i < cdl.Count; i++)
            {
                comboBoxDateImport.Items.Add(cdl[i]);
                comboBoxDateException.Items.Add(cdl[i]);
                listBoxReportDates.Items.Add(cdl[i]);
            }
        }

        public void refreshSections()
        {
            comboBoxSections.Items.Clear();
            // Fill in all of the sections
            List<Section> s = db.getSections();
            for (int i = 0; i < s.Count; i++)
            {
                comboBoxSections.Items.Add(s[i]);
            }
            comboBoxSections.SelectedIndex = 0;
        }

        public void refreshReasons()
        {
            comboBoxReasons.Items.Clear();
            // Fill in all of the sections
            List<Reason> res = db.getReasons();
            for (int i = 0; i < res.Count; i++)
            {
                comboBoxReasons.Items.Add(res[i]);
            }
            comboBoxReasons.SelectedIndex = 0;
        }

        public void refreshStudents()
        {
            listBoxStudents.Items.Clear();
            // Fill in all of the students
            List<Student> allStudents = c.DB.getAllStudents();
            listBoxStudents.Items.Clear();
            for (int i = 0; i < allStudents.Count; i++)
            {
                listBoxStudents.Items.Add(allStudents[i]);
            }
        }

        public int addDyKnowFile(DyKnowReader dr, String filename, Classdate cd)
        {
            
            File f = new File(cd.Id, filename, dr.MeanStrokes, dr.StdDevStrokes, dr.MinStrokeCount,
                dr.MaxStrokeCount, dr.MeanStrokeDistance, dr.StdDevStrokeDistance,
                dr.MinStrokeDistance, dr.MaxStrokeDistance);
            int fileId = db.addFile(f);
            
            if (fileId < 0)
            {
                return fileId;
            }

            for (int i = 0; i < dr.NumOfPages(); i++)
            {
                if (!db.isStudentUsername(dr.getDyKnowPage(i).UserName))
                {
                    db.addStudent(new Student(dr.getDyKnowPage(i)));
                }
                db.addPanel(fileId, dr.getDyKnowPage(i));
            }

            return fileId;
        }


        public void closeDatabase()
        {
            if (db != null)
            {
                db.Connection.Dispose();
                db = null;
            }
            comboBoxDateImport.Items.Clear();
            comboBoxDateException.Items.Clear();
            comboBoxSections.Items.Clear();
            comboBoxReasons.Items.Clear();
            listBoxStudents.Items.Clear();
             
        }
    }
}
