// <copyright file="TabletPC.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The representation of a TabletPC.</summary>
namespace HandwritingAccuracy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The representation of a Tablet PC.
    /// </summary>
    public class TabletPC
    {
        /// <summary>
        /// The primary id.
        /// </summary>
        private int pid;

        /// <summary>
        /// The manafacturer.
        /// </summary>
        private string manufacturer;

        /// <summary>
        /// The model.
        /// </summary>
        private string model;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabletPC"/> class.
        /// </summary>
        /// <param name="pid">The pid value.</param>
        /// <param name="manafacturer">The manafacturer value.</param>
        /// <param name="model">The model value.</param>
        internal TabletPC(int pid, string manafacturer, string model)
        {
            this.pid = pid;
            this.manufacturer = manafacturer;
            this.model = model;
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
        /// Gets or sets the manafacturer.
        /// </summary>
        /// <value>The manafacturer.</value>
        public string Manafacturer
        {
            get { return this.manufacturer; }
            set { this.manufacturer = value; }
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public string Model
        {
            get { return this.model; }
            set { this.model = value; }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.manufacturer + " (" + this.model + ")";
        }
    }
}
