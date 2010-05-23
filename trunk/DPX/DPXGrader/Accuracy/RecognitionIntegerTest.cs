// <copyright file="RecognitionIntegerTest.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Recognition test for an integer.</summary>
namespace DPXGrader.Accuracy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Recognition test for an integer.
    /// </summary>
    public class RecognitionIntegerTest : RecognitionTest
    {
        /// <summary>
        /// The number that is being tested.
        /// </summary>
        private int target;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecognitionIntegerTest"/> class.
        /// </summary>
        public RecognitionIntegerTest()
            : base()
        {
            this.target = RecognitionTest.Random.Next(0, 101);
        }

        /// <summary>
        /// Gets the text for this test.
        /// </summary>
        /// <value>The text for this test.</value>
        public override string Text
        {
            get
            {
                return this.target.ToString();
            }
        }
    }
}
