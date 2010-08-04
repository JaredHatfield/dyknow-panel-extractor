// <copyright file="AnswerProcessQueueItem.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The logic for processing the answer boxes.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Ink;
    using System.Windows.Threading;
    using DPXCommon;
    using DPXReader.DyKnow;

    /// <summary>
    /// The logic for processing the answer boxes.
    /// </summary>
    internal class AnswerProcessQueueItem : QueueItem
    {
        /// <summary>
        /// The DyKnow file to render.
        /// </summary>
        private DyKnow dyknow;

        /// <summary>
        /// The index of the panel to render
        /// </summary>
        private int index;

        /// <summary>
        /// The answer for the panel.
        /// </summary>
        private PanelAnswer answer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerProcessQueueItem"/> class.
        /// </summary>
        /// <param name="dyknow">The dyknow file.</param>
        /// <param name="index">The panel index.</param>
        /// <param name="answer">The answer.</param>
        internal AnswerProcessQueueItem(DyKnow dyknow, int index, PanelAnswer answer)
        {
            this.dyknow = dyknow;
            this.index = index;
            this.answer = answer;
        }

        /// <summary>
        /// Executes the action required by this item.
        /// </summary>
        public override void Run()
        {
            // Render the ink canvas
            InkCanvas ink = new InkCanvas();
            ink.Width = 1024;
            ink.Height = 768;
            DPXReader.DyKnow.Page page;
            int goal = 0;
            lock (this.dyknow)
            {
                page = this.dyknow.DATA[this.index] as DPXReader.DyKnow.Page;
                this.dyknow.Render(ink, this.index);
                goal = this.dyknow.DATA.Count;
            }

            // Identify all of the answer boxs
            Collection<Abox> aboxes = new Collection<Abox>();
            for (int i = 0; i < page.OLST.Count; i++)
            {
                if (page.OLST[i] is Abox)
                {
                    aboxes.Add(page.OLST[i] as Abox);
                }
            }

            // Perform the handwriting recognition
            for (int i = 0; i < aboxes.Count; i++)
            {
                // Clone and then clip the stroke collection
                StrokeCollection strokes = ink.Strokes.Clone();
                Rect bounds = aboxes[i].GetRect(ink.Height, ink.Width);
                strokes.Clip(bounds);

                if (strokes.Count > 0)
                {
                    InkAnalyzer theInkAnalyzer = new InkAnalyzer();
                    theInkAnalyzer.AddStrokes(strokes);
                    AnalysisStatus status = null;

                    try
                    {
                        // Attempt the handwriting analysis
                        status = theInkAnalyzer.Analyze();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("The analysis failed! " + e.Message);
                    }

                    if (status != null && status.Successful)
                    {
                        // Add the result to the answer
                        lock (this.answer)
                        {
                            this.answer.AddResult(
                                bounds, 
                                theInkAnalyzer.GetRecognizedString(), 
                                theInkAnalyzer.GetAlternates());
                        }
                    }
                }
            }
        }
    }
}
