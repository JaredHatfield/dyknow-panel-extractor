using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickReader
{
    public class ImageData
    {
        Guid id;
        String img;
        
        
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }
        public String Img
        {
            get { return img; }
        }

        public ImageData(Guid id, String img)
        {
            this.id = id;
            this.img = img;
        }

        public ImageData(String id, String img)
        {
            this.id = new Guid(id);
            this.img = img;
        }

        public String getImageDataString64()
        {
            return img;
        }

        public override string ToString()
        {
            return id.ToString() + " - Length: " + img.Length;
        }

    }
}
