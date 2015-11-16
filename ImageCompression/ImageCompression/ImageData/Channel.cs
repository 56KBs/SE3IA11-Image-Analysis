using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.ImageData
{
    class Channel<T>
    {
        public int id { get; private set; }
        public T data { get; set; }

        public Channel(int id, T data)
        {
            this.data = data;
        }
    }
}
