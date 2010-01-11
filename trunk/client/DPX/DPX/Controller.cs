// <copyright file="Controller.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace DPX
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Windows.Controls;
    using DPXDatabase;
    using QuickReader;

    /// <summary>
    /// Singleton class used for accessing the datbase.
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
        /// List of dates.
        /// </summary>
        private ComboBox comboBoxDateImport;

        /// <summary>
        /// List of exceptions.
        /// </summary>
        private ComboBox comboBoxDateException;

        /// <summary>
        /// List of sections.
        /// </summary>
        private ComboBox comboBoxSections;

        /// <summary>
        /// List of reasons.
        /// </summary>
        private ComboBox comboBoxReasons;

        /// <summary>
        /// List of students.
        /// </summary>
        private ListBox listBoxStudents;

        /// <summary>
        /// List of dates.
        /// </summary>
        private ListBox listBoxReportDates;

        /// <summary>
        /// Prevents a default instance of the Controller class from being created.
        /// </summary>
        private Controller()
        {
            this.db = null;
        }

        /// <summary>
        /// Gets an instance of the database.
        /// </summary>
        public Database DB
        {
            get { return this.db; }
        }

        /// <summary>
        /// Gets an instance of the Controller class using a Singleton accessor.
        /// </summary>
        /// <returns>An instance of the Controller class.</returns>
        public static Controller Instance()
        {
            if (c == null)
            {
                c = new Controller();
            }

            return c;
        }

        /// <summary>
        /// Sets a reference to the comboBoxDateImport object.
        /// </summary>
        /// <param name="cbd">The instance of comboBoxDateImport found in the GUI.</param>
        public void SetComboBoxDateImport(ComboBox cbd)
        {
            if (this.comboBoxDateImport == null)
            {
                this.comboBoxDateImport = cbd;
            }
        }

        /// <summary>
        /// Sets a reference to the comboBoxDateException object.
        /// </summary>
        /// <param name="cbd">The instance of comboBoxDateException found in the GUI.</param>
        public void SetComboBoxDateException(ComboBox cbd)
        {
            if (this.comboBoxDateException == null)
            {
                this.comboBoxDateException = cbd;
            }
        }

        /// <summary>
        /// Sets a reference to the comboBoxSections object.
        /// </summary>
        /// <param name="cbs">The instance of comboBoxSections found in the GUI.</param>
        public void SetComboBoxSections(ComboBox cbs)
        {
            if (this.comboBoxSections == null)
            {
                this.comboBoxSections = cbs;
            }
        }

        /// <summary>
        /// Sets a reference to the comboBoxReason object.
        /// </summary>
        /// <param name="cbr">The instance of comboBoxReason found in the GUI.</param>
        public void SetComboBoxReason(ComboBox cbr)
        {
            if (this.comboBoxReasons == null)
            {
                this.comboBoxReasons = cbr;
            }
        }

        /// <summary>
        /// Sets a reference to the listBoxStudents found in the GUI.
        /// </summary>
        /// <param name="stu">The instance of listBoxStudents found in the GUI.</param>
        public void SetListBoxStudents(ListBox stu)
        {
            if (this.listBoxStudents == null)
            {
                this.listBoxStudents = stu;
            }
        }

        /// <summary>
        /// Sets a reference to the listBoxReportDates found in the GUI.
        /// </summary>
        /// <param name="r">The instance of listBoxReportDates found in the GUI.</param>
        public void SetListBoxReportDates(ListBox r)
        {
            if (this.listBoxReportDates == null)
            {
                this.listBoxReportDates = r;
            }
        }

        /// <summary>
        /// Determine if the database is open.
        /// </summary>
        /// <returns>True if the database is open.</returns>
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
        /// Open the specified database.
        /// </summary>
        /// <param name="filename">Path to the database to open.</param>
        public void OpenDatabaseFile(string filename)
        {
            this.db = new Database(filename);

            this.RefreshClassdate();
            this.RefreshStudents();
            this.RefreshSections();
            this.RefreshReasons();
        }

        /// <summary>
        /// Refresh the class dates.
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
        /// Refresh the sections.
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
        /// Refresh the reasons.
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
        /// Refresh the students.
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
        /// Add a DyKnow file to the database.
        /// </summary>
        /// <param name="dr">DyKnow file to add.</param>
        /// <param name="filename">The filename of the DyKnow file.</param>
        /// <param name="cd">The class date for the DyKnow fiel.</param>
        /// <returns>ID for the file that was added to the datbase.</returns>
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
        /// Add a student to the class roster.
        /// </summary>
        /// <param name="firstname">First name of the student.</param>
        /// <param name="lastname">Last name of the student.</param>
        /// <param name="username">Username of the student.</param>
        /// <param name="section">Section of the student (automatically added if it does not exist).</param>
        public void AddStudentToRoster(string firstname, string lastname, string username, string section)
        {
            // Get the section id or add it to the database if it doesn't exist
            int sectionId = this.DB.GetSectionId(section);
            if (sectionId == -1)
            {
                this.DB.AddSection(section);
                sectionId = this.DB.GetSectionId(section);
            }

            string fullname = lastname.ToString() + ", ".ToString() + firstname.ToString();
            Student student = new Student(username, fullname, firstname, lastname, sectionId, true);
            this.db.AddStudent(student);
        }

        /// <summary>
        /// Close the database.
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
