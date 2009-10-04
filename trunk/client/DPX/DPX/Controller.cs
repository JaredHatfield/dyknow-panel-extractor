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
        Database db;

       // Collections of data kept in the interface
        ComboBox comboBoxDates;
        ListBox listBoxStudents;

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


        public void setComboBoxDates(ComboBox cbd)
        {
            comboBoxDates = cbd;
        }

        public void setListBoxStudents(ListBox stu)
        {
            listBoxStudents = stu;
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
            
        }

        public void refreshClassdate()
        {
            comboBoxDates.Items.Clear();
            // Fill in all of the currently available dates
            List<Classdate> cdl = db.getClassdates();
            for (int i = 0; i < cdl.Count; i++)
            {
                comboBoxDates.Items.Add(cdl[i]);
            }
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

        public void addDyKnowFile(DyKnowReader dr, String filename, Classdate cd)
        {
            File f = new File(cd.Id, filename, dr.MeanStrokes, dr.StdDevStrokes, dr.MinStrokeCount,
                dr.MaxStrokeCount, dr.MeanStrokeDistance, dr.StdDevStrokeDistance,
                dr.MinStrokeDistance, dr.MaxStrokeDistance);
            int fileId = db.addFile(f);

            for (int i = 0; i < dr.NumOfPages(); i++)
            {
                db.addPanel(fileId, dr.getDyKnowPage(i));
            }
        }

        public void closeDatabase()
        {
            if (db != null)
            {
                db.Connection.Dispose();
                db = null;
            }
            comboBoxDates.Items.Clear();
            listBoxStudents.Items.Clear();
        }
    }
}
