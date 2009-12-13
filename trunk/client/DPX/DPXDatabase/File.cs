// <copyright file="File.cs" company="DPX">
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
    public class File
    {
        /// <summary>
        /// 
        /// </summary>
        private int id;

        /// <summary>
        /// 
        /// </summary>
        private int classdate;

        /// <summary>
        /// 
        /// </summary>
        private string fileName;

        /// <summary>
        /// 
        /// </summary>
        private double meanStrokes;

        /// <summary>
        /// 
        /// </summary>
        private double stdDevStrokes;

        /// <summary>
        /// 
        /// </summary>
        private double minStrokes;

        /// <summary>
        /// 
        /// </summary>
        private double maxStrokes;

        /// <summary>
        /// 
        /// </summary>
        private double meanDataLength;

        /// <summary>
        /// 
        /// </summary>
        private double stdDevDataLength;

        /// <summary>
        /// 
        /// </summary>
        private double minDataLength;

        /// <summary>
        /// 
        /// </summary>
        private double maxDataLength;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="classdate"></param>
        /// <param name="fileName"></param>
        /// <param name="meanStrokes"></param>
        /// <param name="stdDevStrokes"></param>
        /// <param name="minStrokes"></param>
        /// <param name="maxStrokes"></param>
        /// <param name="meanDataLength"></param>
        /// <param name="stdDevDataLength"></param>
        /// <param name="minDataLength"></param>
        /// <param name="maxDataLength"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classdate"></param>
        /// <param name="fileName"></param>
        /// <param name="meanStrokes"></param>
        /// <param name="stdDevStrokes"></param>
        /// <param name="minStrokes"></param>
        /// <param name="maxStrokes"></param>
        /// <param name="meanDataLength"></param>
        /// <param name="stdDevDataLength"></param>
        /// <param name="minDataLength"></param>
        /// <param name="maxDataLength"></param>
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

        /// <summary>
        /// 
        /// </summary>
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
        public int Classdate
        {
            get { return this.classdate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            get { return this.fileName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double MeanStrokes
        {
            get { return this.meanStrokes; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double StdDevStrokes
        {
            get { return this.stdDevStrokes; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double MinStrokes
        {
            get { return this.minStrokes; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double MaxStrokes
        {
            get { return this.maxStrokes; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double MeanDataLength
        {
            get { return this.meanDataLength; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double StdDevDataLength
        {
            get { return this.stdDevDataLength; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double MinDataLength
        {
            get { return this.minDataLength; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double MaxDataLength
        {
            get { return this.maxDataLength; }
        }
    }
}
