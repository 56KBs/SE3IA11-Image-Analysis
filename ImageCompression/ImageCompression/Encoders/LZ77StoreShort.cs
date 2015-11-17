﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Encoders
{
    public class LZ77StoreShort<T> : ILZ77Store<T>
        where T : ColorModel.RGB
    {
        public T data { get; private set; }

        public bool shortForm { get; private set; }

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
    }
}
