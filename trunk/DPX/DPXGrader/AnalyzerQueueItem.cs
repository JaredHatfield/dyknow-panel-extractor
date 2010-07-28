// <copyright file="AnalyzerQueueItem.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>A queue item that needs to be processed.</summary>
namespace DPXGrader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A queue item that needs to be processed.
    /// </summary>
    internal class AnalyzerQueueItem
    {
        /// <summary>
        /// The number of the panel to process.
        /// </summary>
        private int num;

        /// <summary>
        /// The total number of panels.
        /// </summary>
        private int goal;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyzerQueueItem"/> class.
        /// </summary>
        /// <param name="num">The number.</param>
        /// <param name="goal">The goal number.</param>
        public AnalyzerQueueItem(int num, int goal)
        {
            this.num = num;
            this.goal = goal;
        }

        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <value>The number.</value>
        public int Num
        {
            get { return this.num; }
        }

        /// <summary>
        /// Gets the goal number.
        /// </summary>
        /// <value>The goal number.</value>
        public int Goal
        {
            get { return this.goal; }
        }
    }
}
