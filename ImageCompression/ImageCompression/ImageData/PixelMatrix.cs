using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageCompression.ImageData
{
    class PixelMatrix
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
    }
}
