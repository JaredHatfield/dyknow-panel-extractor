// <copyright file="ImageData.cs" company="DPX">
// GNU General Public License v3
// </copyright>
namespace QuickReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ImageData
    {
        private Guid id;

        private string img;

        public ImageData(Guid id, string img)
        {
            this.id = id;
            this.img = img;
        }

        public ImageData(string id, string img)
        {
            this.id = new Guid(id);
            this.img = img;
        }

        public Guid Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Img
        {
            get { return this.img; }
        }

        public string GetImageDataString64()
        {
            return this.img;
        }

        public override string ToString()
        {
            return this.id.ToString() + " - Length: " + this.img.Length;
        }
    }
}
