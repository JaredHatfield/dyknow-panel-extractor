// <copyright file="RecognitionDoubleTest.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Recognition test for a double.</summary>
namespace DPXGrader.Accuracy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Recognition test for a double.
    /// </summary>
    public class RecognitionDoubleTest : RecognitionTest
    {
        /// <summary>
        /// The number that is being tested.
        /// </summary>
        private double target;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecognitionDoubleTest"/> class.
        /// </summary>
        public RecognitionDoubleTest()
            : base()
        {
            this.target = Math.Round(RecognitionTest.Random.NextDouble() * 10, 2);
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
