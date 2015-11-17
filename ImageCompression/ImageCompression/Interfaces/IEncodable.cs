using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Interfaces
{
    public interface IEncodable
    {
        byte[] ToByteArray();
    }
}
