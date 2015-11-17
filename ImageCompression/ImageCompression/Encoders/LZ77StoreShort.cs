using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Encoders
{
    public class LZ77StoreShort<T> : LZ77Store
        where T : Interfaces.IEncodable
    {
        public T data { get; private set; }

        public LZ77StoreShort(T data)
        {
            this.data = data;
            this.shortForm = true;
        }

        public override string ToString()
        {
            return Convert.ToByte(this.shortForm) + "," + data.ToString();
        }

        public override bool Equals(object obj)
        {
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

        public override byte[] ToByteArray()
        {
            var dataBytes = data.ToByteArray();

            var returnByteArray = new byte[dataBytes.Length + 1];

            returnByteArray[0] = 0x0001;
            dataBytes.CopyTo(returnByteArray, 1);

            return returnByteArray;
        }
    }
}
