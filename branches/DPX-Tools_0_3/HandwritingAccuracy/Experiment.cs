// <copyright file="Experiment.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The representation of a Experiment.</summary>
namespace HandwritingAccuracy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The representation of a Experiment.
    /// </summary>
    public class Experiment
    {
        /// <summary>
        /// The pid value.
        /// </summary>
        private int pid;

        /// <summary>
        /// The participant id.
        /// </summary>
        private int participant;

        /// <summary>
        /// The tablet id.
        /// </summary>
        private int tablet;

        /// <summary>
        /// The time value.
        /// </summary>
        private DateTime time;

        /// <summary>
        /// Initializes a new instance of the <see cref="Experiment"/> class.
        /// </summary>
        /// <param name="pid">The pid value.</param>
        /// <param name="participant">The participant value.</param>
        /// <param name="tablet">The tablet value.</param>
        /// <param name="time">The time value.</param>
        public Experiment(int pid, int participant, int tablet, DateTime time)
        {
            this.pid = pid;
            this.participant = participant;
            this.tablet = tablet;
            this.time = time;
        }

        /// <summary>
        /// Gets or sets the PID.
        /// </summary>
        /// <value>The PID value.</value>
        internal int PID
        {
            get { return this.pid; }
            set { this.pid = value; }
        }

        /// <summary>
        /// Gets or sets the participant.
        /// </summary>
        /// <value>The participant.</value>
        internal int Participant
        {
            get { return this.participant; }
            set { this.participant = value; }
        }

        /// <summary>
        /// Gets or sets the tablet.
        /// </summary>
        /// <value>The tablet.</value>
        internal int Tablet
        {
            get { return this.tablet; }
            set { this.tablet = value; }
        }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time value.</value>
        internal DateTime Time
        {
            get { return this.time; }
            set { this.time = value; }
        }
    }
}
