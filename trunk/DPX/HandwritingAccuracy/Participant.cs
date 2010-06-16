// <copyright file="Participant.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The representation of a Participant.</summary>
namespace HandwritingAccuracy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The representation of a Participant.
    /// </summary>
    internal class Participant
    {
        /// <summary>
        /// The pid value.
        /// </summary>
        private int pid;

        /// <summary>
        /// The first name value.
        /// </summary>
        private string firstname;

        /// <summary>
        /// The last name value.
        /// </summary>
        private string lastname;

        /// <summary>
        /// The handedness value.
        /// </summary>
        private string handedness;

        /// <summary>
        /// The gender value.
        /// </summary>
        private string gender;

        /// <summary>
        /// The own value.
        /// </summary>
        private int own;

        /// <summary>
        /// The use value.
        /// </summary>
        private int use;

        /// <summary>
        /// Initializes a new instance of the <see cref="Participant"/> class.
        /// </summary>
        /// <param name="pid">The pid value.</param>
        /// <param name="firstname">The firstname value.</param>
        /// <param name="lastname">The lastname value.</param>
        /// <param name="handedness">The handedness value.</param>
        /// <param name="gender">The gender value.</param>
        /// <param name="own">The own value.</param>
        /// <param name="use">The use value.</param>
        internal Participant(int pid, string firstname, string lastname, string handedness, string gender, int own, int use)
        {
            this.pid = pid;
            this.firstname = firstname;
            this.lastname = lastname;
            this.handedness = handedness;
            this.gender = gender;
            this.own = own;
            this.use = use;
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
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name value.</value>
        public string FirstName
        {
            get { return this.firstname; }
            set { this.firstname = value; }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name value.</value>
        private string LastName
        {
            get { return this.lastname; }
            set { this.lastname = value; }
        }

        /// <summary>
        /// Gets or sets the handedness.
        /// </summary>
        /// <value>The handedness value.</value>
        private string Handedness
        {
            get { return this.handedness; }
            set { this.handedness = value; }
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender value.</value>
        private string Gender
        {
            get { return this.gender; }
            set { this.gender = value; }
        }

        /// <summary>
        /// Gets or sets the own.
        /// </summary>
        /// <value>The own value.</value>
        private int Own
        {
            get { return this.own; }
            set { this.own = value; }
        }

        /// <summary>
        /// Gets or sets the use.
        /// </summary>
        /// <value>The use value.</value>
        private int Use
        {
            get { return this.use; }
            set { this.use = value; }
        }
    }
}
