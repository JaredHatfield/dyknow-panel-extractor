// <copyright file="DyKnowTest.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Performs the serialization / deserialization regression tests on a DyKnow file.</summary>
namespace UnitTests
{
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using DPXReader.DyKnow;
    using Microsoft.VisualStudio.TestTools.UnitTesting;   
    
    /// <summary>
    /// This is a test class for DyKnowTest and is intended
    /// to contain all DyKnowTest Unit Tests
    /// </summary>
    [TestClass()]
    public class DyKnowTest
    {
        /// <summary>
        /// THe test context instance.
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }

            set
            {
                this.testContextInstance = value;
            }
        }
        
        /// <summary>
        /// Test the serialization and deserialization of a DyKnow file that only contains only some simple ink content on a single panel.
        /// </summary>
        [TestMethod()]
        public void SerializationTestSimpleInking()
        {
            this.PerformSerializationTest("TestSimpleInking", "Simple Inking");
        }

        /// <summary>
        /// Test the serilization and deserilization of a DyKnow file that contains some complicated ink content.
        /// </summary>
        [TestMethod()]
        public void SerilicationTestAdvancedInking()
        {
            this.PerformSerializationTest("TestAdvancedInking", "Advanced Inking");
        }

        /// <summary>
        /// Test the serilization and deserilization of a DyKnow file that contains deleted ink content.
        /// </summary>
        [TestMethod()]
        public void SerilicationTestDeleteInking()
        {
            this.PerformSerializationTest("TestDeleteInking", "DeleteInking");
        }

        /// <summary>
        /// Test the serilization and deserilization of a DyKnow file that contains resized ink content.
        /// </summary>
        [TestMethod()]
        public void SerilicationTestScaleInking()
        {
            this.PerformSerializationTest("TestScaleInking", "Scale Inking");
        }

        /// <summary>
        /// Test the serilization and deserilization of a DyKnow file that contains a simple image.
        /// </summary>
        [TestMethod()]
        public void SerilicationTestSimpleImage()
        {
            this.PerformSerializationTest("TestSimpleImage", "Simple Image");
        }

        /// <summary>
        /// Test the serilization and deserilization of a DyKnow file that contains a multiple images.
        /// </summary>
        [TestMethod()]
        public void SerilicationTestMultipleImages()
        {
            this.PerformSerializationTest("TestMultipleImages", "Multiple Images");
        }

        /// <summary>
        /// Test the serilization and deserilization of a DyKnow file that contains moderator full page text.
        /// </summary>
        [TestMethod()]
        public void SerilicationTestFullPanelTextModerator()
        {
            this.PerformSerializationTest("TestFullPanelTextModerator", "Full Panel Text Moderator");
        }

        /// <summary>
        /// Test the serialization and deserialization of a DyKnow file that contains an animation.
        /// </summary>
        [TestMethod()]
        public void SerializationTestAnimation()
        {
            this.PerformSerializationTest("TestAnimation", "Animation");
        }

        /// <summary>
        /// Test the serialization and deserialization of a DyKnow file that contains a simple text box.
        /// </summary>
        [TestMethod()]
        public void SerializationTestSimpleTextBox()
        {
            this.PerformSerializationTest("TestSimpleTextBox", "Simple Text Box");
        }

        /// <summary>
        /// Test the serialization and deserialization of a DyKnow file that contains a hyperlink.
        /// </summary>
        [TestMethod()]
        public void SerializationTestHyperlink()
        {
            this.PerformSerializationTest("TestHyperlink", "Hyperlink");
        }

        /// <summary>
        /// Test the serialization and deserialization of a DyKnow file that contains a simple poll.
        /// </summary>
        [TestMethod()]
        public void SerializationTestSimplePoll()
        {
            this.PerformSerializationTest("TestSimplePoll", "Simple Poll");
        }

        /// <summary>
        /// Performs the serialization test.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="name">The name.</param>
        private void PerformSerializationTest(string file, string name)
        {
            // Read in the file as a string
            string original = this.GetFileContent(file);
            DyKnow dyknow = this.DeserializeDyKnow(original);

            if (dyknow == null)
            {
                Assert.Fail("The XML could not be deserialized properly.");
                Debug.WriteLine(name + " Test Failed to Serialize");
            }
            else
            {
                string repacked = this.SerializeDyKnow(dyknow);

                // Perform a test to determine if the string are equal so we can write out the results.
                if (!original.Equals(repacked))
                {
                    // Write the original XML (with line breaks) to a file
                    TextWriter tro = new StreamWriter(file + "-Original" + ".txt");
                    tro.WriteLine(original.Replace("><", ">\n<"));
                    tro.Close();

                    // Write the repacked XML (with line breaks) to a file
                    TextWriter trr = new StreamWriter(file + "-Repacked" + ".txt");
                    trr.WriteLine(repacked.Replace("><", ">\n<"));
                    trr.Close();
                }

                Assert.AreEqual(original, repacked, "The reserialized object did not match the original xml");
            }
        }

        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        /// <param name="name">The name of the file.</param>
        /// <returns>A string containg all of the contents of the file</returns>
        private string GetFileContent(string name)
        {
            string file = "..\\..\\..\\UnitTests\\DyKnowFiles\\" + name + ".dyz";
            FileStream inputFile = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            GZipStream gzipFile = new GZipStream(inputFile, CompressionMode.Decompress, true);
            StreamReader reader = new StreamReader(gzipFile);
            return reader.ReadToEnd();
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
            catch
            {
                Debug.WriteLine("DyKnow object could not be deserialized.");
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
