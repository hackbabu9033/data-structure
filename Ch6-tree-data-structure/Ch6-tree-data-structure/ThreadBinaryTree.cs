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
        private static int Id = 0;

        private static int NextId
        {
            get
            {
                return Id++;
            }
        }

        public T Data { set; get; }
        public int? NodeId { set; get; } = null;
        public ThreadBinaryTree<T> Llink { set; get; }
        public ThreadBinaryTree<T> Rlink { set; get; }
        public bool LBit { set; get; }
        public bool RBit { set; get; }

        // 建立ThreadBinaryTree時，固定建立一個head node
        public ThreadBinaryTree(out ThreadBinaryTree<T> headNode)
        {
            headNode = new ThreadBinaryTree<T>()
            {
                Data = default,
                LBit = true,
                RBit = true,
                Llink = this
            };
            headNode.Rlink = headNode;
            NodeId = NextId;
        }

        public ThreadBinaryTree()
        {
            NodeId = NextId;
        }


        public static ThreadBinaryTree<T> ConvertToThreadBinTree(BinaryTreeNode<T> tree)
        {
            var stack = new Stack<BinaryTreeNode<T>>();
            stack.Push(tree);
            var ThreadBinaryTree = new ThreadBinaryTree<T>(out var headNode);
            var cloneTreeStack = new Stack<ThreadBinaryTree<T>>();
            ThreadBinaryTree.Data = tree.Data;
            cloneTreeStack.Push(ThreadBinaryTree);
            while (stack.Count > 0)
            {
                var popup = stack.Pop();
                var clonePopup = cloneTreeStack.Pop();

                clonePopup.LBit = false;
                clonePopup.RBit = false;
                if (popup.Llink != null)
                {
                    stack.Push(popup.Llink);
                    clonePopup.LBit = true;
                    clonePopup.Llink = new ThreadBinaryTree<T>() { Data = popup.Llink.Data };
                    cloneTreeStack.Push(clonePopup.Llink);
                }

                if (popup.Rlink != null)
                {
                    stack.Push(popup.Rlink);
                    clonePopup.RBit = true;
                    clonePopup.Rlink = new ThreadBinaryTree<T>() { Data = popup.Rlink.Data };
                    cloneTreeStack.Push(clonePopup.Rlink);
                }

            }

            var inorderTreeList = new List<ThreadBinaryTree<T>>();
            GetInOrdertList(ThreadBinaryTree, inorderTreeList);
            BindingThreadNodes(headNode, ThreadBinaryTree, inorderTreeList);

            return ThreadBinaryTree;
        }

        private static void BindingThreadNodes(ThreadBinaryTree<T> headNode,
            ThreadBinaryTree<T> threadBinaryTree,
            List<ThreadBinaryTree<T>> inorderTreeList)
        {
            var stack = new Stack<ThreadBinaryTree<T>>();
            stack.Push(threadBinaryTree);
            while (stack.Count > 0)
            {
                var popup = stack.Pop();
                if (popup.Llink != null)
                {
                    stack.Push(popup.Llink);
                }
                else
                {
                    BindingToNeighborNode(popup, headNode, inorderTreeList, isRLink: false);
                }

                if (popup.Rlink != null)
                {
                    stack.Push(popup.Rlink);
                }
                else
                {
                    BindingToNeighborNode(popup, headNode, inorderTreeList, isRLink: true);
                }
            }
        }

       

        private static void GetInOrdertList(ThreadBinaryTree<T> tree, List<ThreadBinaryTree<T>> result)
        {
            if (tree != null)
            {
                GetInOrdertList(tree.Llink, result);
                result.Add(tree);
                GetInOrdertList(tree.Rlink, result);
            }
        }

        private static void BindingToNeighborNode(ThreadBinaryTree<T> node,
            ThreadBinaryTree<T> headNode,
            List<ThreadBinaryTree<T>> inOrderNodes,
            bool isRLink)
        {
            var matchedNode = inOrderNodes.Find(o => o.NodeId == node.NodeId);
            var isRightNode = (isRLink) ? 1 : -1;
            var neighborIndex = inOrderNodes.IndexOf(matchedNode) + isRightNode;
            ThreadBinaryTree<T> reAssignNode;
            if (neighborIndex < inOrderNodes.Count && neighborIndex > 0)
            {
                var neighborNode = inOrderNodes[neighborIndex];
                reAssignNode = neighborNode;
            }
            else
            {
                reAssignNode = headNode;
            }

            if (isRLink)
            {
                node.Rlink = reAssignNode;
                return;
            }
            node.Llink = reAssignNode;
        }
    }
}
