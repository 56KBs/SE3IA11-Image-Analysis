using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCompression.ExtensionMethods;

namespace ImageCompression.Encoders
{
    public class HuffmanNode<T>
        where T : Interfaces.IEncodable
    {
        public int frequency { get; private set; }

        public string symbol { get; private set; }

        public T data { get; private set; }

        public HuffmanNode<T> left { get; private set; }

        public HuffmanNode<T> right { get; private set; }

        public HuffmanNode(T data, int frequency)
        {
            this.data = data;
            this.frequency = frequency;
            this.symbol = "";
        }

        public HuffmanNode()
        {
            // Empty node
            this.frequency = 0;
            this.symbol = "";
        }

        private void UpdateSymbol(bool right)
        {
            if (this.IsLeaf())
            {
                symbol = Convert.ToInt32(right).ToString() + symbol;
            }
            else
            {
                if (this.left != null)
                {
                    this.left.UpdateSymbol(right);
                }

                if (this.right != null)
                {
                    this.right.UpdateSymbol(right);
                }
            }           
        }

        public void SetLeftNode(HuffmanNode<T> node)
        {
            node.UpdateSymbol(false);

            this.frequency += node.frequency;
            this.left = node;
        }

        public void SetRightNode(HuffmanNode<T> node)
        {
            node.UpdateSymbol(true);

            this.frequency += node.frequency;
            this.right = node;
        }

        public byte SymbolAsByte()
        {
            var length = this.symbol.Length;

            var dataByte = (byte)0;

            for (var i = 0; i < length; i++)
            {
                dataByte = dataByte.PushBit(Convert.ToBoolean(Char.GetNumericValue(symbol[i])));
            }

            return dataByte;
        }

        public static HuffmanNode<T> JoinNodes(HuffmanNode<T> leftNode, HuffmanNode<T> rightNode)
        {
            var emptyNode = new HuffmanNode<T>();

            emptyNode.SetLeftNode(leftNode);
            emptyNode.SetRightNode(rightNode);

            return emptyNode;
        }

        public List<HuffmanNode<T>> ExtractNodes()
        {
            var returnList = new List<HuffmanNode<T>>();

            if (this.IsLeaf())
            {
                return new List<HuffmanNode<T>> { this };
            }
            else
            {
                if (this.left != null)
                {
                    returnList = this.left.ExtractNodes();
                }

                if (this.right != null)
                {
                    if (returnList.Count == 0)
                    {
                        returnList = this.right.ExtractNodes();
                    }
                    else
                    {
                        var extractedNodes = this.right.ExtractNodes();

                        foreach (HuffmanNode<T> node in extractedNodes)
                        {
                            returnList.Add(node);
                        }
                    }
                }

                return returnList;
            }
        }

        public bool IsLeaf()
        {
            return left == null && right == null ? true : false;
        }
    }
}
