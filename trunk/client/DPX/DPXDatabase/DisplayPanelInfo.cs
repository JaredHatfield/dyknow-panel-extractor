namespace DPXDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DisplayPanelInfo
    {
        private DateTime date;
        private string filename;
        private int slideNumber;
        private int totalStrokeCount;
        private int netStrokeCount;
        private bool isBlank;
        private string analysis;

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

        public DateTime Date
        {
            get { return this.date; }
        }

        public string Filename
        {
            get { return this.filename; }
        }

        public int SlideNumber
        {
            get { return this.slideNumber; }
        }

        public int TotalStrokeCount
        {
            get { return this.totalStrokeCount; }
        }

        public int NetStrokeCount
        {
            get { return this.netStrokeCount; }
        }

        public bool IsBlank
        {
            get { return this.isBlank; }
        }

        public string Analysis
        {
            get { return this.analysis; }
        }
    }
}
