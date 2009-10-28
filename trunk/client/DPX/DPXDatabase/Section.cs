namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Section
    {
        private int id;
        private string sectionName;

        public Section(int id, string sectionName)
        {
            this.id = id;
            this.sectionName = sectionName;
        }

        public Section()
        {
            this.id = -1;
            this.sectionName = "No-Name-Section";
        }

        public int Id
        {
            get { return this.id; }
        }

        public string SectionName
        {
            get { return this.sectionName; }
        }

        public override string ToString()
        {
            return this.sectionName;
        }
    }
}
