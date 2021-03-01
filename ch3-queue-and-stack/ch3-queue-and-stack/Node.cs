using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ch3_queue_and_stack
{
    public class Node<T>
    {
        public T Value { set; get; }
        public Node<T> Left { set; get; }
        public Node<T> Right { set; get; }

        public NodePos? Pos { set; get; }

        public Node<T> Parent { set; get; }

        public Node(T value, Node<T> parent = null, NodePos? nodePos = null)
        {
            this.Value = value;
            Left = null;
            Right = null;

            if (parent != null)
            {
                this.Parent = parent;
            }

            if (nodePos != null)
            {
                Pos = nodePos;
            }
        }

        public Node(Node<T> parent = null, NodePos? nodePos = null)
        {
            this.Value = default;
            Left = null;
            Right = null;

            if (parent != null)
            {
                this.Parent = parent;
            }

            if (nodePos != null)
            {
                Pos = nodePos;
            }
        }
    }
    public class BinaryTree<T>
    {

        public Node<T> Nodes { set; get; }
        public void CreateBinaryTree()
        {
            this.Nodes = new Node<T>();
        }

        public static void AppendChildNodes(Node<T> node)
        {
            node.Left = new Node<T>(node);
            node.Right = new Node<T>(node);
        }


        /// <summary>
        /// convert IEnumerable<T> to binaryTree
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Items"></param>
        /// <returns></returns>
        public static Node<T> CreateBinaryTree(IEnumerable<T> Items)
        {
            var node = new Node<T>();
            var nodeQueue = new Queue<Node<T>>();
            nodeQueue.Enqueue(node);
            foreach (var item in Items)
            {
                var addedNode = DequeueForCurNode(nodeQueue);

                addedNode.Value = item;
            }
            return node;
        }

        public static void PrintTreeNodes(Node<T> rootNodes)
        {
            var nodeQueue = new Queue<Node<T>>();
            Console.WriteLine("--- print all nodes in tree ---");
            nodeQueue.Enqueue(rootNodes);
            while (nodeQueue.Count > 0)
            {
                var removedNode = nodeQueue.Dequeue();
                if (removedNode.Left != null)
                {
                    nodeQueue.Enqueue(removedNode.Left);
                }
                if (removedNode.Right != null)
                {
                    nodeQueue.Enqueue(removedNode.Right);
                }
                Console.WriteLine($"current node value is : {removedNode.Value}");
            }
        }

        private static Node<T> DequeueForCurNode(Queue<Node<T>> nodeQueue)
        {
            var addedNode = nodeQueue.Dequeue();
            var leftNode = new Node<T>(addedNode, NodePos.left);
            var rightNode = new Node<T>(addedNode, NodePos.right);
            nodeQueue.Enqueue(leftNode);
            nodeQueue.Enqueue(rightNode);
            if (addedNode.Parent != null)
            {
                switch (addedNode.Pos)
                {
                    case NodePos.left:
                        addedNode.Parent.Left = addedNode;
                        break;
                    case NodePos.right:
                        addedNode.Parent.Right = addedNode;
                        break;
                    default:
                        break;
                }
            }
            return addedNode;
        }
    }

    /// <summary>
    /// convert list to max/min heap
    /// </summary>
    public class Heapify
    {
        public static List<int> GetMaxHeap(List<int> items)
        {
            var size = items.Count;
            int startIndex = (size / 2) - 1;
            for (int i = startIndex; i >= 0; i--)
            {
                HeapifyMax(items, i);
            }
            return items;
        }

        public static List<int> GetMinHeap(List<int> items)
        {
            var size = items.Count;
            int startIndex = (size / 2) - 1;
            for (int i = startIndex; i >= 0; i--)
            {
                HeapifyMin(items, i);
            }
            return items;
        }

        private static void HeapifyMin(List<int> items, int smallestIndex)
        {
            var i = smallestIndex;
            var leftChildIndex = 2 * smallestIndex + 1;
            var rightChildIndex = 2 * smallestIndex + 2;

            if (leftChildIndex < items.Count && items[leftChildIndex] < items[i])
                i = leftChildIndex;


            if (rightChildIndex < items.Count && items[rightChildIndex] < items[i])
                i = rightChildIndex;

            if (i != smallestIndex)
            {
                items[i] = items[i] + items[smallestIndex];
                items[smallestIndex] = items[i] - items[smallestIndex];
                items[i] = items[i] - items[smallestIndex];
                HeapifyMin(items, i);
            }
        }

        private static void HeapifyMax(List<int> items, int largestIndex)
        {
            var i = largestIndex;
            var leftChildIndex = 2 * largestIndex + 1;
            var rightChildIndex = 2 * largestIndex + 2;

            if (leftChildIndex < items.Count && items[leftChildIndex] > items[i])
                i = leftChildIndex;


            if (rightChildIndex < items.Count && items[rightChildIndex] > items[i])
                i = rightChildIndex;

            if (i != largestIndex)
            {
                items[i] = items[i] + items[largestIndex];
                items[largestIndex] = items[i] - items[largestIndex];
                items[i] = items[i] - items[largestIndex];
                HeapifyMax(items, i);
            }

        }
    }

    public class Heap
    {
        public List<int> Data { set; get; }
        public Node<int> Tree { set; get; }
        public HeapType HeapType { set; get; }

        public delegate List<int> HeapMethod(List<int> data);

        public HeapMethod ReHeapify { set; get; }

        public Heap(List<int> data, HeapType heapType)
        {
            var cloneData = new List<int>();
            foreach (var item in data)
            {
                cloneData.Add(item);
            }
            switch (heapType)
            {
                case HeapType.Max:
                    ReHeapify = new HeapMethod(Heapify.GetMaxHeap);
                    break;
                case HeapType.Min:
                    ReHeapify = new HeapMethod(Heapify.GetMinHeap);
                    break;
                default:
                    break;
            }
            Data = ReHeapify(cloneData);
            Tree = BinaryTree<int>.CreateBinaryTree(Data);
            HeapType = heapType;
        }

        public Node<int> Insert(int item)
        {
            Data.Add(item);
            var reheapedResult = ReHeapify(Data);
            // reheap list after add a new object
            Data = reheapedResult;
            // create node from current tree
            Tree = BinaryTree<int>.CreateBinaryTree(Data);
            return Tree;
        }

        public Node<int> Delete(int item)
        {
            if (!Data.Contains(item))
            {
                throw new Exception($"{item} didn't exist");
            }
            Data.Remove(item);
            var reheapedResult = ReHeapify(Data);
            Data = reheapedResult;
            Tree = BinaryTree<int>.CreateBinaryTree(Data);
            return Tree;
        }

        public int Peek()
        {
            return Data.First();
        }

        public int extract()
        {
            if (Data.Count <= 0)
            {
                return int.MinValue;
            }
            var removeValue = Data.First();
            Data.Remove(removeValue);
            return removeValue;
        }
    }
}
