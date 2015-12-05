using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.ExtensionMethods
{
    public static class LinqExtensions
    {
        public static IEnumerable<IGrouping<TKey, TElement>> GroupRuns<TKey, TElement>(
            this IEnumerable<TElement> source,
            Func<TElement, TKey> keySelector)
        {
            var dataList = new List<TElement>();
            var listEmpty = true;
            var previousKey = default(TKey);

            foreach (TElement item in source)
            {
                TKey key = keySelector(item);

                // If the list isn't empty, see if we're at the end of a run
                if (!listEmpty)
                {
                    // If the keys are equal, add to the list
                    if (key.Equals(previousKey))
                    {
                        dataList.Add(item);
                        previousKey = key;
                    }
                    // We've hit the end of a run of items
                    else
                    {
                        // Yield return so we continue running if we're called again
                        yield return new GroupOfData<TElement, TKey>(dataList, previousKey);

                        // Create a new list for next time
                        dataList = new List<TElement>();
                        dataList.Add(item);
                        previousKey = key;
                    }
                }
                else
                {
                    dataList.Add(item);
                    previousKey = key;
                    listEmpty = false;
                }
            }

            // Once we've hit the end of the loop, return what we've got
            if (!listEmpty)
            {
                yield return new GroupOfData<TElement, TKey>(dataList, previousKey);
            }
        }
    }

    public class GroupOfData<TElement, TKey> : IGrouping<TKey, TElement>
    {
        public TKey key { get; private set; }

        public List<TElement> dataList { get; set; }

        public TKey Key
        {
            get
            {
                return key;
            }
        }

        public GroupOfData(List<TElement> dataList, TKey key)
        {
            this.dataList = dataList;
            this.key = key;
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            return ((IEnumerable<TElement>)dataList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<TElement>)dataList).GetEnumerator();
        }
    }
}
