// <copyright file="Run.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The representation of a Run.</summary>
namespace HandwritingAccuracy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The representation of a Run.
    /// </summary>
    public class Run
    {
        /// <summary>
        /// The pid value.
        /// </summary>
        private int pid;

        /// <summary>
        /// The experiment value.
        /// </summary>
        private int experiment;

        /// <summary>
        /// The number value.
        /// </summary>
        private int number;

        /// <summary>
        /// The type value.
        /// </summary>
        private string type;

        /// <summary>
        /// The value value.
        /// </summary>
        private string value;

        /// <summary>
        /// The recognized value.
        /// </summary>
        private string recognized;

        /// <summary>
        /// The content value.
        /// </summary>
        private string content;

        /// <summary>
        /// The match value.
        /// </summary>
        private int match;

        /// <summary>
        /// The time value.
        /// </summary>
        private DateTime time;

        /// <summary>
        /// Initializes a new instance of the <see cref="Run"/> class.
        /// </summary>
        /// <param name="pid">The pid value.</param>
        /// <param name="experiment">The experiment value.</param>
        /// <param name="number">The number value.</param>
        /// <param name="type">The type value.</param>
        /// <param name="value">The value value.</param>
        /// <param name="recognized">The recognized value.</param>
        /// <param name="content">The content value.</param>
        /// <param name="match">The match value.</param>
        /// <param name="time">The time value.</param>
        public Run(int pid, int experiment, int number, string type, string value, string recognized, string content, int match, DateTime time)
        {
            this.pid = pid;
            this.experiment = experiment;
            this.number = number;
            this.type = type;
            this.value = value;
            this.recognized = recognized;
            this.content = content;
            this.match = match;
            this.time = time;
        }

        /// <summary>
        /// Gets or sets the PID.
        /// </summary>
        /// <value>The PID value.</value>
        public int PID
        {
            get { return this.pid; }
            set { this.pid = value; }
        }

        /// <summary>
        /// Gets or sets the experiment.
        /// </summary>
        /// <value>The experiment.</value>
        public int Experiment
        {
            get { return this.experiment; }
            set { this.experiment = value; }
        }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public int Number
        {
            get { return this.number; }
            set { this.number = value; }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type value.</value>
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Gets or sets the recognized.
        /// </summary>
        /// <value>The recognized.</value>
        public string Recognized
        {
            get { return this.recognized; }
            set { this.recognized = value; }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }

        /// <summary>
        /// Gets or sets the match.
        /// </summary>
        /// <value>The match.</value>
        public int Match
        {
            get { return this.match; }
            set { this.match = value; }
        }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time value.</value>
        private DateTime Time
        {
            get { return this.time; }
            set { this.time = value; }
        }
    }
}
