using Ch6_Ch7_tree_data_structure;
using Ch6_Ch7_tree_data_structure.Extension;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ch6_tree_data_structure
{
    public class AVLTree<T> : BinaryTreeNode<T> where T: IComparable<T>
    {
        public AVLTree():base()
        {
           
        }

        /// <summary>
        /// left node
        /// </summary>
        public AVLTree<T> Left { get; set; }

        /// <summary>
        /// right node
        /// </summary>
        public AVLTree<T> Right { get; set; }

        public void Insert(AVLTree<T> root,T data)
        {
            // LL

            // LR

            // RR

            // RL
        }


        private void RightRotation(AVLTree<T> parent, AVLTree<T> pivot)
        {
            var pivotRightSubTree = pivot.Right;
            var pivotLeftSubTree = pivot.Left;
            var parentData = parent.Data;
            // swap parent and pivot data
            parent.Data = pivot.Data;

            pivot.Left = pivotLeftSubTree;

            pivot.Right.Data = parentData;
            pivot.Right.Left = pivotRightSubTree;
            pivot.Right.Right = parent.Right;
        }

        private void LeftRotation(AVLTree<T> parent, AVLTree<T> pivot)
        {
            var pivotRightSubTree = pivot.Right;
            var pivotLeftSubTree = pivot.Left;
            var parentData = parent.Data;
            // swap parent and pivot data
            parent.Data = pivot.Data;

            pivot.Right = pivotRightSubTree;

            pivot.Left.Data = parentData;
            pivot.Left.Left = parent.Left;
            pivot.Left.Right = pivotLeftSubTree;
        }

        //public int? GetUnbalanceTreeTypeAfterInsert(AVLTree<T> rootNode,T data)
        //{
        //    rootNode.Insert(data);
        //    var queue = new Queue<AVLTree<T>>();
        //    if (rootNode.Data == null)
        //    {
        //        rootNode.Data = data;
        //        return null;
        //    }
        //    queue.Enqueue(rootNode);
        //    while (queue.Count > 0)
        //    {
        //        var popup = queue.Dequeue();
        //        if (popup.Left != null)
        //        {
        //            queue.Enqueue(popup.Left);
        //        }
        //        if (popup.Right != null)
        //        {
        //            queue.Enqueue(popup.Right);
        //        }

        //        if (popup.Data.CompareTo(data) == 0)
        //        {
                    
        //        }
        //    }
        //}

        public int CaculateNodebalanceFactor(AVLTree<T> treeNode)
        {
            return GetTreeNodeMaxDepth(treeNode.Left) - GetTreeNodeMaxDepth(treeNode.Right);
        }

        private int GetTreeNodeMaxDepth(AVLTree<T> node)
        {
            int leftSubTreeDepth = 0;
            int rightSubTreeDepth = 0;
            if (node == null)
            {
                return 0;
            }
            if (node.Left != null)
            {
                leftSubTreeDepth = GetTreeNodeMaxDepth(node.Left);
            }

            if (node.Right != null)
            {
                rightSubTreeDepth = GetTreeNodeMaxDepth(node.Right);
            }

            return Math.Max(1 + leftSubTreeDepth, 1 + rightSubTreeDepth);
        }
    }
}
