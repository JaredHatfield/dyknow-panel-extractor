// <copyright file="BoxAnalysis.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The analysis results of an answer box.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using GradeLibrary;

    /// <summary>
    /// The analysis results of an answer box.
    /// </summary>
    public class BoxAnalysis : IAnswer
    {
        /// <summary>
        /// The recognized string for the answer box.
        /// </summary>
        private string answer;

        /// <summary>
        /// The alternate strings for tha answer box.
        /// </summary>
        private Collection<string> alternates;

        /// <summary>
        /// The index for the panel.
        /// </summary>
        private int panelIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoxAnalysis"/> class.
        /// </summary>
        /// <param name="answer">The answer.</param>
        /// <param name="alternates">The alternates.</param>
        /// <param name="panelIndex">Index of the panel.</param>
        internal BoxAnalysis(string answer, Collection<string> alternates, int panelIndex)
        {
            this.answer = answer;
            this.alternates = alternates;
            this.panelIndex = panelIndex;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="BoxAnalysis"/> class from being created.
        /// </summary>
        private BoxAnalysis()
        {
        }

        /// <summary>
        /// Gets the answer.
        /// </summary>
        /// <value>The answer.</value>
        public string Answer
        {
            get { return this.answer; }
        }

        /// <summary>
        /// Gets the alternates.
        /// </summary>
        /// <value>The alternates.</value>
        public ReadOnlyCollection<string> Alternates
        {
            get { return new ReadOnlyCollection<string>(this.alternates); }
        }

        /// <summary>
        /// Gets the index of the panel.
        /// </summary>
        /// <value>The index of the panel.</value>
        public int PanelIndex
        {
            get { return this.panelIndex; }
        }

        /// <summary>
        /// Gets the string representation of a BoxGrade.
        /// </summary>
        /// <param name="grade">The grade to convert.</param>
        /// <returns>The string represntation.</returns>
        public static string BoxGradeString(Grade grade)
        {
            switch (grade)
            {
                case Grade.NOTSET:
                    return "?";
                case Grade.CORRECT:
                    return "Correct";
                case Grade.INCORRECT:
                    return "Incorrect";
                case Grade.INVALID:
                    return "?";
            }

            return string.Empty;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            BoxAnalysis ba = new BoxAnalysis();
            ba.answer = this.answer.Clone() as string;
            ba.alternates = new Collection<string>();
            for (int i = 0; i < this.alternates.Count; i++)
            {
                ba.alternates.Add(this.alternates[i].Clone() as string);
            }

            ba.panelIndex = this.panelIndex;
            return ba;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            try
            {
                BoxAnalysis ba = obj as BoxAnalysis;

                // Equivalence is based on the recognized answer
                return this.answer.Equals(ba.answer);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
