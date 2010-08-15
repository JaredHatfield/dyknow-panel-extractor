// <copyright file="FileProcessRow.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The record for a file that needs to be processed.</summary>
namespace DPXPreview
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Threading;

    /// <summary>
    /// The record for a file that needs to be processed.
    /// </summary>
    internal class FileProcessRow
    {
        /// <summary>
        /// The dispatcher that is used to update the gui.
        /// </summary>
        private Dispatcher dispatcher;

        /// <summary>
        /// The filename that is being updated.
        /// </summary>
        private string filename;

        /// <summary>
        /// The label that will be updated with the progress.
        /// </summary>
        private Label progress;

        /// <summary>
        /// The result of the processing.
        /// </summary>
        private ParserTestResult result;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileProcessRow"/> class.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="progress">The progress.</param>
        public FileProcessRow(Dispatcher dispatcher, string filename, Label progress)
        {
            this.dispatcher = dispatcher;
            this.filename = filename;
            this.progress = progress;
            this.result = ParserTestResult.QUEUED;
        }

        /// <summary>
        /// The process update delegate
        /// </summary>
        /// <param name="result">The result that should be displayed.</param>
        private delegate void ProcessUpdateDelegate(ParserTestResult result);

        /// <summary>
        /// The enum that represents the test results.
        /// </summary>
        internal enum ParserTestResult
        {
            /// <summary>
            /// The test is currently sitting in the queue.
            /// </summary>
            QUEUED,

            /// <summary>
            /// The test is currently being processed.
            /// </summary>
            PROCESSING,

            /// <summary>
            /// The test failed with an unknown error.
            /// </summary>
            FAILED,

            /// <summary>
            /// The test included a known error.
            /// </summary>
            WARNING,

            /// <summary>
            /// The test passed with no errors.
            /// </summary>
            PASSED
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        internal string FileName
        {
            get { return this.filename; }
        }

        /// <summary>
        /// Sets the file processing.
        /// </summary>
        internal void SetFileProcessing()
        {
            if (this.result.Equals(ParserTestResult.QUEUED))
            {
                this.result = ParserTestResult.PROCESSING;
                this.dispatcher.Invoke(new ProcessUpdateDelegate(this.DispatchedProcessUpdate), DispatcherPriority.Input, this.result);
            }
        }

        /// <summary>
        /// Sets the file passed.
        /// </summary>
        internal void SetFilePassed()
        {
            if (this.result.Equals(ParserTestResult.PROCESSING))
            {
                this.result = ParserTestResult.PASSED;
                this.dispatcher.Invoke(new ProcessUpdateDelegate(this.DispatchedProcessUpdate), DispatcherPriority.Input, this.result);
            }
        }

        /// <summary>
        /// Sets the file warning.
        /// </summary>
        internal void SetFileWarning()
        {
            if (this.result.Equals(ParserTestResult.PROCESSING))
            {
                this.result = ParserTestResult.WARNING;
                this.dispatcher.Invoke(new ProcessUpdateDelegate(this.DispatchedProcessUpdate), DispatcherPriority.Input, this.result);
            }
        }

        /// <summary>
        /// Sets the file failed.
        /// </summary>
        internal void SetFileFailed()
        {
            if (this.result.Equals(ParserTestResult.PROCESSING) || this.result.Equals(ParserTestResult.WARNING))
            {
                this.result = ParserTestResult.FAILED;
                this.dispatcher.Invoke(new ProcessUpdateDelegate(this.DispatchedProcessUpdate), DispatcherPriority.Input, this.result);
            }
        }

        /// <summary>
        /// Dispatcheds the process update.
        /// </summary>
        /// <param name="result">The result.</param>
        private void DispatchedProcessUpdate(ParserTestResult result)
        {
            if (result.Equals(ParserTestResult.PASSED))
            {
                this.progress.Content = "Passed";
                this.progress.Background = Brushes.LightGreen;
            }
            else if (result.Equals(ParserTestResult.WARNING))
            {
                this.progress.Content = "Known Errors";
                this.progress.Background = Brushes.Yellow;
            }
            else if (result.Equals(ParserTestResult.FAILED))
            {
                this.progress.Content = "Failed";
                this.progress.Background = Brushes.Salmon;
            }
            else if (result.Equals(ParserTestResult.PROCESSING))
            {
                this.progress.Content = "Processing...";
                this.progress.Background = Brushes.LightBlue;
            }
        }
    }
}
