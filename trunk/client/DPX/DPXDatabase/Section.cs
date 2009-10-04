using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPXDatabase
{
    public class Section
    {
        private int id;
        private String sectionName;

        public int Id
        {
            get { return id; }
        }
        public String SectionName
        {
            get { return sectionName; }
        }

        public Section(int id, String sectionName)
        {
            this.id = id;
            this.sectionName = sectionName;
        }

        public Section()
        {
            this.id = -1;
            this.sectionName = "No-Name-Section";
        }

        public override string ToString()
        {
            return sectionName;
        }
    }
}
