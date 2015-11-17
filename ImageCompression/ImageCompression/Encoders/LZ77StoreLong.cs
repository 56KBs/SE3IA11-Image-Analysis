using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Encoders
{
    public class LZ77StoreLong<T> : ILZ77Store<T>
        where T : ColorModel.RGB
    {
        public bool shortForm { get; private set; }

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
    }
}
