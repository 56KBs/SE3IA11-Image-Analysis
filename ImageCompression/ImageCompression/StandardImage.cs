using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ImageCompression.ImageData;
using ImageCompression.ExtensionMethods;

namespace ImageCompression
{
    public class StandardImage
    {
        private Bitmap image { get; set; }

        private PixelMatrix data { get; set; }

        public StandardImage(Bitmap image)
        {
            this.image = image;
            this.data = new PixelMatrix(image.GetRGBPixelArray());
        }

        public StandardImage(ColorModel.RGB[,] data)
        {
            this.data = new PixelMatrix(data);
        }

        public Bitmap GetBitmap()
        {
            return this.image;
        }

        public ColorModel.RGB[,] GetPixelMatrix()
        {
            return this.data.data;
        }
    }
}
