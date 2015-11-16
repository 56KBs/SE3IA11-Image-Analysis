using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Helpers
{
    public class LimitedQueue<T> : Queue<T>
    {
        public int size { get; private set; }

        public LimitedQueue(int size)
        {
            this.size = size;
        }

        public new void Enqueue(T data)
        {
            if (base.Count == size)
            {
                base.Dequeue();
            }

            base.Enqueue(data);
        }
    }
}
