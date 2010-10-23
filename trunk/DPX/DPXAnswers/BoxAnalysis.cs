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

    /// <summary>
    /// The analysis results of an answer box.
    /// </summary>
    internal class BoxAnalysis
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
        /// The flag indicating the answer is correct.
        /// </summary>
        private Grade boxGrade;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoxAnalysis"/> class.
        /// </summary>
        /// <param name="answer">The answer.</param>
        /// <param name="alternates">The alternates.</param>
        public BoxAnalysis(string answer, Collection<string> alternates)
        {
            this.answer = answer;
            this.alternates = alternates;
            this.boxGrade = Grade.NOTSET;
        }

        /// <summary>
        /// The different settings for each grade.
        /// </summary>
        public enum Grade
        {
            /// <summary>
            /// The box is not valid.
            /// </summary>
            INVALID,

            /// <summary>
            /// The answer has not been graded.
            /// </summary>
            NOTSET,

            /// <summary>
            /// The user set the answer as correct.
            /// </summary>
            SETCORRECT,

            /// <summary>
            /// The user set the answer as incorrect.
            /// </summary>
            SETINCORRECT,

            /// <summary>
            /// The system set the answer as correct.
            /// </summary>
            AUTOCORRECT,

            /// <summary>
            /// The system set the answer as incorrect.
            /// </summary>
            AUTOINCORRECT
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
        /// Gets or sets the box grade.
        /// </summary>
        /// <value>The box grade.</value>
        public Grade BoxGrade
        {
            get { return this.boxGrade; }
            set { this.boxGrade = value; }
        }

        /// <summary>
        /// Gets the string representation of a BoxGrade.
        /// </summary>
        /// <param name="grade">The grade to convert.</param>
        /// <returns>The string represntation.</returns>
        public static string BoxGradeString(BoxAnalysis.Grade grade)
        {
            switch (grade)
            {
                case BoxAnalysis.Grade.NOTSET:
                    return "?";
                case BoxAnalysis.Grade.AUTOCORRECT:
                    return "Correct (Auto)";
                case BoxAnalysis.Grade.SETCORRECT:
                    return "Correct";
                case BoxAnalysis.Grade.AUTOINCORRECT:
                    return "Incorrect (Auto)";
                case BoxAnalysis.Grade.SETINCORRECT:
                    return "Incorrect";
                case BoxAnalysis.Grade.INVALID:
                    return "?";
            }

            return string.Empty;
        }
    }
}
