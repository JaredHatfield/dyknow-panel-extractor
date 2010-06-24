// <copyright file="KnownMistake.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>A structure that represents a known mistake in the serialization difference.</summary>
namespace DPXPreview
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A structure that represents a known mistake in the serialization difference.
    /// </summary>
    public struct KnownMistake
    {
        /// <summary>
        /// The diff operation.
        /// </summary>
        public DiffMatchPatch.Operation Operation;

        /// <summary>
        /// The diff value.
        /// </summary>
        public string Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="KnownMistake"/> struct.
        /// </summary>
        /// <param name="operation">The diff operation.</param>
        /// <param name="value">The string value.</param>
        public KnownMistake(DiffMatchPatch.Operation operation, string value)
        {
            this.Operation = operation;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KnownMistake"/> struct.
        /// </summary>
        /// <param name="diff">The diff to use.</param>
        public KnownMistake(DiffMatchPatch.Diff diff)
        {
            this.Operation = diff.operation;
            this.Value = diff.text;
        }
    }
}
