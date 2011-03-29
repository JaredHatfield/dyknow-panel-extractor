// <copyright file="AnalyzerQueueItem.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>A queue item that needs to be processed.</summary>
namespace DPXGrader
{
    using System;
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
    /// A queue item that needs to be processed.
    /// </summary>
    internal class AnalyzerQueueItem : QueueItem
    {
        /// <summary>
        /// The window element that will be manipulated by this item.
        /// </summary>
        private PanelProcessorWindow window;

        /// <summary>
        /// The number of the panel to process.
        /// </summary>
        private int num;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyzerQueueItem"/> class.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="num">The number.</param>
        public AnalyzerQueueItem(PanelProcessorWindow window, int num)
        {
            this.window = window;
            this.num = num;
        }

        /// <summary>
        /// Executes the action required by this item.
        /// </summary>
        public override void Run()
        {
            InkCanvas ink = new InkCanvas();
            ink.Width = 400;
            ink.Height = 300;
            DPXReader.DyKnow.Page page;
            int goal = 0;

            lock (this.window.DyKnow)
            {
                page = this.window.DyKnow.DATA[this.num] as DPXReader.DyKnow.Page;
                this.window.DyKnow.Render(ink, this.num);
                goal = this.window.DyKnow.DATA.Count;
            }

            ink.Strokes.Clip(this.window.GetRectangleArea());
            string val = string.Empty;
            double valDigit = 0;
            if (ink.Strokes.Count > 0)
            {
                // Perform the handwriting recognition
                try
                {
                    InkAnalyzer theInkAnalyzer = InkAnalysisHelper.Analyze(ink.Strokes, 4);
                    val = theInkAnalyzer.GetRecognizedString();
                    theInkAnalyzer.Dispose();
                    try
                    {
                        valDigit = double.Parse(val);
                    }
                    catch
                    {
                    }
                }
                catch
                {
                }
            }

            // Add the new record to the results table
            this.window.Dispatcher.Invoke(new PanelProcessorWindow.AddRowToResultsDelegate(this.window.AddRowToResults), DispatcherPriority.Input, this.num, page.ONERN, page.ONER, val, valDigit);

            // Add the values to the results collection
            string[] record = new string[5];
            record[0] = (this.num + 1).ToString();
            record[1] = page.ONERN;
            record[2] = page.ONER;
            record[3] = val;
            record[4] = string.Empty + valDigit;
            int progressNumber = 0;
            lock (this.window.Results)
            {
                this.window.Results.Add(record);
                progressNumber = this.window.Results.Count;

                // Notify the parent thread that something was added to the results
                Monitor.Pulse(this.window.Results);
            }

            // Update the progress bar
            this.window.Dispatcher.Invoke(new PanelProcessorWindow.UpdateProgressBarDelegate(this.window.UpdateProcessProgressBar), DispatcherPriority.Input, progressNumber, goal);
        }
    }
}
