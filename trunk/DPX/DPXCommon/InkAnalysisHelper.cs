
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
    /// 
    /// </summary>
    public class InkAnalysisHelper
    {
        /// <summary>
        /// Analyzes the specified strokes.
        /// </summary>
        /// <param name="strokes">The strokes.</param>
        /// <param name="attemptCount">The attempt count.</param>
        /// <returns></returns>
        public static InkAnalyzer Analyze(StrokeCollection strokes, int attemptCount)
        {
            InkAnalyzer theInkAnalyzer = new InkAnalyzer();
            theInkAnalyzer.AddStrokes(strokes);
            AnalysisStatus status = null;

            // It is possible for this to fail. :(
            // We want to make multiple attempts to perform the analysis if necessary.
            while (attemptCount > 0)
            {
                try
                {
                    // Attempt the handwriting analysis
                    status = theInkAnalyzer.Analyze();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("The analysis failed! " + e.Message);

                    // If we failed for some reason, lets take a short break
                    Thread.Sleep(100);
                }

                // It worked so we do not need to make any more attempts
                if (status != null && status.Successful)
                {
                    return theInkAnalyzer;
                }

                attemptCount--;
            }

            throw new Exception("The analysis failed!  Giving up.");
        }
    }
}
