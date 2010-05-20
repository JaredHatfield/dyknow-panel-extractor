// <copyright file="SerializerHelper.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Provides methods for serialization and deserialization.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Provides methods for serialization and deserialization.
    /// </summary>
    public class SerializerHelper
    {
        /// <summary>
        /// Method to convert a custom Object to XML string
        /// </summary>
        /// <param name="obj">Object that is to be serialized to XML</param>
        /// <param name="type">The object type.</param>
        /// <returns>XML string</returns>
        public string SerializeObject(object obj, Type type)
        {
            try
            {
                string result = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(type);
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                
                // xmlTextWriter.Formatting = Formatting.Indented;
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);
                
                xs.Serialize(xmlTextWriter, obj, ns);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                result = this.UTF8ByteArrayToString(memoryStream.ToArray());
                return result;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Method to reconstruct an Object from XML string
        /// </summary>
        /// <param name="str">The p xmlizedstring.</param>
        /// <param name="type">The object type.</param>
        /// <returns>The object that was created from the XML.</returns>
        public object DeserializeObject(string str, Type type)
        {
            XmlSerializer xs = new XmlSerializer(type);
            MemoryStream memoryStream = new MemoryStream(this.StringToUTF8ByteArray(str));
            return xs.Deserialize(memoryStream);
        }

        /// <summary>
        /// Deserializes the object.
        /// </summary>
        /// <param name="s">The stream to deserialize.</param>
        /// <param name="type">The object type.</param>
        /// <returns>The object that was created from the XML.</returns>
        public object DeserializeObject(Stream s, Type type)
        {
            XmlSerializer xs = new XmlSerializer(type);
            return xs.Deserialize(s);
        }

        /// <summary>
        /// To convert a byte Array of Unicode values (UTF-8 encoded) to a complete string.
        /// </summary>
        /// <param name="characters">Unicode byte Array to be converted to string</param>
        /// <returns>string converted from Unicode byte Array</returns>
        private string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedstring = encoding.GetString(characters);
            return constructedstring;
        }

        /// <summary>
        /// Converts the string to UTF8 byte array and is used in De serialization
        /// </summary>
        /// <param name="str">The p xmlstring.</param>
        /// <returns>A byte array of the characters.</returns>
        private byte[] StringToUTF8ByteArray(string str)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(str);
            return byteArray;
        }
    }
}
