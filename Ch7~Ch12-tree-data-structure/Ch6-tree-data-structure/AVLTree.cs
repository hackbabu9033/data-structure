using Ch6_Ch7_tree_data_structure;
using Ch6_Ch7_tree_data_structure.Extension;
using Ch6_tree_data_structure.Enum;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ch6_tree_data_structure
{
    public class AVLTree<T> where T: IComparable<T>
    {
        public AVLTree():base()
        {
           
        }

        public T Data { get; set; }

        /// <summary>
        /// left node
        /// </summary>
        public AVLTree<T> Left { get; set; }

        /// <summary>
        /// right node
        /// </summary>
        public AVLTree<T> Right { get; set; }

        public AVLTree<T> Insert(T data)
        {
            AVLTree<T> pivot;
            AVLTree<T> parent;
            AVLTree<T> rotatedTree;
            var root = this;
            root.AddTreeNode(data);
            var unbalanceType = GetUnbalanceTreeTypeAfterInsert(root, data);
            if (!unbalanceType.HasValue)
            {
                return root;
            }
            switch (unbalanceType)
            {
                case AVLTreeUnBalanceType.LL:
                    pivot = root.Left;
                    return RightRotation(root, pivot);
                case AVLTreeUnBalanceType.LR:
                    parent = root.Left;
                    pivot = parent.Right;
                    rotatedTree = LeftRotation(root, pivot);
                    root.Left = rotatedTree;
                    return RightRotation(root, rotatedTree);
                case AVLTreeUnBalanceType.RR:
                    pivot = root.Right;
                    return LeftRotation(root, pivot);
                case AVLTreeUnBalanceType.RL:
                    parent = root.Right;
                    pivot = parent.Left;
                    rotatedTree = RightRotation(parent, pivot);
                    return LeftRotation(rotatedTree, rotatedTree.Right);
            }
            return root;

        }


        private AVLTree<T> RightRotation(AVLTree<T> parent, AVLTree<T> pivot)
        {
            var pivotRightSubTree = pivot.Right;
            var pivotLeftSubTree = pivot.Left;
            var orgParentNode = parent;
            parent = pivot;
            parent.Left = pivotLeftSubTree;
            parent.Right = orgParentNode;
            parent.Right.Left = pivotRightSubTree;
            return parent;
        }

        private AVLTree<T> LeftRotation(AVLTree<T> parent, AVLTree<T> pivot)
        {
            var pivotRightSubTree = pivot.Right;
            var pivotLeftSubTree = pivot.Left;
            var orgParentNode = parent;
            parent = pivot;
            parent.Right = pivotRightSubTree;
            parent.Left = orgParentNode;
            parent.Left.Right = pivotLeftSubTree;

            return parent;
        }

        public AVLTreeUnBalanceType? GetUnbalanceTreeTypeAfterInsert(AVLTree<T> rootNode, T data)
        {
            var rootBalanceFactor = CaculateNodebalanceFactor(rootNode);
            var leftNodeBalanceFactor = CaculateNodebalanceFactor(rootNode.Left);
            var rightNodeBalanceFactor = CaculateNodebalanceFactor(rootNode.Right);

            if (rootBalanceFactor == 2)
            {
                if (leftNodeBalanceFactor == 1)
                {
                    return AVLTreeUnBalanceType.LL;
                }
                if (leftNodeBalanceFactor == -1)
                {
                    return AVLTreeUnBalanceType.LR;
                }
            }
            else if (rootBalanceFactor == -2)
            {
                if (rightNodeBalanceFactor == 1)
                {
                    return AVLTreeUnBalanceType.RL;
                }
                if (rightNodeBalanceFactor == -1)
                {
                    return AVLTreeUnBalanceType.RR;
                }
            }
            return null;
        }

        public int CaculateNodebalanceFactor(AVLTree<T> treeNode)
        {
            if (treeNode == null)
            {
                return 0;
            }
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

            return 1 + Math.Max(leftSubTreeDepth, rightSubTreeDepth);
        }
    }

    public class RotationContent<T> where T : IComparable<T>
    {
        public AVLTreeUnBalanceType? UnbalanceType { get; set; }
        public AVLTree<T> Pivot { get; set; }

        public AVLTree<T> Root { get; set; }
    }
}
