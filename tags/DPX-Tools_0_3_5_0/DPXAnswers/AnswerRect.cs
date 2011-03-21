// <copyright file="AnswerRect.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The Answer Rectangle.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using ClusterLibraryCore;
    using GradeLibrary;

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
        /// The cluster of answers.
        /// </summary>
        private ICluster<IAnswer, GroupData> cluster;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerRect"/> class.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <param name="index">The box index.</param>
        public AnswerRect(Rect rect, int index)
        {
            this.rect = rect;
            this.index = index;
            this.cluster = new Cluster<IAnswer, GroupData>(new GroupData());
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
        /// Gets the cluster.
        /// </summary>
        /// <value>The cluster.</value>
        public ICluster<IAnswer, GroupData> Cluster
        {
            get { return this.cluster; }
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
