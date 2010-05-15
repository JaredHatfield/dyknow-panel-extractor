// <copyright file="RosterFile.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Reads in a class roster file to be parsed and added to the list of students.</summary>
namespace DPXDatabase
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Reads in a class roster file to be parsed and added to the list of students.
    /// </summary>
    public class RosterFile
    {
        /// <summary>
        /// The list of string tokens parsed from the file.
        /// </summary>
        private ObservableCollection<string[]> parsedData;

        /// <summary>
        /// Initializes a new instance of the RosterFile class for a specified file.
        /// </summary>
        /// <param name="file">Path of file to read.</param>
        public RosterFile(string file)
        {
            this.parsedData = new ObservableCollection<string[]>();
            try
            {
                using (StreamReader read = new StreamReader(file))
                {
                    string line;
                    string[] row;

                    while ((line = read.ReadLine()) != null)
                    {
                        row = line.Split(',');
                        this.parsedData.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                // Something bad happened
                e.ToString();
            }
        }

        /// <summary>
        /// Gets a specific record as parsed from the file.
        /// </summary>
        /// <value>Parsed data from the file.</value>
        public ObservableCollection<string[]> ParsedData
        {
            get { return this.parsedData; }
        }

        /// <summary>
        /// Gets the maximum number of columns for all of the records.
        /// </summary>
        /// <value>Number of columns.</value>
        public int NumColumns
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.parsedData.Count; i++)
                {
                    int n = this.parsedData[i].Length;
                    if (n > num)
                    {
                        num = n;
                    }
                }

                return num;
            }
        }

        /// <summary>
        /// Gets the number of records.
        /// </summary>
        /// <value>The number of records.</value>
        public int Count
        {
            get { return this.parsedData.Count; }
        }
    }
}
