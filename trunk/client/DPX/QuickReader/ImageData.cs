// <copyright file="ImageData.cs" company="DPX">
// GNU General Public License v3
// </copyright>
// <summary>Image Data object.</summary>
namespace QuickReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class ImageData
    {
        /// <summary>
        /// 
        /// </summary>
        private Guid id;

        /// <summary>
        /// 
        /// </summary>
        private string img;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="img"></param>
        public ImageData(Guid id, string img)
        {
            this.id = id;
            this.img = img;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="img"></param>
        public ImageData(string id, string img)
        {
            this.id = new Guid(id);
            this.img = img;
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Img
        {
            get { return this.img; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetImageDataString64()
        {
            return this.img;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.id.ToString() + " - Length: " + this.img.Length;
        }
    }
}
