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
    /// 
    /// </summary>
    public class DisplayPanelInfo
    {
        /// <summary>
        /// 
        /// </summary>
        private DateTime date;

        /// <summary>
        /// 
        /// </summary>
        private string filename;

        /// <summary>
        /// 
        /// </summary>
        private int slideNumber;

        /// <summary>
        /// 
        /// </summary>
        private int totalStrokeCount;

        /// <summary>
        /// 
        /// </summary>
        private int netStrokeCount;

        /// <summary>
        /// 
        /// </summary>
        private bool isBlank;

        /// <summary>
        /// 
        /// </summary>
        private string analysis;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="filename"></param>
        /// <param name="slideNumber"></param>
        /// <param name="totalStrokeCount"></param>
        /// <param name="netStrokeCount"></param>
        /// <param name="isBlank"></param>
        /// <param name="analysis"></param>
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
        /// 
        /// </summary>
        public DateTime Date
        {
            get { return this.date; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Filename
        {
            get { return this.filename; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SlideNumber
        {
            get { return this.slideNumber; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TotalStrokeCount
        {
            get { return this.totalStrokeCount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int NetStrokeCount
        {
            get { return this.netStrokeCount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsBlank
        {
            get { return this.isBlank; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Analysis
        {
            get { return this.analysis; }
        }
    }
}
