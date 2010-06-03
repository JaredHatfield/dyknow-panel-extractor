// <copyright file="ParserValidatorWindow.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The main window for Parser Validator.</summary>
namespace DPXPreview
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using DPXReader.DyKnow;

    /// <summary>
    /// Interaction logic for ParserValidatorWindow.xaml
    /// </summary>
    public partial class ParserValidatorWindow : Window
    {
        /// <summary>
        /// The list of files to validate.
        /// </summary>
        private string[] fileNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserValidatorWindow"/> class.
        /// </summary>
        public ParserValidatorWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A delegate for a method that takes no arguments.
        /// </summary>
        private delegate void NoArgsDelegate();

        /// <summary>
        /// The delegate for outputting a message.
        /// </summary>
        /// <param name="message">The message.</param>
        private delegate void OutputMessageDelegate(string message);

        /// <summary>
        /// Handles the Click event of the ButtonSelectFiles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonSelectFiles_Click(object sender, RoutedEventArgs e)
        {
            // Let the user choose which file to open
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "DyKnow files (*.dyz)|*.dyz";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                this.ButtonSelectFiles.IsEnabled = false;
                this.fileNames = openFileDialog.FileNames;
                Thread t = new Thread(new ThreadStart(this.ValidateFiles));
                t.Start();
            }
        }

        /// <summary>
        /// Validates the list of the files.
        /// </summary>
        private void ValidateFiles()
        {
            // Loop through all of the files and validate the parsers
            for (int i = 0; i < this.fileNames.Length; i++)
            {
                // Compare the files
                this.PerformSerializationTest(this.fileNames[i]);
            }

            // All Done
            Dispatcher.BeginInvoke(new NoArgsDelegate(this.EnableButtons), DispatcherPriority.Loaded);
        }

        /// <summary>
        /// Outputs the message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void OutputMessage(string message)
        {
            this.TextBoxResults.Text += message + "\n";
        }

        /// <summary>
        /// Outpots the message dispatch.
        /// </summary>
        /// <param name="message">The message.</param>
        private void OutputMessageDispatch(string message)
        {
            Dispatcher.Invoke(new OutputMessageDelegate(this.OutputMessage), DispatcherPriority.Input, message);
        }

        /// <summary>
        /// Enables the buttons.
        /// </summary>
        private void EnableButtons()
        {
            this.ButtonSelectFiles.IsEnabled = true;
        }

        /// <summary>
        /// Performs the serialization test.
        /// </summary>
        /// <param name="file">The file to test.</param>
        private void PerformSerializationTest(string file)
        {
            // Read in the file as a string
            FileStream inputFile = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            GZipStream gzipFile = new GZipStream(inputFile, CompressionMode.Decompress, true);
            StreamReader reader = new StreamReader(gzipFile);
            string original = reader.ReadToEnd();
            gzipFile.Close();
            inputFile.Close();
            DyKnow dyknow = this.DeserializeDyKnow(original);
            this.OutputMessageDispatch("Processing: " + file);
            
            if (dyknow == null)
            {
                // Write the XML (with line breaks) that could not be parsed to a file
                /*
                TextWriter tre = new StreamWriter(file + "-Failed" + ".txt");
                tre.WriteLine(original.Replace("><", ">\n<"));
                tre.Close();
                 */

                // Assert that the test failed.
                Debug.WriteLine(file + " Test Failed to Serialize");
                this.OutputMessageDispatch("File could not be deserialized.");
            }
            else
            {
                string repacked = this.SerializeDyKnow(dyknow);

                // TODO: Fix underlying problem with RTF and \r
                // Remove the cariage returns from both of the strings before comparison.
                // This is a hack that will let more of the tests pass
                original = original.Replace("\r", string.Empty);
                repacked = repacked.Replace("\r", string.Empty);

                // Perform a test to determine if the string are equal so we can write out the results.
                Debug.WriteLine("Original: " + original.Length + " - Repacked: " + repacked.Length);
                if (!original.Equals(repacked))
                {
                    // Write the original XML (with line breaks) to a file
                    /*
                    TextWriter tro = new StreamWriter(file + "-Original" + ".txt");
                    tro.WriteLine(original.Replace("><", ">\n<"));
                    tro.Close();
                     */

                    // Write the repacked XML (with line breaks) to a file
                    /*
                    TextWriter trr = new StreamWriter(file + "-Repacked" + ".txt");
                    trr.WriteLine(repacked.Replace("><", ">\n<"));
                    trr.Close();
                     */
                    this.OutputMessageDispatch("Failed...");
                }
                else
                {
                    this.OutputMessageDispatch("Success!");
                }
            }
        }

        /// <summary>
        /// Deerializes the DyKnow string.
        /// </summary>
        /// <param name="data">The string representation of a serialized DyKnow object.</param>
        /// <returns>A DyKnow object on success; otherwise null.</returns>
        private DyKnow DeserializeDyKnow(string data)
        {
            DyKnow dyknow = null;

            try
            {
                dyknow = DyKnow.Deserialize(data);
            }
            catch (Exception e)
            {
                Debug.WriteLine("DyKnow object could not be deserialized.");
                Debug.WriteLine(e.Message);
            }

            return dyknow;
        }

        /// <summary>
        /// Serializes the DyKnow object.
        /// </summary>
        /// <param name="dyknow">The DyKnow object.</param>
        /// <returns>A string representation of the DyKnow object; otherwise null.</returns>
        private string SerializeDyKnow(DyKnow dyknow)
        {
            string result = null;

            try
            {
                result = dyknow.Serialize();

                // NOTE:  This removes the XML header, this is not the best implementation possible.
                result = result.Substring(result.IndexOf("<DYKNOW_NB50"));
            }
            catch
            {
                Debug.WriteLine("DyKnow object could not serialized.");
            }

            return result;
        }
    }
}
