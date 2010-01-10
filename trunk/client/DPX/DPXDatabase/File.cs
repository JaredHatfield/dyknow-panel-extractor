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
    /// Object for a file as represented in the database.
    /// </summary>
    public class File
    {
        /// <summary>
        /// The database identifier.
        /// </summary>
        private int id;

        /// <summary>
        /// The date for the file.
        /// </summary>
        private int classdate;

        /// <summary>
        /// The file name.
        /// </summary>
        private string fileName;

        /// <summary>
        /// The mean number of pen strokes.
        /// </summary>
        private double meanStrokes;

        /// <summary>
        /// The standard deviation of the pen strokes.
        /// </summary>
        private double stdDevStrokes;

        /// <summary>
        /// The minimum number of pen strokes on a panel.
        /// </summary>
        private double minStrokes;

        /// <summary>
        /// The maximum number of pen strokes on a panel.
        /// </summary>
        private double maxStrokes;

        /// <summary>
        /// The mean length of pen stroke data.
        /// </summary>
        private double meanDataLength;

        /// <summary>
        /// The standard deviation of the pen stroke data.
        /// </summary>
        private double stdDevDataLength;

        /// <summary>
        /// The mimimum length of the pen stroke data.
        /// </summary>
        private double minDataLength;

        /// <summary>
        /// The maximum length of the pen stroke data.
        /// </summary>
        private double maxDataLength;

        /// <summary>
        /// Initializes a new instance of the File class.
        /// </summary>
        /// <param name="id">The database identifier</param>
        /// <param name="classdate">The date for the file.</param>
        /// <param name="fileName">The filename.</param>
        /// <param name="meanStrokes">The mean number of pen strokes per panel.</param>
        /// <param name="stdDevStrokes">The standard deviation of the pen strokes.</param>
        /// <param name="minStrokes">The minimum number of pen strokes on a panel.</param>
        /// <param name="maxStrokes">The maximum number of pen strokes on a panel.</param>
        /// <param name="meanDataLength">The mean amount of data per panel.</param>
        /// <param name="stdDevDataLength">The standard deviation of the amount of data.</param>
        /// <param name="minDataLength">The minimum amount of data on a panel.</param>
        /// <param name="maxDataLength">The maximum amount of data on a panel.</param>
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
        /// Initializes a new instance of the File class.
        /// </summary>
        /// <param name="classdate">The date for the file.</param>
        /// <param name="fileName">The filename.</param>
        /// <param name="meanStrokes">The mean number of pen strokes per panel.</param>
        /// <param name="stdDevStrokes">The standard deviation of the pen strokes.</param>
        /// <param name="minStrokes">The minimum number of pen strokes on a panel.</param>
        /// <param name="maxStrokes">The maximum number of pen strokes on a panel.</param>
        /// <param name="meanDataLength">The mean amount of data per panel.</param>
        /// <param name="stdDevDataLength">The standard deviation of the amount of data.</param>
        /// <param name="minDataLength">The minimum amount of data on a panel.</param>
        /// <param name="maxDataLength">The maximum amount of data on a panel.</param>
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
        /// Initializes a new instance of the File class.
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
        /// Gets the database identifier.
        /// </summary>
        /// <value>The database identifier.</value>
        public int Id
        {
            get { return this.id; }
        }

        /// <summary>
        /// Gets the date for the file.
        /// </summary>
        /// <value>The date for the file.</value>
        public int Classdate
        {
            get { return this.classdate; }
        }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        /// <value>The file name.</value>
        public string FileName
        {
            get { return this.fileName; }
        }

        /// <summary>
        /// Gets the mean number of pen strokes.
        /// </summary>
        /// <value>The mean number of pen strokes.</value>
        public double MeanStrokes
        {
            get { return this.meanStrokes; }
        }

        /// <summary>
        /// Gets the standard deviation of the pen strokes.
        /// </summary>
        /// <value>The standard deviation of the pen strokes.</value>
        public double StdDevStrokes
        {
            get { return this.stdDevStrokes; }
        }

        /// <summary>
        /// Gets the minimum number of pen strokes from the panels.
        /// </summary>
        /// <value>The minimum number of pen strokes from the panels.</value>
        public double MinStrokes
        {
            get { return this.minStrokes; }
        }

        /// <summary>
        /// Gets the maximum number of pen strokes from the panels.
        /// </summary>
        /// <value>The maximum number of pen strokes from the panels.</value>
        public double MaxStrokes
        {
            get { return this.maxStrokes; }
        }

        /// <summary>
        /// Gets the mean data length form the panels.
        /// </summary>
        /// <value>The mean data length from the panels.</value>
        public double MeanDataLength
        {
            get { return this.meanDataLength; }
        }

        /// <summary>
        /// Gets the standard deviation of the data length.
        /// </summary>
        /// <value>The standard deviation of the data length.</value>
        public double StdDevDataLength
        {
            get { return this.stdDevDataLength; }
        }

        /// <summary>
        /// Gets the minimum data length from the panels.
        /// </summary>
        /// <value>The minimum data length from the panels.</value>
        public double MinDataLength
        {
            get { return this.minDataLength; }
        }

        /// <summary>
        /// Gets the maximum data length from the panels.
        /// </summary>
        /// <value>The maximum data length from the panels.</value>
        public double MaxDataLength
        {
            get { return this.maxDataLength; }
        }
    }
}
