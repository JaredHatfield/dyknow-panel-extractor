﻿// <copyright file="AnswerProcessQueueItem.cs" company="University of Louisville Speed School of Engineering">
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
        /// The width of the canvas used for recognition.
        /// </summary>
        public const int DefaultWidth = 1024;

        /// <summary>
        /// The height of the canvas used for recognition.
        /// </summary>
        public const int DefaultHeight = 768;

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
            ink.Width = AnswerProcessQueueItem.DefaultWidth;
            ink.Height = AnswerProcessQueueItem.DefaultHeight;
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
                    try
                    {
                        InkAnalyzer theInkAnalyzer = InkAnalysisHelper.Analyze(strokes, 4);

                        // Add the result to the answer
                        lock (this.answer)
                        {
                            this.answer.AddResult(
                                bounds,
                                theInkAnalyzer.GetRecognizedString(),
                                theInkAnalyzer.GetAlternates());
                        }
                    }
                    catch
                    {
                    }
                }
            }
            
            // Mark the answer as complete
            lock (this.answer)
            {
                this.answer.FlagProcesed();
            }
        }
    }
}