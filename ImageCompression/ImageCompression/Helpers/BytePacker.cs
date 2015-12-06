using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCompression.ExtensionMethods;

namespace ImageCompression.Helpers
{
    public static class BytePacker
    {
        /// <summary>
        /// Masks to only reveal the first X bits
        /// </summary>
        [Flags]
        public enum FrontMasks
        {
            One = 0x01,
            Two = 0x03,
            Three = 0x07,
            Four = 0x0F,
            Five = 0x1F,
            Six = 0x3F,
            Seven = 0x7F,
            Eight = 0xFF
        }

        /// <summary>
        /// Masks to only reveal the last X bits
        /// </summary>
        [Flags]
        public enum RearMasks
        {
            One = 0xFE,
            Two = 0xFC,
            Three = 0xF8,
            Four = 0xF0,
            Five = 0xE0,
            Six = 0xC0,
            Seven = 0x80,
            Eight = 0x00
        }

        public static byte[] Pack(List<Interfaces.IEncodable> encodableList)
        {
            var byteList = new List<byte>();
            byteList.Add(0); // Add empty value for doing operations on

            var byteListIndex = 0;
            var byteBitsRemaining = 8;
            var byteLengthPatternIndex = 0;

            foreach (Interfaces.IEncodable encodableData in encodableList)
            {
                var byteLengthPatternCount = encodableData.bytePattern.Length;

                var byteData = encodableData.ToByteArray();
                foreach (byte byteValue in byteData)
                {
                    PackVariableByte(ref byteList, ref byteListIndex, ref byteBitsRemaining, byteValue, encodableData.bytePattern[byteLengthPatternIndex]);

                    if (byteLengthPatternIndex == byteLengthPatternCount - 1)
                    {
                        byteLengthPatternIndex = 0;
                    }
                    else
                    {
                        byteLengthPatternIndex++;
                    }
                }
            }

            // Tidy up the list

            // If the last item has bits remaining, pad out the byte
            if (byteBitsRemaining < 8)
            {
                byteList[byteListIndex] = byteList[byteListIndex].Pad(ref byteBitsRemaining);
            }
            // The item is full so we have a trailing empty byte, remove it
            else
            {
                byteList.RemoveAt(byteListIndex);
                byteListIndex--;
            }

            return byteList.ToArray();
        }

        private static void PackVariableByte(ref List<byte> byteList, ref int byteListIndex, ref int byteBitsRemaining, byte dataByte, byte byteLength)
        {
            // Can merge in exactly, or with space to go
            if (byteBitsRemaining >= byteLength)
            {
                byteList[byteListIndex] = byteList[byteListIndex].PushBits(dataByte, ref byteBitsRemaining, byteLength);
                
                if (byteBitsRemaining == 0)
                {
                    byteList.Add(0);
                    byteListIndex++;
                    byteBitsRemaining = 8;
                }
            }
            // Requires a merge over two bytes
            else
            {
                // Get the bits that will fit
                var partialByteStart = dataByte.GetOffsetBits(8 - byteLength, byteBitsRemaining);
                //var partialByteStart = (byte)(variableByte.ToFullByte() >> byteBitsRemaining);

                var remainingBits = byteLength - byteBitsRemaining;

                // Push the byte in
                byteList[byteListIndex] = byteList[byteListIndex].PushBits(partialByteStart, ref byteBitsRemaining, byteBitsRemaining);
                
                if (byteBitsRemaining == 0)
                {
                    byteList.Add(0);
                    byteListIndex++;
                    byteBitsRemaining = 8;
                }

                // Get the bits that still remain
                var partialByteEnd = dataByte.GetLastBits(remainingBits);

                // Push the byte in
                byteList[byteListIndex] = byteList[byteListIndex].PushBits(partialByteEnd, ref byteBitsRemaining, remainingBits);
                if (byteBitsRemaining == 0)
                {
                    byteList.Add(0);
                    byteListIndex++;
                    byteBitsRemaining = 8;
                }
            }
        }
    }
}
