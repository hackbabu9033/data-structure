﻿using ch6_tree;
using Ch6_Ch7_tree_data_structure.Extension;
using Ch6_Ch7_tree_data_structure.Helper;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using Ch6_tree_data_structure;
using Ch6_tree_data_structure.Enum;

namespace Ch6_Ch7_tree_data_structure
{
    class Program
    {
        static void Main(string[] args)
        {
            //var treeNode = NodeTreeJsonFileReader.GetTreeNodeFromData<int>("intTreeNode.json");
            //var threadBinTreeNode = ThreadBinaryTree<int>.ConvertToThreadBinTree(treeNode);
            //var node = treeNode.Search(13);
            //treeNode.Insert(25);

            //var maxHeap = new Heap<int>(50)
            //{
            //    HeapType = HeapCategory.Max
            //};
            //maxHeap.HeapTree[0] = 100;
            //maxHeap.HeapTree[1] = 80;
            //maxHeap.HeapTree[2] = 90;
            //maxHeap.HeapTree[3] = 70;
            //maxHeap.HeapTree[4] = 65;
            //maxHeap.HeapTree[5] = 50;
            //maxHeap.HeapTree[6] = 40;
            //maxHeap.HeapTree[7] = 60;
            //maxHeap.HeapTree[8] = 25;
            //maxHeap.HeapTree[9] = 17;
            //maxHeap.HeapTree[10] = 19;
            //maxHeap.LastIndex = 10;
            //maxHeap.InsertNode(95);

            var avlTree = new AVLTree<int>();
            avlTree.Data = 80;
            avlTree = avlTree.Insert(20);
            avlTree = avlTree.Insert(15);
            avlTree = avlTree.Insert(90);
            avlTree = avlTree.Insert(12);

        }
    }
}
