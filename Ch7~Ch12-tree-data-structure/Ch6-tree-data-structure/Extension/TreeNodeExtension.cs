using Ch6_tree_data_structure;
using System;
using System.Collections.Generic;

namespace Ch6_Ch7_tree_data_structure.Extension
{
    public static class TreeNodeExtension
    {
        /// <summary>
        /// search binary Tree using specific value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="binaryTreeRoot">binaryTree root node</param>
        /// <param name="data">search data value</param>
        /// <returns></returns>
        public static BinaryTreeNode<T> Search<T>(this BinaryTreeNode<T> binaryTreeRoot,T data) where T : IComparable<T>
        {
            if (binaryTreeRoot == null)
            {
                return null;
            }
            var compareResult = data.CompareTo(binaryTreeRoot.Data);
            if (compareResult == 0)
            {
                return binaryTreeRoot;
            }
            else if (compareResult <0)
            {
                return Search(binaryTreeRoot.Llink, data);
            }
            else
            {
                return Search(binaryTreeRoot.Rlink, data);
            }
        }

        public static void Insert<T>(this BinaryTreeNode<T> binaryTreeNode,T data) where T:IComparable<T>
        {
            if (binaryTreeNode == null)
            {
                binaryTreeNode.Data = data;
                return;
            }
            var curNode = binaryTreeNode;
            BinaryTreeNode<T> preNode = null;
            while (curNode != null)
            {
                var compareResult = data.CompareTo(curNode.Data);
                preNode = curNode;
                if (compareResult == 0)
                {
                    throw new ArgumentException("input data has already in this tree");
                }
                else if (compareResult < 0)
                {
                    curNode = curNode.Llink;
                }
                else
                {
                    curNode = curNode.Rlink;
                }
            }

            var newAddNode = new BinaryTreeNode<T>()
            {
                Data = data
            };
            if (data.CompareTo(preNode.Data) > 0)
            {
                preNode.Rlink = newAddNode;
            }
            else
            {
                preNode.Llink = newAddNode;
            }
        }
    }
}
