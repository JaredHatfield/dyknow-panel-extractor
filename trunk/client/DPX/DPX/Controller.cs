// <copyright file="Controller.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace DPX
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Controls;
    using DPXDatabase;
    using QuickReader;

    /// <summary>
    /// 
    /// </summary>
    internal class Controller
    {
        /// <summary>
        /// Singleton instance of the class.
        /// </summary>
        private static Controller c;

        /// <summary>
        /// Instance of the database.
        /// </summary>
        private Database db;

        // Collections of data kept in the interface

        /// <summary>
        /// 
        /// </summary>
        private ComboBox comboBoxDateImport;

        /// <summary>
        /// 
        /// </summary>
        private ComboBox comboBoxDateException;

        /// <summary>
        /// 
        /// </summary>
        private ComboBox comboBoxSections;

        /// <summary>
        /// 
        /// </summary>
        private ComboBox comboBoxReasons;

        /// <summary>
        /// 
        /// </summary>
        private ListBox listBoxStudents;

        /// <summary>
        /// 
        /// </summary>
        private ListBox listBoxReportDates;

        /// <summary>
        /// 
        /// </summary>
        private ProgressBar progressBarMaster;

        /// <summary>
        /// 
        /// </summary>
        private Controller()
        {
            this.db = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public Database DB
        {
            get { return this.db; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Controller Instance()
        {
            if (c == null)
            {
                c = new Controller();
            }

            return c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cbd"></param>
        public void SetComboBoxDateImport(ComboBox cbd)
        {
            if (this.comboBoxDateImport == null)
            {
                this.comboBoxDateImport = cbd;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cbd"></param>
        public void SetComboBoxDateException(ComboBox cbd)
        {
            if (this.comboBoxDateException == null)
            {
                this.comboBoxDateException = cbd;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cbs"></param>
        public void SetComboBoxSections(ComboBox cbs)
        {
            if (this.comboBoxSections == null)
            {
                this.comboBoxSections = cbs;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cbr"></param>
        public void SetComboBoxReason(ComboBox cbr)
        {
            if (this.comboBoxReasons == null)
            {
                this.comboBoxReasons = cbr;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stu"></param>
        public void SetListBoxStudents(ListBox stu)
        {
            if (this.listBoxStudents == null)
            {
                this.listBoxStudents = stu;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        public void SetListBoxReportDates(ListBox r)
        {
            if (this.listBoxReportDates == null)
            {
                this.listBoxReportDates = r;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pb"></param>
        public void SetProgressBarMaster(ProgressBar pb)
        {
            if (this.progressBarMaster == null)
            {
                this.progressBarMaster = pb;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsDatabaseOpen()
        {
            if (this.db == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public void OpenDatabaseFile(string filename)
        {
            this.db = new Database(filename);

            this.RefreshClassdate();
            this.RefreshStudents();
            this.RefreshSections();
            this.RefreshReasons();
        }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshClassdate()
        {
            this.comboBoxDateImport.Items.Clear();
            this.comboBoxDateException.Items.Clear();
            this.listBoxReportDates.Items.Clear();

            // Fill in all of the currently available dates
            List<Classdate> cdl = this.db.GetClassdates();
            for (int i = 0; i < cdl.Count; i++)
            {
                this.comboBoxDateImport.Items.Add(cdl[i]);
                this.comboBoxDateException.Items.Add(cdl[i]);
                this.listBoxReportDates.Items.Add(cdl[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshSections()
        {
            this.comboBoxSections.Items.Clear();

            // Fill in all of the sections
            List<Section> s = this.db.GetSections();
            for (int i = 0; i < s.Count; i++)
            {
                this.comboBoxSections.Items.Add(s[i]);
            }

            this.comboBoxSections.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshReasons()
        {
            this.comboBoxReasons.Items.Clear();

            // Fill in all of the sections
            List<Reason> res = this.db.GetReasons();
            for (int i = 0; i < res.Count; i++)
            {
                this.comboBoxReasons.Items.Add(res[i]);
            }

            this.comboBoxReasons.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshStudents()
        {
            this.listBoxStudents.Items.Clear();

            // Fill in all of the students
            List<Student> allStudents = c.DB.GetAllStudents();
            this.listBoxStudents.Items.Clear();
            for (int i = 0; i < allStudents.Count; i++)
            {
                this.listBoxStudents.Items.Add(allStudents[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="filename"></param>
        /// <param name="cd"></param>
        /// <returns></returns>
        public int AddDyKnowFile(DyKnowReader dr, string filename, Classdate cd)
        {
            File f = new File(
                cd.Id, 
                filename, 
                dr.MeanStrokes, 
                dr.StdDevStrokes, 
                dr.MinStrokeCount,
                dr.MaxStrokeCount, 
                dr.MeanStrokeDistance, 
                dr.StdDevStrokeDistance,
                dr.MinStrokeDistance, 
                dr.MaxStrokeDistance);
            int fileId = this.db.AddFile(f);
            
            if (fileId < 0)
            {
                return fileId;
            }

            for (int i = 0; i < dr.NumOfPages(); i++)
            {
                if (!this.db.IsStudentUsername(dr.GetDyKnowPage(i).UserName))
                {
                    this.db.AddStudent(new Student(dr.GetDyKnowPage(i)));
                }

                this.db.AddPanel(fileId, dr.GetDyKnowPage(i));
            }

            return fileId;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CloseDatabase()
        {
            if (this.db != null)
            {
                this.db.Connection.Dispose();
                this.db = null;
            }

            this.comboBoxDateImport.Items.Clear();
            this.comboBoxDateException.Items.Clear();
            this.comboBoxSections.Items.Clear();
            this.comboBoxReasons.Items.Clear();
            this.listBoxStudents.Items.Clear();
        }
    }
}
