using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using ImageCompression.ExtensionMethods;

namespace ImageCompression.ImageData
{
    public class FourierMatrix<T> where T : ColorModel.RGB
    {
        private List<Channel<Complex[,]>> data { get; set; }

        public FourierMatrix(T[,] data)
        {
            this.data = new List<Channel<Complex[,]>>();

            foreach (ColorModel.RGB.Channels channel in Enum.GetValues(typeof(ColorModel.RGB.Channels)))
            {
                var complexComponentMatrix = data.ConvertAll2D<ColorModel.RGB, Complex>(new Converter<ColorModel.RGB, Complex>(value => (Complex)value.SelectChannel(channel)));
                this.data.Add(new Channel<Complex[,]>((int)channel, complexComponentMatrix));
            }
        }

        private Complex[,] getComplexChannel(ColorModel.RGB.Channels channel)
        {
            return data.Find(x => x.id == (int)channel).data;
        }
    }
}
