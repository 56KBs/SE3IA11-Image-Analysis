using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Encoders
{
    public abstract class LZ77Store : Interfaces.IEncodable
    {
        public bool shortForm { get; protected set; }

        public abstract byte[] ToByteArray();
    }
}
