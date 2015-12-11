using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Interfaces
{
    public interface IEncodable
    {
        /// <summary>
        /// Pattern of the byte lengths in the underlying data
        /// </summary>
        byte[] bytePattern { get; }

        /// <summary>
        /// Interface for getting the byte array for the data
        /// </summary>
        /// <returns></returns>
        byte[] ToByteArray();
    }
}
