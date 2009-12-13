// <copyright file="File.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class File
    {
        private int id;
        private int classdate;
        private string fileName;
        private double meanStrokes;
        private double stdDevStrokes;
        private double minStrokes;
        private double maxStrokes;
        private double meanDataLength;
        private double stdDevDataLength;
        private double minDataLength;
        private double maxDataLength;

        public File(
            int id, 
            int classdate, 
            string fileName, 
            double meanStrokes, 
            double stdDevStrokes,
            double minStrokes, 
            double maxStrokes, 
            double meanDataLength, 
            double stdDevDataLength,
            double minDataLength, 
            double maxDataLength)
        {
            this.id = id;
            this.classdate = classdate;
            this.fileName = fileName;
            this.meanStrokes = meanStrokes;
            this.stdDevStrokes = stdDevStrokes;
            this.minStrokes = minStrokes;
            this.maxStrokes = maxStrokes;
            this.meanDataLength = maxDataLength;
            this.stdDevDataLength = stdDevDataLength;
            this.minDataLength = minDataLength;
            this.maxDataLength = maxDataLength;
        }

        public File(
            int classdate, 
            string fileName, 
            double meanStrokes, 
            double stdDevStrokes,
            double minStrokes, 
            double maxStrokes, 
            double meanDataLength, 
            double stdDevDataLength,
            double minDataLength, 
            double maxDataLength)
        {
            this.id = -1;
            this.classdate = classdate;
            this.fileName = fileName;
            this.meanStrokes = meanStrokes;
            this.stdDevStrokes = stdDevStrokes;
            this.minStrokes = minStrokes;
            this.maxStrokes = maxStrokes;
            this.meanDataLength = maxDataLength;
            this.stdDevDataLength = stdDevDataLength;
            this.minDataLength = minDataLength;
            this.maxDataLength = maxDataLength;
        }

        public File()
        {
            this.id = -1;
            this.classdate = -1;
            this.fileName = string.Empty;
            this.meanStrokes = -1;
            this.stdDevStrokes = -1;
            this.minStrokes = -1;
            this.maxStrokes = -1;
            this.meanDataLength = -1;
            this.stdDevDataLength = -1;
            this.minDataLength = -1;
            this.maxDataLength = -1;
        }

        public int Id
        {
            get { return this.id; }
        }

        public int Classdate
        {
            get { return this.classdate; }
        }

        public string FileName
        {
            get { return this.fileName; }
        }

        public double MeanStrokes
        {
            get { return this.meanStrokes; }
        }

        public double StdDevStrokes
        {
            get { return this.stdDevStrokes; }
        }

        public double MinStrokes
        {
            get { return this.minStrokes; }
        }

        public double MaxStrokes
        {
            get { return this.maxStrokes; }
        }

        public double MeanDataLength
        {
            get { return this.meanDataLength; }
        }

        public double StdDevDataLength
        {
            get { return this.stdDevDataLength; }
        }

        public double MinDataLength
        {
            get { return this.minDataLength; }
        }

        public double MaxDataLength
        {
            get { return this.maxDataLength; }
        }
    }
}
