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
        public static List<LZ77Store> Encode<T>(List<T> data, int windowSize, int lookaheadSize)
            where T : Interfaces.IEncodable
        {
            return LZ77.Encode(data.ToArray(), windowSize, lookaheadSize).ToList();
        }

        private static int AddQueueItems<T>(ref T[] data, ref LimitedQueue<T> queue, int startPosition, int count)
        {
            var endPosition = startPosition;

            for (var i = 0; i < count; i++, endPosition++)
            {
                queue.Enqueue(data[startPosition + i]);
            }

            return endPosition;
        }

        public static LZ77Store[] Encode<T>(T[] data, int windowSize, int lookaheadSize)
            where T : Interfaces.IEncodable
        {
            var outputList = new List<LZ77Store>();

            var window = new Helpers.LimitedQueue<T>(windowSize);
            var lookahead = new Helpers.LimitedQueue<T>(lookaheadSize);

            var position = 0;
            var lookaheadEnd = 0;
            var windowEnd = 0;

            // Add first item
            // Nothing in the window, so just output the first item as-is
            outputList.Add(new LZ77StoreShort<T>(data[position]));

            position++;
            lookaheadEnd++;

            windowEnd = LZ77.AddQueueItems(ref data, ref window, windowEnd, 1);

            if (lookahead.size > data.Length)
            {
                lookahead = new Helpers.LimitedQueue<T>(data.Length - 1);
            }

            lookaheadEnd = LZ77.AddQueueItems(ref data, ref lookahead, lookaheadEnd, lookahead.size);

            while (position < data.Length)
            {
                var relativeMatchPosition = 0;
                var matchLength = LZ77.LongestMatch(window.ToArray(), lookahead.ToArray(), out relativeMatchPosition);
                var matchPosition = window.Count - relativeMatchPosition;

                var incrementBy = 1;

                // Long store needed
                if (matchLength != 0)
                {
                    outputList.Add(new LZ77StoreLong<T>(matchPosition, matchLength));
                    incrementBy = matchLength;
                }
                else
                {
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

        public static int LongestMatch<T>(T[] window, T[] buffer, out int longestMatchStart)
            where T : Interfaces.IEncodable
        {
            var windowSize = window.Length;
            var bufferSize = buffer.Length;

            longestMatchStart = 0;
            var longestMatch = 0;

            var currentMatchStart = 0;
            var currentMatch = 0;

            // Can the last item in the window match the first buffer item?
            if (window[windowSize - 1].Equals(buffer[0]))
            //if (window[windowSize - 1].ToByteArray().SequenceEqual(buffer[0].ToByteArray()))
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
                //if (window[i].ToByteArray().SequenceEqual(buffer[0].ToByteArray()))
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

                    var currentPosition = decodedData.Count - longData.position;

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

        private static bool IsLongForm(ref BitReader bitReader)
        {
            return !bitReader.ReadBoolean();
        }

        public static LZ77Store[] DecodeBinaryStream(ref BinaryReader binaryReader, ColorModel.RGB.ColorDepth colorDepth)
        {
            var dataList = new List<LZ77Store>();

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
                    var colorBitLength = (int)colorDepth / 3;

                    if (colorDepth == ColorModel.RGB.ColorDepth.Eight)
                    {
                        dataList.Add(new LZ77StoreShort<ColorModel.RGB>(
                            new ColorModel.RGB(
                                bitReader.ReadSmallBits(3),
                                bitReader.ReadSmallBits(3),
                                bitReader.ReadSmallBits(2),
                                colorDepth
                            )
                        ));
                    }
                    else
                    {
                        dataList.Add(new LZ77StoreShort<ColorModel.RGB>(
                            new ColorModel.RGB(
                                bitReader.ReadSmallBits(colorBitLength),
                                bitReader.ReadSmallBits(colorBitLength),
                                bitReader.ReadSmallBits(colorBitLength),
                                colorDepth
                            )
                        ));
                    }
                }
            }

            return dataList.ToArray();
        }
 
        public static Helpers.BytePacker.FrontMasks GetFrontByteMask(int numberToMask)
        {
            switch (numberToMask)
            {
                case 0:
                    return 0x00;
                case 1:
                    return Helpers.BytePacker.FrontMasks.One;
                case 2:
                    return Helpers.BytePacker.FrontMasks.Two;
                case 3:
                    return Helpers.BytePacker.FrontMasks.Three;
                case 4:
                    return Helpers.BytePacker.FrontMasks.Four;
                case 5:
                    return Helpers.BytePacker.FrontMasks.Five;
                case 6:
                    return Helpers.BytePacker.FrontMasks.Six;
                case 7:
                    return Helpers.BytePacker.FrontMasks.Seven;
                case 8:
                    return Helpers.BytePacker.FrontMasks.Eight;
                default:
                    throw new ArgumentOutOfRangeException("numberToMask");
            }
        }

        public static Helpers.BytePacker.RearMasks GetRearByteMask(int numberToMask)
        {
            switch (numberToMask)
            {
                case 0:
                    return 0x00;
                case 1:
                    return Helpers.BytePacker.RearMasks.One;
                case 2:
                    return Helpers.BytePacker.RearMasks.Two;
                case 3:
                    return Helpers.BytePacker.RearMasks.Three;
                case 4:
                    return Helpers.BytePacker.RearMasks.Four;
                case 5:
                    return Helpers.BytePacker.RearMasks.Five;
                case 6:
                    return Helpers.BytePacker.RearMasks.Six;
                case 7:
                    return Helpers.BytePacker.RearMasks.Seven;
                case 8:
                    return Helpers.BytePacker.RearMasks.Eight;
                default:
                    throw new ArgumentOutOfRangeException("numberToMask");
            }
        }
    }
}
