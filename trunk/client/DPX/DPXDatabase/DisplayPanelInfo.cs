// <copyright file="DisplayPanelInfo.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Object for DisplayPanelInfo.
    /// </summary>
    public class DisplayPanelInfo
    {
        /// <summary>
        /// The date for the file.
        /// </summary>
        private DateTime date;

        /// <summary>
        /// The file name when imported.
        /// </summary>
        private string filename;

        /// <summary>
        /// The slide number.
        /// </summary>
        private int slideNumber;

        /// <summary>
        /// The total number of pen strokes.
        /// </summary>
        private int totalStrokeCount;

        /// <summary>
        /// The net number of pen strokes.
        /// </summary>
        private int netStrokeCount;

        /// <summary>
        /// Flag that denotes if the panel is blank.
        /// </summary>
        private bool isBlank;

        /// <summary>
        /// The analysis of the panel.
        /// </summary>
        private string analysis;

        /// <summary>
        /// Initializes a new instance of the DisplayPanelInfo class.
        /// </summary>
        /// <param name="date">The date of the panel.</param>
        /// <param name="filename">The file name.</param>
        /// <param name="slideNumber">The slide number.</param>
        /// <param name="totalStrokeCount">The total number of pen strokes.</param>
        /// <param name="netStrokeCount">The net number of pen strokes.</param>
        /// <param name="isBlank">Flag that determines if the panel is blank.</param>
        /// <param name="analysis">The analysis of the panel.</param>
        public DisplayPanelInfo(
            DateTime date, 
            string filename, 
            int slideNumber, 
            int totalStrokeCount,
            int netStrokeCount, 
            bool isBlank, 
            string analysis)
        {
            this.date = date;
            this.filename = filename;
            this.slideNumber = slideNumber;
            this.totalStrokeCount = totalStrokeCount;
            this.netStrokeCount = netStrokeCount;
            this.isBlank = isBlank;
            this.analysis = analysis;
        }

        /// <summary>
        /// Gets the date of the panel.
        /// </summary>
        /// <value>The date of the panel.</value>
        public DateTime Date
        {
            get { return this.date; }
        }
        
        /// <summary>
        /// Gets the file name.
        /// </summary>
        /// <value>The file name.</value>
        public string Filename
        {
            get { return this.filename; }
        }

        /// <summary>
        /// Gets the slide number.
        /// </summary>
        /// <value>The slide number.</value>
        public int SlideNumber
        {
            get { return this.slideNumber; }
        }

        /// <summary>
        /// Gets the total pen stroke count.
        /// </summary>
        /// <value>The total pen stroke count.</value>
        public int TotalStrokeCount
        {
            get { return this.totalStrokeCount; }
        }

        /// <summary>
        /// Gets the net number of pen strokes.
        /// </summary>
        /// <value>The net number of pen strokes.</value>
        public int NetStrokeCount
        {
            get { return this.netStrokeCount; }
        }

        /// <summary>
        /// Gets a value indicating whether the panel is blank.
        /// </summary>
        /// <value>A value indicating whether the panel is blank.</value>
        public bool IsBlank
        {
            get { return this.isBlank; }
        }

        /// <summary>
        /// Gets the analysis of the panel.
        /// </summary>
        /// <value>The analysis of the panel.</value>
        public string Analysis
        {
            get { return this.analysis; }
        }
    }
}
