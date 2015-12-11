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
        /// <summary>
        /// Position of the data, relative to the cursor
        /// </summary>
        public int position { get; private set; }

        /// <summary>
        /// Length of the copy to complete
        /// </summary>
        public int length { get; private set; }

        /// <summary>
        /// The bytepattern for this data item
        /// </summary>
        public override byte[] bytePattern { get; }


        public LZ77StoreLong(int position, int length)
        {
            // Set up data stores
            this.shortForm = false;
            this.position = position;
            this.length = length;
            this.bytePattern = new byte[] { 1, 8, 8 };
        }

        /// <summary>
        /// Represent as a string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return Convert.ToByte(this.shortForm) + "," + this.position + "," + this.length;
        }

        public override bool Equals(object obj)
        {
            // Overridden equals to ensure equality checks function correctly
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
            // Overridden hash code to ensure the base hashcode is requred
            return base.GetHashCode();
        }

        /// <summary>
        /// Gets the item as a byte array
        /// </summary>
        /// <returns>byte array representing this data</returns>
        public override byte[] ToByteArray()
        {
            var returnByteArray = new byte[3];

            returnByteArray[0] = 0;
            returnByteArray[1] = (byte)this.position;
            returnByteArray[2] = (byte)this.length;

            return returnByteArray;
        }
    }
}
