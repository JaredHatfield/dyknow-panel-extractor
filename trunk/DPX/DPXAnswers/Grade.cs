// <copyright file="Grade.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The different state for a grade.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The different state for a grade.
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
        /// The answer is correct.
        /// </summary>
        CORRECT,

        /// <summary>
        /// The answer is incorrect.
        /// </summary>
        INCORRECT
    }
}
