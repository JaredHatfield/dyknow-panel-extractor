// <copyright file="RecognitionWordTest.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Recognition test for a word.</summary>
namespace HandwritingAccuracy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Recognition test for a word.
    /// </summary>
    public class RecognitionWordTest : RecognitionTest
    {
        /// <summary>
        /// The word that is attempted to be recognized.
        /// </summary>
        private string target;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecognitionWordTest"/> class.
        /// </summary>
        /// <param name="word">The word to recognize.</param>
        public RecognitionWordTest(string word)
            : base()
        {
            this.target = word;
        }

        /// <summary>
        /// Gets the text for this test.
        /// </summary>
        /// <value>The text for this test.</value>
        public override string Text
        {
            get
            {
                return this.target;
            }
        }
    }
}
