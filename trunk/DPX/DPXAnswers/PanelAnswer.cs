// <copyright file="PanelAnswer.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The results from analyzing the answer boxes.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Ink;

    /// <summary>
    /// The results from analyzing the answer boxes.
    /// </summary>
    internal class PanelAnswer
    {
        /// <summary>
        /// The list of answers for the given answer boxes.
        /// </summary>
        private Dictionary<Rect, string> answers;

        /// <summary>
        /// The list of alternates for the given answer boxes.
        /// </summary>
        private Dictionary<Rect, Collection<string>> alternates;

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelAnswer"/> class.
        /// </summary>
        internal PanelAnswer()
        {
            this.answers = new Dictionary<Rect, string>();
            this.alternates = new Dictionary<Rect, Collection<string>>();
        }

        /// <summary>
        /// Adds the result.
        /// </summary>
        /// <param name="rect">The bounding rectangle.</param>
        /// <param name="recognized">The recognized string.</param>
        /// <param name="aac">The Analysis Alternate Collection.</param>
        internal void AddResult(Rect rect, string recognized, AnalysisAlternateCollection aac)
        {
            this.answers.Add(rect, recognized);
            Collection<string> alt = new Collection<string>();
            for (int i = 0; i < aac.Count; i++)
            {
                alt.Add(aac[i].RecognizedString);
            }

            this.alternates.Add(rect, alt);
        }
    }
}
