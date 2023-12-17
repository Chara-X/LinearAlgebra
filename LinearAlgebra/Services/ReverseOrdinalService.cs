using System.Collections.Generic;
using System.Linq;

namespace LinearAlgebra.Services
{
    public class ReverseOrdinalService
    {
        private Node _root = new Node();

        public int Compute(double[] items)
        {
            return CountSmaller(items).Sum();
        }

        public IList<int> CountSmaller(double[] items)
        {
            _root = new Node();
            var res = new int[items.Length];
            for (var i = items.Length - 1; i >= 0; i--)
            {
                res[i] = Re(_root, items[i]);
            }

            return res.ToList();
        }

        private static int Re(Node node, double num)
        {
            while (true)
            {
                if (node.IsLeaf)
                {
                    node.ToEntity(num);
                    return 0;
                }

                if (num > node.Val) return Re(node.R, num) + node.LSize + 1;
                node.LSize++;
                node = node.L;
            }
        }

        public class Node
        {
            public double Val;
            public int LSize;
            public bool IsLeaf;
            public Node L;
            public Node R;

            public Node()
            {
                IsLeaf = true;
            }

            public void ToEntity(double val)
            {
                Val = val;
                IsLeaf = false;
                L = new Node();
                R = new Node();
            }
        }
    }
}
