using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ImageCompression.ExtensionMethods;

namespace ImageCompression.ImageData
{
    public class PixelMatrix
    {
        public ColorModel.RGB[,] data { get; set; }

        public FourierMatrix<ColorModel.RGB> fourierData { get; set; }

        public int width
        {
            get { return this.data.GetLength(0); }
        }

        public int height
        {
            get { return this.data.GetLength(1); }
        }

        public PixelMatrix(ColorModel.RGB[,] data)
        {
            this.data = data.Clone() as ColorModel.RGB[,];

            //this.fourierData = new FourierMatrix<ColorModel.RGB>(data);
        }

        public PixelMatrix(int width, int height)
        {
            this.data = new ColorModel.RGB[width, height];
        }

        public Bitmap GetBitmap()
        {
            var converted = this.data.ConvertAll2D(new Converter<ColorModel.RGB, ColorModel.RGB>(x => x.ToDepth(ColorModel.RGB.ColorDepth.TwentyFour)));

            var image = new Bitmap(this.data.GetLength(0), this.data.GetLength(1));

            for (var i = 0; i < image.Width; i++)
            {
                for (var j = 0; j < image.Height; j++)
                {
                    image.SetPixel(i, j, converted[i, j].ToColor());
                }
            }

            return image;
        }
    }
}
