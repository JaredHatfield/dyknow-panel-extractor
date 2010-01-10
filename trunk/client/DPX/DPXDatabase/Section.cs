// <copyright file="Section.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Object for a Section as represented in the database.
    /// </summary>
    public class Section
    {
        /// <summary>
        /// The database identifier.
        /// </summary>
        private int id;
        
        /// <summary>
        /// 
        /// </summary>
        private string sectionName;

        /// <summary>
        /// Initializes a new instance of the Section class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sectionName"></param>
        public Section(int id, string sectionName)
        {
            this.id = id;
            this.sectionName = sectionName;
        }

        /// <summary>
        /// Initializes a new instance of the Section class.
        /// </summary>
        public Section()
        {
            this.id = -1;
            this.sectionName = "No-Name-Section";
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get { return this.id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SectionName
        {
            get { return this.sectionName; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.sectionName;
        }
    }
}
