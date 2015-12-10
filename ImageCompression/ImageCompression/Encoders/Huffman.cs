using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Encoders
{
    public class Huffman<T>
        where T : Interfaces.IEncodable
    {
        public Dictionary<T, byte> symbolTable { get; private set; }

        public List<byte> dataList { get; private set; }

        public static Huffman<T> Empty
        {
            get { return new Huffman<T>(); }
        }

        public Huffman()
        {
            // Creates empty huffman list
        }

        public Huffman(Dictionary<T, byte> symbolTable, List<byte> dataList)
        {
            this.symbolTable = symbolTable;
            this.dataList = dataList;
        }

        public Huffman(Dictionary<T, byte> symbolTable, List<T> dataList)
        {
            this.symbolTable = symbolTable;
            this.AddData(dataList);
        }

        public void AddSymbolTable(List<HuffmanNode<T>> nodeList)
        {
            symbolTable = new Dictionary<T, byte>();

            foreach (HuffmanNode<T> node in nodeList)
            {
                symbolTable.Add(node.data, node.SymbolAsByte());
            }
        }

        public void AddData(List<T> rawData)
        {
            dataList = new List<byte>();

            foreach (T dataItem in rawData)
            {
                dataList.Add(symbolTable[dataItem]);
            }
        }

        public static Huffman<T> Encode(T[] rawData)
        {
            return Huffman<T>.Encode(rawData.ToList());
        }

        public static Huffman<T> Encode(List<T> rawData)
        {
            var returnHuffmanData = Huffman<T>.Empty;

            var groupedData = rawData.GroupBy(x => x);

            var nodeList = new List<HuffmanNode<T>>();

            // Build up the basic node list
            foreach (var grouping in groupedData)
            {
                nodeList.Add(new HuffmanNode<T>(grouping.Key, grouping.Count()));
            }

            // Keep looping until we just have a single HuffmanNode which is a tree of all the data
            while (nodeList.Count > 1)
            {
                // Order the nodeList
                var orderedNodes = nodeList.OrderBy(node => node.frequency).ToList();

                // Take the first two nodes (Lowest frequencies) and join them
                nodeList.Add(HuffmanNode<T>.JoinNodes(orderedNodes[0], orderedNodes[1]));
                nodeList.Remove(orderedNodes[0]);
                nodeList.Remove(orderedNodes[1]);
            }

            // Extract all the nodes into a list as we now have all the symbols required
            returnHuffmanData.AddSymbolTable(nodeList[0].ExtractNodes());

            // Add the raw data to the data list
            returnHuffmanData.AddData(rawData);



            /* Return the following data:
             *
             * Length of symbol table
             * Size of symbols (int)
             * Symbol table
             * Encoded data
             */

            // Build the symbol table

            // Find the longest symbol size

            var groupedDataToDoubleCheck = returnHuffmanData.symbolTable.GroupBy(s => s.Key);
            
            return returnHuffmanData;
        }

        public static List<T> Decode(List<HuffmanNode<T>> rawData)
        {
            var dataList = new List<T>();

            return dataList;
        }
    }
}
