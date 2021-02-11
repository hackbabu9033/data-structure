using System;
using System.Collections.Generic;

namespace ch3_queue_and_stack
{
    class Program
    {
        static void Main(string[] args)
        {
            #region circleQueue exercise
            var intCircleQueue = new CircleQueue<int>(5);
          
            intCircleQueue.Enqueue(1);
            intCircleQueue.Dequeue();
            intCircleQueue.Enqueue(2);
            intCircleQueue.Enqueue(3);
            intCircleQueue.Enqueue(4);
            intCircleQueue.Enqueue(5);
            intCircleQueue.Dequeue();
            intCircleQueue.Dequeue();
            intCircleQueue.Dequeue();
            intCircleQueue.Dequeue();
            intCircleQueue.Enqueue(10);
            intCircleQueue.Enqueue(8);
            #endregion

            #region covert collection to complete binaryTree
            var arrayList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var trees = BinaryTree<int>.CreateBinaryTree(arrayList);
            BinaryTree<int>.PrintTreeNodes(trees);
            #endregion

            #region heapify to max-heap
            var datas = new List<int>() { 1, 5, 8, 3, 4, 5, 6, 7 };
            var maxHeap = new Heap(datas,HeapType.Max);
            var minHeap = new Heap(datas,HeapType.Min);
            BinaryTree<int>.PrintTreeNodes(maxHeap.Tree);
            BinaryTree<int>.PrintTreeNodes(minHeap.Tree);
            #endregion
        }
    }
}
