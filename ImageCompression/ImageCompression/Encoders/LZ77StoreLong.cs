using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Encoders
{
    public class LZ77StoreLong<T> : LZ77Store
        where T : Interfaces.IEncodable
    {
        public int position { get; private set; }

        public int length { get; private set; }

        public LZ77StoreLong(int position, int length)
        {
            this.shortForm = false;
            this.position = position;
            this.length = length;
        }

        public override string ToString()
        {
            return Convert.ToByte(this.shortForm) + "," + this.position + "," + this.length;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            LZ77StoreLong<T> storeLong = obj as LZ77StoreLong<T>;
            if ((System.Object)storeLong == null)
            {
                return false;
            }

            return this.ToString() == storeLong.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override VariableByte[] ToByteArray()
        {
            var returnByteArray = new VariableByte[3];

            returnByteArray[0] = VariableByte.Zero;
            returnByteArray[1] = new VariableByte((byte)this.position, VariableByte.Bits.Eight);
            returnByteArray[2] = new VariableByte((byte)this.length, VariableByte.Bits.Eight);

            return returnByteArray;
        }

        public override byte[] ToFullByteArray()
        {
            var returnByteArray = new byte[3];

            returnByteArray[0] = 0x00;
            returnByteArray[1] = (byte)this.position;
            returnByteArray[2] = (byte)this.length;

            return returnByteArray;
        }
    }
}
