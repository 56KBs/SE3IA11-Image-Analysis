using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCompression.ExtensionMethods;

namespace ImageCompression.Encoders
{
    public class LZ77StoreShort<T> : LZ77Store
        where T : Interfaces.IEncodable
    {
        /// <summary>
        /// The data being stored
        /// </summary>
        public T data { get; private set; }

        public override byte[] bytePattern { get; }

        public LZ77StoreShort(T data)
        {
            // Set everything up
            this.data = data;
            this.shortForm = true;
            this.bytePattern = ((byte)1).Merge(this.data.bytePattern);
        }

        public override string ToString()
        {
            return Convert.ToByte(this.shortForm) + "," + data.ToString();
        }

        public override bool Equals(object obj)
        {
            // Override equals for equality checks
            if (obj == null)
            {
                return false;
            }

            LZ77StoreShort<T> storeShort = obj as LZ77StoreShort<T>;
            if ((System.Object)storeShort == null)
            {
                return false;
            }

            return this.ToString() == storeShort.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Return the object as a byte array
        /// </summary>
        /// <returns>byte array of object</returns>
        public override byte[] ToByteArray()
        {
            var dataBytes = data.ToByteArray();

            var returnByteArray = new byte[dataBytes.Length + 1];

            returnByteArray[0] = 0x01;
            dataBytes.CopyTo(returnByteArray, 1);

            return returnByteArray;
        }
    }
}
