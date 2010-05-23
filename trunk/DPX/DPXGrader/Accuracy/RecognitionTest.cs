// <copyright file="RecognitionTest.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The base class for a recognition test.</summary>
namespace DPXGrader.Accuracy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Ink;

    /// <summary>
    /// The base class for a recognition test.
    /// </summary>
    public abstract class RecognitionTest
    {
        /// <summary>
        /// The random number generator.
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// The collection of strokes.
        /// </summary>
        private StrokeCollection strokes;

        /// <summary>
        /// A flag to indicate if the analyis of the ink strokes has run.
        /// </summary>
        private bool analyisTestDidRun;

        /// <summary>
        /// The recognized text.
        /// </summary>
        private string recognizedText;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecognitionTest"/> class.
        /// </summary>
        public RecognitionTest()
        {
            this.strokes = new StrokeCollection();
            this.strokes.StrokesChanged += new StrokeCollectionChangedEventHandler(this.StrokeCollectionChanged);
            this.analyisTestDidRun = false;
        }

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        public static Random Random
        {
            get { return RecognitionTest.random; }
        }

        /// <summary>
        /// Gets the StrokeCollection.
        /// </summary>
        /// <value>The StrokeCollection.</value>
        public StrokeCollection Strokes
        {
            get { return this.strokes; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RecognitionTest"/> passed.
        /// </summary>
        /// <value><c>true</c> if test passed; otherwise, <c>false</c>.</value>
        public bool Passed
        {
            get
            {
                if (this.Text.Equals(this.RecognizedText))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the text for this test.
        /// </summary>
        /// <value>The text for this test.</value>
        public abstract string Text
        {
            get;
        }

        /// <summary>
        /// Gets the recognized text.
        /// </summary>
        /// <value>The recognized text.</value>
        public string RecognizedText
        {
            get
            {
                if (!this.analyisTestDidRun)
                {
                    if (this.strokes.Count > 0)
                    {
                        InkAnalyzer theInkAnalyzer = new InkAnalyzer();
                        theInkAnalyzer.AddStrokes(this.strokes);
                        AnalysisStatus status = theInkAnalyzer.Analyze();

                        if (status.Successful)
                        {
                            this.recognizedText = theInkAnalyzer.GetRecognizedString();
                        }
                        else
                        {
                            this.recognizedText = string.Empty;
                        }
                    }
                    else
                    {
                        this.recognizedText = string.Empty;
                    }

                    this.analyisTestDidRun = true;
                }

                return this.recognizedText;
            }
        }

        /// <summary>
        /// Strokes the collection changed event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Ink.StrokeCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void StrokeCollectionChanged(object sender, StrokeCollectionChangedEventArgs e)
        {
            this.analyisTestDidRun = false;
        }
    }
}
