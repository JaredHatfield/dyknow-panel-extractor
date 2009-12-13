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
    /// 
    /// </summary>
    public class Section
    {
        /// <summary>
        /// 
        /// </summary>
        private int id;
        
        /// <summary>
        /// 
        /// </summary>
        private string sectionName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sectionName"></param>
        public Section(int id, string sectionName)
        {
            this.id = id;
            this.sectionName = sectionName;
        }

        /// <summary>
        /// 
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
