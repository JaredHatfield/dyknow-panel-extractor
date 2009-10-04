using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPXDatabase
{
    public class DisplayPanelInfo
    {
        private DateTime date;
        private String filename;
        private int slideNumber;
        private int totalStrokeCount;
        private int netStrokeCount;
        private Boolean isBlank;
        private String analysis;

        public DateTime Date
        {
            get { return date; }
        }
        public String Filename
        {
            get { return System.IO.Path.GetFileNameWithoutExtension( filename); }
        }
        public int SlideNumber
        {
            get { return slideNumber; }
        }
        public int TotalStrokeCount
        {
            get { return totalStrokeCount; }
        }
        public int NetStrokeCount
        {
            get { return netStrokeCount; }
        }
        public Boolean IsBlank
        {
            get { return isBlank; }
        }
        public String Analysis
        {
            get { return analysis; }
        }

        public DisplayPanelInfo(DateTime date, String filename, int slideNumber, int totalStrokeCount,
            int netStrokeCount, Boolean isBlank, String analysis)
        {
            this.date = date;
            this.filename = filename;
            this.slideNumber = slideNumber;
            this.totalStrokeCount = totalStrokeCount;
            this.netStrokeCount = netStrokeCount;
            this.isBlank = isBlank;
            this.analysis = analysis;
        }
    }
}
