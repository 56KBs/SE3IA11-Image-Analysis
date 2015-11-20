using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCompression.Helpers;
using ImageCompression.ExtensionMethods;

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
                var matchPosition = position - relativeMatchPosition;

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

        private static bool IsLongForm(byte b, int byteBitsRemaining)
        {
            return b.GetFirstBits(1) >> (byteBitsRemaining - 1) == 0;
        }

        private static byte GetByte(ref byte[] data, ref int byteIndex, ref int byteBitsRemaining, int bitLength)
        {
            var returnByte = (byte)0;

            // If we can directly take the information out, do so
            if (byteBitsRemaining == bitLength)
            {
                returnByte = data[byteIndex].GetOffsetBits(8 - byteBitsRemaining, bitLength);

                byteIndex++;
                byteBitsRemaining = 8;
            }
            // We only need to take our section out of the bit
            else if (byteBitsRemaining > bitLength)
            {
                returnByte = data[byteIndex].GetOffsetBits(8 - byteBitsRemaining, bitLength);
                byteBitsRemaining -= bitLength;
            }
            // We need to span two bits to get our data
            else
            {
                // Get the data from the first byte
                var partialByteStart = data[byteIndex].GetLastBits(byteBitsRemaining);
                var bitsToExtract = bitLength - byteBitsRemaining;

                byteIndex++;
                byteBitsRemaining = 8;

                // Get the data from the second byte
                var partialByteEnd = data[byteIndex].GetFirstBits(bitsToExtract);
                byteBitsRemaining -= bitsToExtract;

                // Merge the two partials
                returnByte = (byte)((byte)(partialByteStart << (bitLength - byteBitsRemaining)) | partialByteEnd);
            }

            return returnByte;
        }

        public static LZ77Store[] BytesToLZ77StoreArray<T>(byte[] byteData, ColorModel.RGB.ColorDepth colourDepth)
            where T : Interfaces.IEncodable
        {
            var dataList = new List<LZ77Store>();

            var byteIndex = 0;
            var byteBitsRemaining = 8;

            while (byteIndex < byteData.Length)
            {
                if (LZ77.IsLongForm(byteData[byteIndex], byteBitsRemaining))
                {
                    if (byteBitsRemaining == 1)
                    {
                        byteIndex++;
                        byteBitsRemaining = 8;
                    }
                    else
                    {
                        byteBitsRemaining--;
                    }
                    
                    dataList.Add(new LZ77StoreLong<ColorModel.RGB>(
                        LZ77.GetByte(ref byteData, ref byteIndex, ref byteBitsRemaining, 8),
                        LZ77.GetByte(ref byteData, ref byteIndex, ref byteBitsRemaining, 8)
                    ));
                }
                else
                {
                    if (byteBitsRemaining == 1)
                    {
                        byteIndex++;
                        byteBitsRemaining = 8;
                    }
                    else
                    {
                        byteBitsRemaining--;
                    }

                    var colorBitLength = (int)colourDepth / 3;

                    if (colourDepth == ColorModel.RGB.ColorDepth.Eight)
                    {
                        dataList.Add(new LZ77StoreShort<ColorModel.RGB>(
                            new ColorModel.RGB(
                                LZ77.GetByte(ref byteData, ref byteIndex, ref byteBitsRemaining, 3),
                                LZ77.GetByte(ref byteData, ref byteIndex, ref byteBitsRemaining, 3),
                                LZ77.GetByte(ref byteData, ref byteIndex, ref byteBitsRemaining, 2),
                                colourDepth
                            )
                        ));
                    }
                    else
                    {
                        dataList.Add(new LZ77StoreShort<ColorModel.RGB>(
                            new ColorModel.RGB(
                                LZ77.GetByte(ref byteData, ref byteIndex, ref byteBitsRemaining, colorBitLength),
                                LZ77.GetByte(ref byteData, ref byteIndex, ref byteBitsRemaining, colorBitLength),
                                LZ77.GetByte(ref byteData, ref byteIndex, ref byteBitsRemaining, colorBitLength),
                                colourDepth
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
