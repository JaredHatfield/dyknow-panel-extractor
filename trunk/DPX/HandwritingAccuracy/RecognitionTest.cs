// <copyright file="RecognitionTest.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The base class for a recognition test.</summary>
namespace HandwritingAccuracy
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
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
        /// The alternative recognized text.
        /// </summary>
        private AnalysisAlternateCollection alternateText;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecognitionTest"/> class.
        /// </summary>
        public RecognitionTest()
        {
            this.strokes = new StrokeCollection();

            // This seems to be breaking the writing...
            // this.strokes.StrokesChanged += new StrokeCollectionChangedEventHandler(this.StrokeCollectionChanged);
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
        /// Gets the serialized ink.
        /// </summary>
        /// <value>The serialized ink.</value>
        public string SerializedInk
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                this.strokes.Clone().Save(ms);
                ms.Seek(0, SeekOrigin.Begin);
                string encoded = System.Convert.ToBase64String(ms.ToArray());
                return encoded;

                // And the reverse...
                /*
                byte[] decoded = System.Convert.FromBase64String(encoded);
                MemoryStream d = new MemoryStream(decoded);
                this.InkCanvasRight.Strokes = new System.Windows.Ink.StrokeCollection(d);
                 */
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
        /// Gets the name of the experiment.
        /// </summary>
        /// <value>The name of the experiment.</value>
        public abstract string ExperimentName
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
                this.RecognizeStuff();
                return this.recognizedText;
            }
        }

        /// <summary>
        /// Gets the alternative text.
        /// </summary>
        /// <value>The alternative text.</value>
        public AnalysisAlternateCollection AlternativeText
        {
            get
            {
                this.RecognizeStuff();
                return this.alternateText;
            }
        }

        /// <summary>
        /// Preforms the recognition on the stroke collection if necessary.
        /// </summary>
        private void RecognizeStuff()
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
                        this.alternateText = theInkAnalyzer.GetAlternates();
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
