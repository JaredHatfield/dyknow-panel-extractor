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
        /// The list of keys.
        /// </summary>
        private List<Rect> keys;

        /// <summary>
        /// The list of answers for the given answer boxes.
        /// </summary>
        private Dictionary<Rect, string> answers;

        /// <summary>
        /// The list of alternates for the given answer boxes.
        /// </summary>
        private Dictionary<Rect, Collection<string>> alternates;

        /// <summary>
        /// A flag that indicates that this panel has been fully processed.
        /// </summary>
        private bool processed;

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelAnswer"/> class.
        /// </summary>
        internal PanelAnswer()
        {
            this.keys = new List<Rect>();
            this.answers = new Dictionary<Rect, string>();
            this.alternates = new Dictionary<Rect, Collection<string>>();
            this.processed = false;
        }

        /// <summary>
        /// The panel processed delegate.
        /// </summary>
        public delegate void PanelProcessedDelegate();

        /// <summary>
        /// Occurs when panel did process.
        /// </summary>
        public event PanelProcessedDelegate DidProcess;

        /// <summary>
        /// Gets a value indicating whether this instance is processed.
        /// </summary>
        /// <value><c>true</c> if this instance is processed; otherwise, <c>false</c>.</value>
        public bool IsProcessed
        {
            get { return this.processed; }
        }

        /// <summary>
        /// Gets the list of keys.
        /// </summary>
        /// <value>The list of keys.</value>
        public ReadOnlyCollection<Rect> Keys
        {
            get { return new ReadOnlyCollection<Rect>(this.keys); }
        }

        /// <summary>
        /// Adds the result.
        /// </summary>
        /// <param name="rect">The bounding rectangle.</param>
        /// <param name="recognized">The recognized string.</param>
        /// <param name="aac">The Analysis Alternate Collection.</param>
        internal void AddResult(Rect rect, string recognized, AnalysisAlternateCollection aac)
        {
            this.keys.Add(rect);
            this.answers.Add(rect, recognized);
            Collection<string> alt = new Collection<string>();
            for (int i = 0; i < aac.Count; i++)
            {
                alt.Add(aac[i].RecognizedString);
            }

            this.alternates.Add(rect, alt);
        }

        /// <summary>
        /// Gets the recognized string.
        /// </summary>
        /// <param name="rect">The key to lookup.</param>
        /// <returns>The recognized string.</returns>
        internal string GetRecognizedString(Rect rect)
        {
            return this.answers[rect];
        }

        /// <summary>
        /// Gets the alternate string.
        /// </summary>
        /// <param name="rect">The key to lookup.</param>
        /// <returns>The collection of alternative strings.</returns>
        internal Collection<string> GetAlternateString(Rect rect)
        {
            return this.alternates[rect];
        }

        /// <summary>
        /// Flags the procesed.
        /// </summary>
        internal void FlagProcesed()
        {
            this.processed = true;

            // Signal the event
            if (this.DidProcess != null)
            {
                this.DidProcess();
            }
        }
    }
}
