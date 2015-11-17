using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Encoders
{
    public class LZ77
    {
        public static List<LZ77Store> Encode<T>(List<T> data)
            where T : Interfaces.IEncodable
        {
            return LZ77.Encode(data.ToArray()).ToList();
        }

        public static LZ77Store[] Encode<T>(T[] data)
            where T : Interfaces.IEncodable
        {
            var outputList = new List<LZ77Store>();

            var window = new Helpers.LimitedQueue<T>(20);
            var lookahead = new Helpers.LimitedQueue<T>(4);

            var position = 0;
            var lookaheadEnd = 0;

            while (position < data.Length)
            {
                if (position == 0)
                {
                    // Nothing in the window, so just output the first item as-is
                    outputList.Add(new LZ77StoreShort<T>(data[position]));

                    position++;
                    lookaheadEnd++;

                    while (lookaheadEnd++ <= lookahead.size)
                    {
                        lookahead.Enqueue(data[lookaheadEnd - 1]);
                    }

                    window.Enqueue(data[position - 1]);
                }
                else
                {
                    // Find the longest match we van
                    var matchPosition = 0;

                    var matchLength = LZ77.LongestMatch(window.ToArray(), lookahead.ToArray(), out matchPosition);

                    if (matchLength > 0)
                    {
                        outputList.Add(new LZ77StoreLong<T>((position - matchPosition), matchLength));

                        // Last item in the list
                        if (lookaheadEnd == data.Length - 1)
                        {
                            // Do nothing :)
                        }
                        else if (lookaheadEnd + matchLength > data.Length)
                        {
                            // Will overflow to past the end
                            lookahead = new Helpers.LimitedQueue<T>(data.Length - position - matchLength);
                            lookaheadEnd = position + matchLength;
                            var lookaheadNewEnd = lookaheadEnd + lookahead.size;

                            while (lookaheadEnd++ < lookaheadNewEnd)
                            {
                                lookahead.Enqueue(data[lookaheadEnd - 1]);
                            }
                        }
                        else
                        {
                            // Not going to overflow ourselves
                            var lookaheadNewEnd = lookaheadEnd + matchLength;

                            while (lookaheadEnd++ <= lookaheadNewEnd)
                            {
                                lookahead.Enqueue(data[lookaheadEnd - 1]);
                            }
                        }

                        // Add the items we've skipped to the window
                        for (var i = 0; i < matchLength; i++)
                        {
                            window.Enqueue(data[position - i]);
                        }

                        position += matchLength;
                    }
                    else
                    {
                        outputList.Add(new LZ77StoreShort<T>(data[position]));

                        // Only add to the queue's if the loop is going to continue
                        if (++position < data.Length - 1 && position + 2 < data.Length)
                        {
                            lookahead.Enqueue(data[position + 2]);
                            window.Enqueue(data[position - 1]);
                        }
                    }
                }
            }

            return outputList.ToArray();
        }

        public static int LongestMatch<T>(T[] window, T[] buffer, out int longestMatchStart)
        {
            var windowSize = window.Length;
            var bufferSize = buffer.Length;

            longestMatchStart = 0;
            var longestMatch = 0;

            var currentMatchStart = 0;
            var currentMatch = 0;

            // Can the last item in the window match the first buffer item?
            if (window[windowSize - 1].Equals(buffer[0]))
            {
                // If this is true then the last item may repeatedly match along the buffer
                currentMatchStart = windowSize - 1;
                currentMatch++;

                var i = 0;

                while (++i < bufferSize && window[currentMatchStart].Equals(buffer[i]))
                {
                    currentMatch++;
                }

                // Matching run is over, update the latest if possible
                if (currentMatch > longestMatch)
                {
                    longestMatchStart = currentMatchStart;
                    longestMatch = currentMatch;
                }

                // Clear up the current match
                currentMatchStart = 0;
                currentMatch = 0;
            }

            // Do a normal match if possible

            // Loop whilst we can still make a longer match
            for (var i = 0; (windowSize - i) > longestMatch; i++)
            {
                // If we have a starting match
                if (window[i].Equals(buffer[0]))
                {
                    currentMatchStart = i;
                    currentMatch++;

                    // Calculate the length of the match
                    var j = i;

                    // Ensure we don't overflow and the values are equal
                    while (++j < windowSize && (j - i) < bufferSize && window[j].Equals(buffer[j - i]))
                    {
                        currentMatch++;
                    }

                    // Matching run is over, update the latest if possible
                    if (currentMatch > longestMatch)
                    {
                        longestMatchStart = currentMatchStart;
                        longestMatch = currentMatch;
                    }

                    // Clear up the current match
                    currentMatchStart = 0;
                    currentMatch = 0;
                }
            }

            return longestMatch;
        }


        public static T[] Decode<T>(LZ77Store[] encodedData)
            where T : Interfaces.IEncodable
        {
            var decodedData = new List<T>();

            for (var i = 0; i < encodedData.Length; i++)
            {
                if (encodedData[i].shortForm)
                {
                    var shortData = (LZ77StoreShort<T>)encodedData[i];

                    decodedData.Add(shortData.data);
                }
                else
                {
                    var longData = (LZ77StoreLong<T>)encodedData[i];

                    var currentPosition = i - longData.position;
                    var copyLength = longData.length;

                    while (copyLength > 0)
                    {
                        // We have to copy this multiple times
                        if (currentPosition == i - 1 && copyLength > 1)
                        {
                            while (copyLength > 0)
                            {
                                decodedData.Add(decodedData[currentPosition]);
                                currentPosition++;
                                copyLength--;
                            }
                        }
                        else
                        {
                            decodedData.Add(decodedData[currentPosition]);
                            currentPosition++;
                            copyLength--;
                        }
                    }
                }
            }

            return decodedData.ToArray();
        }
    }
}
