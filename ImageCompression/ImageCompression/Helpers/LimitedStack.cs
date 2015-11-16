using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Helpers
{
    public class LimitedStack<T> : Stack<T>
    {
        public int size { get; private set; }

        public LimitedStack(int size)
        {
            this.size = size;
        }

        public new void Push(T data)
        {
            if (base.Count == size)
            {
                base.Pop();
            }

            base.Push(data);
        }
    }
}
