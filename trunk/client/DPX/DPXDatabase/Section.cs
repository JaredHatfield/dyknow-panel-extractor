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
        /// The name of the section.
        /// </summary>
        private string sectionName;

        /// <summary>
        /// Initializes a new instance of the Section class.
        /// </summary>
        /// <param name="id">The database identifier.</param>
        /// <param name="sectionName">The name of the section.</param>
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
        /// Gets the database identifier.
        /// </summary>
        /// <value>The database identifier.</value>
        public int Id
        {
            get { return this.id; }
        }

        /// <summary>
        /// Gets the name of the section.
        /// </summary>
        /// <value>The name of the section.</value>
        public string SectionName
        {
            get { return this.sectionName; }
        }

        /// <summary>
        /// Gets the string representation of a section.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return this.sectionName;
        }
    }
}
