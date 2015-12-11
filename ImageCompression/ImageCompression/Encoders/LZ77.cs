using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCompression.Helpers;
using ImageCompression.ExtensionMethods;
using System.IO;

namespace ImageCompression.Encoders
{
    public class LZ77
    {
        /// <summary>
        /// Add an item(s) to the LZ77 queue
        /// </summary>
        /// <typeparam name="T">Type of the data being addeded</typeparam>
        /// <param name="data">Data to take the data from</param>
        /// <param name="queue">Queue of data to add to</param>
        /// <param name="startPosition">Position to start adding from</param>
        /// <param name="count">Count of items to copy</param>
        /// <returns>Ending position</returns>
        private static int AddQueueItems<T>(ref T[] data, ref LimitedQueue<T> queue, int startPosition, int count)
        {
            // Create a copy of the start position
            var endPosition = startPosition;

            // Iterate over it, adding the data items required
            for (var i = 0; i < count; i++, endPosition++)
            {
                queue.Enqueue(data[startPosition + i]);
            }

            // Return the end position
            return endPosition;
        }

        /// <summary>
        /// Encoda via LZ77
        /// </summary>
        /// <typeparam name="T">Data type, interfaces IEncodable</typeparam>
        /// <param name="data">Data to encode</param>
        /// <param name="windowSize">Size of the past data window</param>
        /// <param name="lookaheadSize">Size of the lookahead window</param>
        /// <returns>LZ77 store array</returns>
        public static LZ77Store[] Encode<T>(T[] data, int windowSize, int lookaheadSize)
            where T : Interfaces.IEncodable
        {
            // Create the output list
            var outputList = new List<LZ77Store>();

            // Set up the lookahead and past windows
            var window = new Helpers.LimitedQueue<T>(windowSize);
            var lookahead = new Helpers.LimitedQueue<T>(lookaheadSize);

            var position = 0;
            var lookaheadEnd = 0;
            var windowEnd = 0;

            // Add first item
            // Nothing in the window, so just output the first item as-is
            outputList.Add(new LZ77StoreShort<T>(data[position]));

            // Advance the positions
            position++;
            lookaheadEnd++;

            // Add the first data item
            windowEnd = LZ77.AddQueueItems(ref data, ref window, windowEnd, 1);

            // If the lookahead size is larger than our data, create a lookahead queue the size of our data
            if (lookahead.size > data.Length)
            {
                lookahead = new Helpers.LimitedQueue<T>(data.Length - 1);
            }

            // Fill the lookahead queue
            lookaheadEnd = LZ77.AddQueueItems(ref data, ref lookahead, lookaheadEnd, lookahead.size);

            // Loop over the data
            while (position < data.Length)
            {
                var relativeMatchPosition = 0;
                // Calculate a match
                var matchLength = LZ77.LongestMatch(window.ToArray(), lookahead.ToArray(), out relativeMatchPosition);
                // Calculate the position of the match relative to the window
                var matchPosition = window.Count - relativeMatchPosition;

                var incrementBy = 1;

                // Long store needed
                if (matchLength != 0)
                {
                    // Add a long store
                    outputList.Add(new LZ77StoreLong<T>(matchPosition, matchLength));
                    incrementBy = matchLength;
                }
                else
                {
                    // Add a small store
                    outputList.Add(new LZ77StoreShort<T>(data[position]));
                }


                // Need to resize the queue as our full size is too big
                if (lookaheadEnd + incrementBy > data.Length)
                {
                    // Resize the queue
                    lookahead = new Helpers.LimitedQueue<T>(data.Length - position - incrementBy);
                    var lookaheadStart = position + incrementBy;

                    lookaheadEnd = LZ77.AddQueueItems(ref data, ref lookahead, lookaheadStart, lookahead.size);
                }
                // There is room in the lookahead queue
                else if (lookaheadEnd + incrementBy < data.Length)
                {
                    lookaheadEnd = LZ77.AddQueueItems(ref data, ref lookahead, lookaheadEnd, incrementBy);
                }

                // Add the items to the window that are skipped over
                windowEnd = LZ77.AddQueueItems(ref data, ref window, windowEnd, incrementBy);

                // Increment position counter
                position += incrementBy;
            }


            return outputList.ToArray();
        }

        /// <summary>
        /// Calculate the longest match in the data
        /// </summary>
        /// <typeparam name="T">Type of data, must be IEncodable</typeparam>
        /// <param name="window">Past data window to look over</param>
        /// <param name="buffer">The buffer of the data set</param>
        /// <param name="longestMatchStart">Output the containst the position of the longest match</param>
        /// <returns>The length of the match</returns>
        public static int LongestMatch<T>(T[] window, T[] buffer, out int longestMatchStart)
            where T : Interfaces.IEncodable
        {
            // Get buffer sizes
            var windowSize = window.Length;
            var bufferSize = buffer.Length;

            longestMatchStart = 0;
            var longestMatch = 0;

            var currentMatchStart = 0;
            var currentMatch = 0;
            
            // If the last item matches the buffer, calculate the length of a repeated match, if possible
            if (window[windowSize - 1].Equals(buffer[0]))
            {
                // If this is true then the last item may repeatedly match along the buffer
                currentMatchStart = windowSize - 1;
                currentMatch++;

                var i = 0;

                // Increment whilst there are matches
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

            // Return the match
            return longestMatch;
        }

        /// <summary>
        /// Decode an array of LZ77Store
        /// </summary>
        /// <typeparam name="T">Type of the return data</typeparam>
        /// <param name="encodedData">The encoded data</param>
        /// <returns>An array of type T, containing the decoded data</returns>
        public static T[] Decode<T>(LZ77Store[] encodedData)
            where T : Interfaces.IEncodable
        {
            // Set up the return list
            var decodedData = new List<T>();

            // Loop over all the data
            for (var i = 0; i < encodedData.Length; i++)
            {
                // If short form just copy the data from it as is
                if (encodedData[i].shortForm)
                {
                    var shortData = (LZ77StoreShort<T>)encodedData[i];

                    decodedData.Add(shortData.data);
                }
                else
                {
                    // If long form

                    // Create a long data item from the current item
                    var longData = (LZ77StoreLong<T>)encodedData[i];

                    // Calculate the current position
                    var currentPosition = decodedData.Count - longData.position;

                    var copyLength = longData.length;
                    
                    // Whilst we've got data to copy
                    while (copyLength > 0)
                    {
                        // We have to copy this multiple times
                        if (currentPosition == i - 1 && copyLength > 1)
                        {
                            // Copy the data repeatedly until we have no more to copy
                            while (copyLength > 0)
                            {
                                decodedData.Add(decodedData[currentPosition]);
                                currentPosition++;
                                copyLength--;
                            }
                        }
                        else
                        {
                            // Copy the data item and increment counters
                            decodedData.Add(decodedData[currentPosition]);
                            currentPosition++;
                            copyLength--;
                        }
                    }
                }
            }

            return decodedData.ToArray();
        }

        /// <summary>
        /// Calculates if the next data item is a long form
        /// </summary>
        /// <param name="bitReader">Reader of data</param>
        /// <returns>Boolean result</returns>
        private static bool IsLongForm(ref BitReader bitReader)
        {
            return !bitReader.ReadBoolean();
        }

        /// <summary>
        /// Reads a binary stream into a LZ77 store
        /// </summary>
        /// <param name="binaryReader">Reference to the binary reader</param>
        /// <param name="colorDepth">Colour depth the image is stored in</param>
        /// <returns>LZ77Store array</returns>
        public static LZ77Store[] DecodeBinaryStream(ref BinaryReader binaryReader, ColorModel.RGB.ColorDepth colorDepth)
        {
            var dataList = new List<LZ77Store>();

            // Create a bit reader from the base stream of the binaryreader
            var bitReader = new BitReader(binaryReader.BaseStream);

            // No packed data is the size of a byte, the remaining data will be picked up anyway
            while (bitReader.BaseStream.Position < bitReader.BaseStream.Length - 1)
            {
                // If is long form
                if (IsLongForm(ref bitReader))
                {
                    dataList.Add(new LZ77StoreLong<ColorModel.RGB>(
                        bitReader.ReadByte(),
                        bitReader.ReadByte()
                    ));
                }
                // If is short form
                else
                {
                    // Calculate length of bits
                    var colorBitLength = (int)colorDepth / 3;

                    if (colorDepth == ColorModel.RGB.ColorDepth.Eight)
                    {
                        // Add the data item
                        dataList.Add(new LZ77StoreShort<ColorModel.RGB>(
                            new ColorModel.RGB(
                                bitReader.ReadSmallBits(3), 3,
                                bitReader.ReadSmallBits(3), 3,
                                bitReader.ReadSmallBits(2), 2,
                                colorDepth
                            )
                        ));
                    }
                    else
                    {
                        // Add the data item
                        dataList.Add(new LZ77StoreShort<ColorModel.RGB>(
                            new ColorModel.RGB(
                                bitReader.ReadSmallBits(colorBitLength), colorBitLength,
                                bitReader.ReadSmallBits(colorBitLength), colorBitLength,
                                bitReader.ReadSmallBits(colorBitLength), colorBitLength,
                                colorDepth
                            )
                        ));
                    }
                }
            }

            return dataList.ToArray();
        }
    }
}
