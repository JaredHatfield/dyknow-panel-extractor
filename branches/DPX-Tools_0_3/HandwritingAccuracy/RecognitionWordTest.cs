// <copyright file="RecognitionWordTest.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Recognition test for a word.</summary>
namespace HandwritingAccuracy
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Resources;
    using System.Text;

    /// <summary>
    /// Recognition test for a word.
    /// </summary>
    public class RecognitionWordTest : RecognitionTest
    {
        /// <summary>
        /// The dictionary of words to choose from.
        /// </summary>
        private static string[] dictionary;

        /// <summary>
        /// The random number generator.
        /// </summary>
        private static Random rand = new Random();

        /// <summary>
        /// The word that is attempted to be recognized.
        /// </summary>
        private string target;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecognitionWordTest"/> class.
        /// </summary>
        public RecognitionWordTest()
            : base()
        {
            // If the dictionary has not been loaded, put it into memory.
            if (RecognitionWordTest.dictionary == null)
            {
                RecognitionWordTest.dictionary = HandwritingAccuracy.Properties.Resources.SimpleDictionary.Split('\n');
                for (int i = 0; i < RecognitionWordTest.dictionary.Length; i++)
                {
                    if (RecognitionWordTest.dictionary[i].Contains('\r'))
                    {
                        RecognitionWordTest.dictionary[i] = RecognitionWordTest.dictionary[i].Replace("\r", string.Empty);
                    }
                }
            }

            // Pick a random word
            this.target = string.Empty;
            while (this.target.Length < 6)
            {
                try
                {
                    int index = RecognitionWordTest.rand.Next(RecognitionWordTest.dictionary.Length);
                    this.target = RecognitionWordTest.dictionary[index];
                }
                catch
                {
                    // Nothing to see here... move along
                }
            }
        }

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

        /// <summary>
        /// Gets the name of the experiment.
        /// </summary>
        /// <value>The name of the experiment.</value>
        public override string ExperimentName
        {
            get { return "Word"; }
        }
    }
}
