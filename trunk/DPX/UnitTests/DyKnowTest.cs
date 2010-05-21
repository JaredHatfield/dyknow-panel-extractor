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
            // Read in the file as a string
            string original = this.GetFileContent("TestSimpleInking");
            DyKnow dyknow = this.DeserializeDyKnow(original);

            if (dyknow == null)
            {
                Assert.Fail("The XML could not be deserialized properly.");
                Debug.WriteLine("Simple Inking Test Failed to Serialize");
            }
            else
            {
                string repacked = this.SerializeDyKnow(dyknow);
                Assert.AreEqual(original, repacked, "The reserialized object did not match the original xml");
                Debug.WriteLine("Simple Inking Test Results:");
                Debug.WriteLine(original);
                Debug.WriteLine(repacked);
            }
        }

        /// <summary>
        /// Test the serilization and deserilization of a DyKnow file that contains some complicated ink content.
        /// </summary>
        [TestMethod()]
        public void SerilicationTestAdvancedInking()
        {
            // Read in the file as a string
            string original = this.GetFileContent("TestAdvancedInking");
            DyKnow dyknow = this.DeserializeDyKnow(original);

            if (dyknow == null)
            {
                Assert.Fail("The XML could not be deserialized properly.");
                Debug.WriteLine("Advanced Inking Test Failed to Serialize");
            }
            else
            {
                string repacked = this.SerializeDyKnow(dyknow);
                Assert.AreEqual(original, repacked, "The reserialized object did not match the original xml");
                Debug.WriteLine("Advanced Inking Test Results: ");
                Debug.WriteLine(original);
                Debug.WriteLine(repacked);
            }
        }

        /// <summary>
        /// Test the serilization and deserilization of a DyKnow file that contains deleted ink content.
        /// </summary>
        [TestMethod()]
        public void SerilicationTestDeleteInking()
        {
            // Read in the file as a string
            string original = this.GetFileContent("TestDeleteInking");
            DyKnow dyknow = this.DeserializeDyKnow(original);

            if (dyknow == null)
            {
                Assert.Fail("The XML could not be deserialized properly.");
                Debug.WriteLine("Delete Inking Test Failed to Serialize");
            }
            else
            {
                string repacked = this.SerializeDyKnow(dyknow);
                Assert.AreEqual(original, repacked, "The reserialized object did not match the original xml");
                Debug.WriteLine("Delete Inking Test Results: ");
                Debug.WriteLine(original);
                Debug.WriteLine(repacked);
            }
        }

        /// <summary>
        /// Test the serilization and deserilization of a DyKnow file that contains resized ink content.
        /// </summary>
        [TestMethod()]
        public void SerilicationTestScaleInking()
        {
            // Read in the file as a string
            string original = this.GetFileContent("TestScaleInking");
            DyKnow dyknow = this.DeserializeDyKnow(original);

            if (dyknow == null)
            {
                Assert.Fail("The XML could not be deserialized properly.");
                Debug.WriteLine("Scale Inking Test Failed to Serialize");
            }
            else
            {
                string repacked = this.SerializeDyKnow(dyknow);
                Assert.AreEqual(original, repacked, "The reserialized object did not match the original xml");
                Debug.WriteLine("Scale Inking Test Results: ");
                Debug.WriteLine(original);
                Debug.WriteLine(repacked);
            }
        }

        /// <summary>
        /// Test the serilization and deserilization of a DyKnow file that contains a simple image.
        /// </summary>
        [TestMethod()]
        public void SerilicationTestSimpleImage()
        {
            // Read in the file as a string
            string original = this.GetFileContent("TestSimpleImage");
            DyKnow dyknow = this.DeserializeDyKnow(original);

            if (dyknow == null)
            {
                Assert.Fail("The XML could not be deserialized properly.");
                Debug.WriteLine("Simple Image Test Failed to Serialize");
            }
            else
            {
                string repacked = this.SerializeDyKnow(dyknow);
                Assert.AreEqual(original, repacked, "The reserialized object did not match the original xml");
                Debug.WriteLine("Simple Image Test Results: ");
                Debug.WriteLine(original);
                Debug.WriteLine(repacked);
            }
        }

        /// <summary>
        /// Test the serilization and deserilization of a DyKnow file that contains moderator full page text.
        /// </summary>
        [TestMethod()]
        public void SerilicationTestFullPanelTextModerator()
        {
            // Read in the file as a string
            string original = this.GetFileContent("TestFullPanelTextModerator");
            DyKnow dyknow = this.DeserializeDyKnow(original);

            if (dyknow == null)
            {
                Assert.Fail("The XML could not be deserialized properly.");
                Debug.WriteLine("Full Panel Text Moderator Test Failed to Serialize");
            }
            else
            {
                string repacked = this.SerializeDyKnow(dyknow);
                Assert.AreEqual(original, repacked, "The reserialized object did not match the original xml");
                Debug.WriteLine("Full Panel Text Moderator Test Results: ");
                Debug.WriteLine(original);
                Debug.WriteLine(repacked);
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
