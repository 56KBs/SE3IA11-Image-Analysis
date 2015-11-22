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

        public static byte[] Pack(List<VariableByte[]> variableBytes)
        {
            var byteList = new List<byte>();
            byteList.Add(0); // Add empty value for doing operations on

            var byteListIndex = 0;
            var byteBitsRemaining = 8;

            foreach (VariableByte[] byteArray in variableBytes)
            {
                foreach (VariableByte byteValue in byteArray)
                {
                    var byteValueLength = byteValue.bitLength.ToInt();

                    PackVariableByte(ref byteList, ref byteListIndex, ref byteBitsRemaining, byteValue);
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

        private static void PackVariableByte(ref List<byte> byteList, ref int byteListIndex, ref int byteBitsRemaining, VariableByte variableByte)
        {
            var variableByteLength = variableByte.bitLength.ToInt();

            // Can merge in exactly, or with space to go
            if (byteBitsRemaining >= variableByteLength)
            {
                byteList[byteListIndex] = byteList[byteListIndex].PushBits(variableByte.ToFullByte(), ref byteBitsRemaining, variableByteLength);
                
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
                var partialByteStart = variableByte.ToFullByte().GetOffsetBits(8 - variableByteLength, byteBitsRemaining);
                //var partialByteStart = (byte)(variableByte.ToFullByte() >> byteBitsRemaining);

                var remainingBits = variableByteLength - byteBitsRemaining;

                // Push the byte in
                byteList[byteListIndex] = byteList[byteListIndex].PushBits(partialByteStart, ref byteBitsRemaining, byteBitsRemaining);
                
                if (byteBitsRemaining == 0)
                {
                    byteList.Add(0);
                    byteListIndex++;
                    byteBitsRemaining = 8;
                }

                // Get the bits that still remain
                var partialByteEnd = variableByte.ToFullByte().GetLastBits(remainingBits);

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

        // This packing methodology works only if the type of data packed is known
        public static byte[] Pack(List<VariableByte[]> variableBytes, int forcetobeanother)
        {
            var bytes = new List<byte>();
            bytes.Add(0);

            var currentPackingIndex = 0;
            var currentPackingLengthRemaining = 8;

            foreach (VariableByte[] variableByteArray in variableBytes)
            {
                for (var i = 0; i < variableByteArray.Length; i++)
                {
                    var variableByte = variableByteArray[i];

                    var currentVariableByteLength = variableByte.bitLength.ToInt();

                    // Our variable byte fits perfectly!
                    if (currentPackingLengthRemaining == currentVariableByteLength)
                    {
                        bytes[currentPackingIndex] = variableByte.ToFullByte();

                        if (i < variableByteArray.Length - 1)
                        {
                            bytes.Add(0);
                            currentPackingIndex++;
                            currentPackingLengthRemaining = 8;
                        }
                    }
                    // Variable byte fits with space to go, so fit it in
                    else if (currentPackingLengthRemaining > currentVariableByteLength)
                    {
                        // Push our variable byte in via a bit shift and a bitwise XOR
                        bytes[currentPackingIndex] = (byte)(bytes[currentPackingIndex] << currentVariableByteLength);
                        bytes[currentPackingIndex] = (byte)(bytes[currentPackingIndex] ^ variableByte.ToFullByte());

                        // Update the length of data left to pack
                        currentPackingLengthRemaining -= currentVariableByteLength;
                    }
                    // Variable byte doesn't fit fully, so split it up
                    else
                    {
                        // Mark how much data we can't fit in
                        var currentVariableByteRemaining = currentVariableByteLength - currentPackingLengthRemaining;

                        // Push in what we can of our variable byte via a bit shift and a bitwise XOR on the shifted data that can fit
                        bytes[currentPackingIndex] = (byte)(bytes[currentPackingIndex] << currentPackingLengthRemaining);
                        bytes[currentPackingIndex] = (byte)(bytes[currentPackingIndex] ^ (variableByte.ToFullByte() >> currentVariableByteRemaining));

                        // Move our indexer along
                        bytes.Add(0);
                        currentPackingIndex++;
                        // Reset the packing size remaining
                        currentPackingLengthRemaining = 8;

                        // Get the mask we need to get the end data
                        var mask = (byte)(RearMasks)Enum.Parse(typeof(RearMasks), currentVariableByteRemaining.ToString());

                        // Remaining variable byte will fit with space to go, so put it in
                        bytes[currentPackingIndex] = (byte)(bytes[currentPackingIndex] << currentVariableByteRemaining);
                        bytes[currentPackingIndex] = (byte)(bytes[currentPackingIndex] ^ (variableByte.ToFullByte() & mask));

                        // Update the length of data left to pack
                        currentPackingLengthRemaining -= currentVariableByteRemaining;
                    }
                }
            }

            return bytes.ToArray();
        }
    }
}
