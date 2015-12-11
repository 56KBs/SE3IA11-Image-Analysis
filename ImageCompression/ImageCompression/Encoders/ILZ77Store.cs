using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Encoders
{
    /// <summary>
    /// Abstract class used to link long and short LZ77 stores to a contract
    /// </summary>
    public abstract class LZ77Store : Interfaces.IEncodable
    {
        /// <summary>
        /// The byte pattern the underlying data has
        /// </summary>
        public abstract byte[] bytePattern { get; }

        /// <summary>
        /// Whether this is a short form LZ77 or not
        /// </summary>
        public bool shortForm { get; protected set; }
        
        /// <summary>
        /// Abstract method to return a byte array of the underlying data
        /// </summary>
        /// <returns></returns>
        public abstract byte[] ToByteArray();
    }
}
