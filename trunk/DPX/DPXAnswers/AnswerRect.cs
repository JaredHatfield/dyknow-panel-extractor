// <copyright file="AnswerRect.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The Answer Rectangle.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using ClusterLibraryCore;

    /// <summary>
    /// The Answer Rectangle.
    /// </summary>
    internal class AnswerRect
    {
        /// <summary>
        /// The rectangle.
        /// </summary>
        private Rect rect;

        /// <summary>
        /// The box index.
        /// </summary>
        private int index;

        /// <summary>
        /// The clustering algorithm used by the algorithm.
        /// </summary>
        private ICluster<BoxAnalysis, Grade> panels;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerRect"/> class.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <param name="index">The box index.</param>
        public AnswerRect(Rect rect, int index)
        {
            this.rect = rect;
            this.index = index;
            this.panels = new Cluster<BoxAnalysis, Grade>(Grade.NOTSET);
        }

        /// <summary>
        /// Gets the rectangle area.
        /// </summary>
        /// <value>The rectangle area.</value>
        public Rect Area
        {
            get { return this.rect; }
        }

        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index
        {
            get { return this.index; }
        }

        /// <summary>
        /// Gets the panels.
        /// </summary>
        /// <value>The panels.</value>
        public ICluster<BoxAnalysis, Grade> Panels
        {
            get { return this.panels; }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return "Box " + this.index;
        }
    }
}
