// <copyright file="PanelAnswer.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The results from analyzing the answer boxes.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Ink;
    using GradeLibrary;

    /// <summary>
    /// The results from analyzing the answer boxes.
    /// </summary>
    internal class PanelAnswer
    {
        /// <summary>
        /// The AnswerRectFactory.
        /// </summary>
        private AnswerRectFactory answerRectFactory;

        /// <summary>
        /// The list of keys.
        /// </summary>
        private List<AnswerRect> keys;

        /// <summary>
        /// The list of analyzed answer boxes.
        /// </summary>
        private Dictionary<Rect, BoxAnalysis> answers;

        /// <summary>
        /// A flag that indicates that this panel has been fully processed.
        /// </summary>
        private bool processed;

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelAnswer"/> class.
        /// </summary>
        /// <param name="answerRectFactory">The AnswerRectFactory.</param>
        internal PanelAnswer(AnswerRectFactory answerRectFactory)
        {
            this.answerRectFactory = answerRectFactory;
            this.keys = new List<AnswerRect>();
            this.answers = new Dictionary<Rect, BoxAnalysis>();
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
        public ReadOnlyCollection<AnswerRect> Keys
        {
            get { return new ReadOnlyCollection<AnswerRect>(this.keys); }
        }

        /// <summary>
        /// Transforms the specified rectangle.
        /// </summary>
        /// <param name="rect">The rectangle to transform.</param>
        /// <param name="width">The target width.</param>
        /// <param name="height">The target height.</param>
        /// <returns>The transformed rectangle.</returns>
        internal static Rect Transform(Rect rect, double width, double height)
        {
            double x = width / (double)AnswerProcessQueueItem.DefaultWidth;
            double y = height / (double)AnswerProcessQueueItem.DefaultHeight;
            Rect r = new Rect(rect.Location, rect.Size);
            r.Scale(x, y);
            return r;
        }

        /// <summary>
        /// Adds the result.
        /// </summary>
        /// <param name="index">The index of the panel.</param>
        /// <param name="rect">The bounding rectangle.</param>
        /// <param name="recognized">The recognized string.</param>
        /// <param name="aac">The Analysis Alternate Collection.</param>
        /// <param name="img">The image thumbnail.</param>
        internal void AddResult(int index, Rect rect, string recognized, AnalysisAlternateCollection aac, Image img)
        {
            // We need to lock on the factory because this action needs to be atomic
            lock (this.answerRectFactory)
            {
                AnswerRect ar = this.answerRectFactory.GetAnswerRect(rect);
                Collection<string> alt = new Collection<string>();
                for (int i = 0; i < aac.Count; i++)
                {
                    alt.Add(aac[i].RecognizedString);
                }

                BoxAnalysis ba = new BoxAnalysis(recognized, alt, index, img);
                ar.Panels.AddValueDynamic(ba);
                this.answers.Add(rect, ba);
                this.keys.Add(ar);
            }
        }

        /// <summary>
        /// Gets the recognized string.
        /// </summary>
        /// <param name="rect">The key to lookup.</param>
        /// <returns>The recognized string.</returns>
        internal string GetRecognizedString(AnswerRect rect)
        {
            try
            {
                return this.answers[rect.Area].Answer;
            }
            catch (KeyNotFoundException)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the alternate string.
        /// </summary>
        /// <param name="rect">The key to lookup.</param>
        /// <returns>The collection of alternative strings.</returns>
        internal ReadOnlyCollection<string> GetAlternateString(AnswerRect rect)
        {
            try
            {
                return this.answers[rect.Area].Alternates;
            }
            catch (KeyNotFoundException)
            {
                return new ReadOnlyCollection<string>(new Collection<string>());
            }
        }

        /// <summary>
        /// Gets the answer.
        /// </summary>
        /// <param name="rect">The key to lookup.</param>
        /// <returns>The answer to the panel.</returns>
        internal Grade GetAnswer(AnswerRect rect)
        {
            try
            {
                BoxAnalysis ba = this.answers[rect.Area];
                return rect.Panels.GetGroup(ba).Label.Grade;
            }
            catch (KeyNotFoundException)
            {
                return Grade.INVALID;
            }
        }

        /// <summary>
        /// Gets the box analysis.
        /// </summary>
        /// <param name="rect">The rect to locate.</param>
        /// <returns>The BoxAnalysis for the given region.</returns>
        internal BoxAnalysis GetBoxAnalysis(AnswerRect rect)
        {
            try
            {
                return this.answers[rect.Area];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
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
