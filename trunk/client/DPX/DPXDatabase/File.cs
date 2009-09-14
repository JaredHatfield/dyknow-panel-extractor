using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPXDatabase
{
    public class File
    {
        private int id;
        private int classdate;
        private String fileName;
        private Double meanStrokes;
        private Double stdDevStrokes;
        private Double minStrokes;
        private Double maxStrokes;
        private Double meanDataLength;
        private Double stdDevDataLength;
        private Double minDataLength;
        private Double maxDataLength;

        public int Id
        {
            get { return id; }
        }
        public int Classdate
        {
            get { return classdate; }
        }
        public String FileName
        {
            get { return fileName; }
        }
        public Double MeanStrokes
        {
            get { return meanStrokes; }
        }
        public Double StdDevStrokes
        {
            get { return stdDevStrokes; }
        }
        public Double MinStrokes
        {
            get { return minStrokes; }
        }
        public Double MaxStrokes
        {
            get { return maxStrokes; }
        }
        public Double MeanDataLength
        {
            get { return meanDataLength; }
        }
        public Double StdDevDataLength
        {
            get { return stdDevDataLength; }
        }
        public Double MinDataLength
        {
            get { return minDataLength; }
        }
        public Double MaxDataLength
        {
            get { return maxDataLength; }
        }


        public File(int id, int classdate, String fileName, Double meanStrokes, Double stdDevStrokes,
            Double minStrokes, Double maxStrokes, Double meanDataLength, Double stdDevDataLength,
            Double minDataLength, Double maxDataLength)
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

        public File(int classdate, String fileName, Double meanStrokes, Double stdDevStrokes, 
            Double minStrokes, Double maxStrokes, Double meanDataLength, Double stdDevDataLength, 
            Double minDataLength, Double maxDataLength)
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
            this.fileName = "";
            this.meanStrokes = -1;
            this.stdDevStrokes = -1;
            this.minStrokes = -1;
            this.maxStrokes = -1;
            this.meanDataLength = -1;
            this.stdDevDataLength = -1;
            this.minDataLength = -1;
            this.maxDataLength = -1;
        }

    }
}
