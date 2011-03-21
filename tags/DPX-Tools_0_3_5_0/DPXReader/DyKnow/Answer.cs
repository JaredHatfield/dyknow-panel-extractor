// <copyright file="Answer.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The answer object.</summary>
namespace DPXReader.DyKnow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The answer object.
    /// </summary>
    [XmlRoot("A")]
    public class Answer
    {
        /// <summary>
        /// The shrt value.
        /// </summary>
        private string shrt;

        /// <summary>
        /// The lng value.
        /// </summary>
        private string lng;

        /// <summary>
        /// Initializes a new instance of the <see cref="Answer"/> class.
        /// </summary>
        public Answer()
        {
        }

        /// <summary>
        /// Gets or sets the SHRT.
        /// </summary>
        /// <value>The SHRT value.</value>
        [XmlAttribute("SHRT")]
        public string SHRT
        {
            get { return this.shrt; }
            set { this.shrt = value; }
        }

        /// <summary>
        /// Gets or sets the LNG.
        /// </summary>
        /// <value>The LNG value.</value>
        [XmlAttribute("LNG")]
        public string LNG
        {
            get { return this.lng; }
            set { this.lng = value; }
        }
    }
}
