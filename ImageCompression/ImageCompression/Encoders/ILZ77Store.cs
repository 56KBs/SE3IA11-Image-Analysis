using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Encoders
{
    public interface ILZ77Store<T> where T : ColorModel.RGB
    {
        bool shortForm { get; }
    }
}
