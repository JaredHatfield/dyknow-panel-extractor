// <copyright file="Alternative.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The representation of a Alternative.</summary>
namespace HandwritingAccuracy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The representation of a Alternative.
    /// </summary>
    public class Alternative
    {
        /// <summary>
        /// The pid value.
        /// </summary>
        private int pid;

        /// <summary>
        /// The run value.
        /// </summary>
        private int run;

        /// <summary>
        /// The recognized value.
        /// </summary>
        private string recognized;

        /// <summary>
        /// The match value.
        /// </summary>
        private int match;

        /// <summary>
        /// Initializes a new instance of the <see cref="Alternative"/> class.
        /// </summary>
        /// <param name="pid">The pid value.</param>
        /// <param name="run">The run value.</param>
        /// <param name="recognized">The recognized value.</param>
        /// <param name="match">The match value.</param>
        public Alternative(int pid, int run, string recognized, int match)
        {
            this.pid = pid;
            this.run = run;
            this.recognized = recognized;
            this.match = match;
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
        /// Gets or sets the run.
        /// </summary>
        /// <value>The run value.</value>
        public int Run
        {
            get { return this.run; }
            set { this.run = value; }
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
        /// Gets or sets the match.
        /// </summary>
        /// <value>The match.</value>
        public int Match
        {
            get { return this.match; }
            set { this.match = value; }
        }
    }
}
