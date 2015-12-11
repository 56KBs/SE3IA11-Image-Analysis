using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Helpers
{
    public class LimitedQueue<T> : Queue<T>
    {
        /// <summary>
        /// Size of the queue
        /// </summary>
        public int size { get; private set; }

        public LimitedQueue(int size)
        {
            this.size = size;
        }

        /// <summary>
        /// Enqueue the data
        /// </summary>
        /// <param name="data">Data item to enqueue</param>
        public new void Enqueue(T data)
        {
            // If the queue is full, dequeue then enqueue
            if (base.Count == size)
            {
                base.Dequeue();
            }

            base.Enqueue(data);
        }
    }
}
