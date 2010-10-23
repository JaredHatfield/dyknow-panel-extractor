// <copyright file="InkAnalysisHelper.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The InkAnalysisHelper for performing handwriting recognition.</summary>
namespace DPXCommon
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows.Ink;

    /// <summary>
    /// The InkAnalysisHelper for performing handwriting recognition.
    /// </summary>
    public class InkAnalysisHelper
    {
        /// <summary>
        /// Analyzes the specified strokes.
        /// </summary>
        /// <param name="strokes">The strokes.</param>
        /// <param name="attemptCount">The attempt count.</param>
        /// <returns>The InkAnalyzer that has already run.</returns>
        public static InkAnalyzer Analyze(StrokeCollection strokes, int attemptCount)
        {
            // It is possible for this to fail. :(
            // We want to make multiple attempts to perform the analysis if necessary.
            int counter = 0;
            while (counter < attemptCount)
            {
                Debug.WriteLine("InkAnalysisHelper->Analyze Attempt " + (counter + 1));
                InkAnalyzer theInkAnalyzer = new InkAnalyzer();
                theInkAnalyzer.AddStrokes(strokes);
                AnalysisStatus status = null;
                bool success = true;

                try
                {
                    // Attempt the handwriting analysis
                    status = theInkAnalyzer.Analyze();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("The analysis failed! " + e.Message);
                    success = false;

                    // If we failed for some reason, lets take a short break
                    Thread.Sleep(100);
                }

                // It worked so we do not need to make any more attempts
                if (success && status != null && status.Successful)
                {
                    return theInkAnalyzer;
                }

                counter++;
            }

            throw new Exception("The analysis failed!  Giving up.  All hope is lost!");
        }
    }
}
