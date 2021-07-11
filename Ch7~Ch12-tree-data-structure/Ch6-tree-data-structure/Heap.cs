using Ch6_tree_data_structure.Enum;
using System;
using System.Linq;

namespace Ch6_tree_data_structure
{
    public class Heap<T> where T : IComparable<T>
    {
        public int Max { get; set; }
        public T[] HeapTree { get; set; }
        public HeapCategory HeapType { get; set; }

        public int LastIndex { get; set; }

        public Heap(int heapMax)
        {
            Max = heapMax;
            HeapTree = new T[heapMax];
        }

        public void InsertNode(T data)
        {
            if (LastIndex >= Max - 1)
            {
                Console.WriteLine("heap is full");
                return;
            }
            var insertIndex = LastIndex + 1;
            HeapTree[insertIndex] = data;

            ReHeapifyFromIndex(insertIndex);
            LastIndex++;
            return;
        }


        public void DeleteNode(T data)
        {
            var deleteNode = HeapTree.Where(o => o.CompareTo(data) == 0).FirstOrDefault();
            if (deleteNode == null)
            {
                throw new ArgumentException("delete data doesn't exist in heap");
            }
            var lastNode = HeapTree[LastIndex];
            var deleteNodeIndex = Array.IndexOf(HeapTree, deleteNode);
            HeapTree[deleteNodeIndex] = lastNode;
            ReHeapifyFromIndex(deleteNodeIndex);
            LastIndex--;
        }

        // if curNode is large or less(depend on Max or Min Heap)
        // then swap with parent Node
        private void ReHeapifyFromIndex(int index)
        {
            while (IsNeedToSwapWithParent(index))
            {
                var temp = HeapTree[index];
                var parentIndex = (index - 1) / 2;
                HeapTree[index] = HeapTree[parentIndex];
                HeapTree[parentIndex] = temp;
                index = parentIndex;
            }
        }

        private bool IsNeedToSwapWithParent(int curIndex)
        {
            var parentIndex = (curIndex - 1) / 2;
            var compareResult = HeapTree[curIndex].CompareTo(HeapTree[parentIndex]);
            if (compareResult == 0)
            {
                throw new Exception("insert node value already exists");
            }
            if (HeapType == HeapCategory.Max)
            {
                return HeapTree[curIndex].CompareTo(HeapTree[parentIndex]) > 0;
            }
            else
            {
                return HeapTree[curIndex].CompareTo(HeapTree[parentIndex]) < 0;
            }
        }
    }
}
