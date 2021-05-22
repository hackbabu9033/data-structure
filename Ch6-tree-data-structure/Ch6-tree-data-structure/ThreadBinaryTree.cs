using Ch6_tree_data_structure;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ch6_tree
{
    /// <summary>
    /// ThreadBinaryTree和binTree的差異
    /// 1. 搜尋時不需要使用stack或queue的方式遍歷節點，因為每個節點總是會指到以中序搜尋中的對應節點
    /// 2. 但cud的時候就相對麻煩，因為要把thread重排
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ThreadBinaryTree<T>
    {
        public T Data { set; get; }
        public ThreadBinaryTree<T> Llink { set; get; }
        public ThreadBinaryTree<T> Rlink { set; get; }
        public bool LBit { set; get; }
        public bool RBit { set; get; }

        public static ThreadBinaryTree<T> ConvertToThreadBinTree(BinaryTreeNode<T> tree)
        {
            var InorderResult = new List<BinaryTreeNode<T>>();
            GetInOrdertList(tree,InorderResult);
            var ThreadBinaryTree = new ThreadBinaryTree<T>();
            ThreadBinaryTree<T> curTreeNode = new ThreadBinaryTree<T>();
            var stack = new Stack<BinaryTreeNode<T>>();
            stack.Push(tree);
            ThreadBinaryTree.Data = tree.Data;
            while (stack.Count > 0)
            {
                var popup = stack.Pop();

                if (popup.Llink != null)
                {
                    stack.Push(popup.Llink);
                }
                else
                {
                    // 這裡應該要丟ThreadBinaryTree的node..
                    BindingToNeighborNode(popup, InorderResult, -1);
                }

                if (popup.Rlink != null)
                {
                    stack.Push(popup.Rlink);
                }
                else
                {
                    BindingToNeighborNode(popup, InorderResult, 1);
                }

            }

            return null;
        }

        private static void GetInOrdertList(BinaryTreeNode<T> tree, List<BinaryTreeNode<T>> result)
        {
            if (tree != null)
            {
                GetInOrdertList(tree.Llink, result);
                result.Add(tree);
                GetInOrdertList(tree.Rlink, result);
            }
        }

        private static void BindingToNeighborNode(BinaryTreeNode<T> node, List<BinaryTreeNode<T>> inOrderNodes,int flag)
        {
            var matchedNode = inOrderNodes.Find(o=>o.NodeId == node.NodeId);
            var isRightNode = (flag > 0) ? 1 : -1;
            var neighborIndex = inOrderNodes.IndexOf(matchedNode) + isRightNode;
            if (neighborIndex < inOrderNodes.Count && neighborIndex > 0)
            {
                var neighborNode = inOrderNodes[neighborIndex];
                var reAssignNode = (flag > 0) ? node.Rlink : node.Llink;
                reAssignNode = neighborNode;
            }
        }
    }
}
