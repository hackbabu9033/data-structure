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
    public class ThreadBinaryTree<T> where T:IComparable<T>
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

        public static void InsertLeftNode(ThreadBinaryTree<T> nodeParent, ThreadBinaryTree<T> insertNode)
        {
            insertNode.LBit = nodeParent.LBit;
            insertNode.Llink = nodeParent.Llink;
            insertNode.RBit = false;
            insertNode.Rlink = nodeParent;

            nodeParent.LBit = true;
            nodeParent.Llink = insertNode;
            // 若是非引線的reference，
            if (insertNode.LBit)
            {

            }
        }

        public static ThreadBinaryTree<T> InorderSuccessor(ThreadBinaryTree<T> node)
        {
            ThreadBinaryTree<T> cur;
            cur = node.Rlink;
            // 第一種情況->node右節點為引線
            // 代表引線連到的點是中序排序下的下一個節點
            if (node.RBit)
            {
                //      5             5
                //     /             /
                //    6<-node       6<-node
                //     \            \
                //      9            9
                //     / \            \
                //    11  10           10
                //  6,11,9,10,5     6,9,10,5
                // 因為中序排序是左->中->右
                // 所以對node 6的右下節點樹而言，只有後面子樹的左節點會影響結果
                // 因此進了node.RBit之後，需要一直判斷當下的子節點是否有左節點(判斷9有沒有左節點)
                // 只有當最左節點沒有節點時，才會是在6的下一個
                while (cur.LBit)
                {
                    cur = cur.Llink;
                }
            }
            return cur;
        }

        public static ThreadBinaryTree<T> InorderPrecessor(ThreadBinaryTree<T> node)
        {
            ThreadBinaryTree<T> cur;
            cur = node.Llink;
            // 若Lbit為引線則直接取左節點
            if (node.LBit)
            {
                //      5             5
                //     /             /
                //    6<-node      6<-node
                //     \            \
                //      9            9
                //     /            /  \
                //    11           11   10
                //  6,11,9,5     6,10,9,10,5
                // 因為是左->中->右的順序，因此對一個節點而言，
                // 前一個必為左子樹中最右的節點(這樣該節點的引線才會連到目前節點)
                while (cur.RBit)
                {
                    cur = cur.Rlink;
                }
            }

            return cur;
        }

        public static ThreadBinaryTree<T> DeleteNodeFromTree(ThreadBinaryTree<T> root,ThreadBinaryTree<T> par,ThreadBinaryTree<T> node)
        {
            if (!node.LBit && !node.RBit)
            {
                return DeleteLeafNode(root, par, node);
            }
            else if (node.LBit && node.RBit)
            {
                return DeleteNodeHasTwoChild(root, node);
            }
            return DeleteSingleSibingNode(root, par, node);
        }

        private static ThreadBinaryTree<T> DeleteNodeHasTwoChild(ThreadBinaryTree<T> root, ThreadBinaryTree<T> node)
        {
            var parSucc = node;
            var succ = node.Rlink;
            // 找出node的inorder successor
            if (succ.Llink != null)
            {
                parSucc = succ;
                succ = succ.Llink;
            }
            // 將inorder successor代替node
            node.Data = succ.Data;

            // 刪除原本的inorder successor
            if (!succ.LBit && !succ.RBit)
            {
                DeleteLeafNode(root, parSucc, succ);
            }
            else
            {
                DeleteSingleSibingNode(root, parSucc, succ);
            }
            return root;
        }

        // todo：思考一般情境下回傳甚麼node比較好
        private static ThreadBinaryTree<T> DeleteLeafNode(ThreadBinaryTree<T> root, ThreadBinaryTree<T> par, ThreadBinaryTree<T> node)
        {
            // par null代表是head Node(開頭節點)
            if (par == null)
                root = null;
            if (par.Llink == node)
            {
                par.LBit = false;
                par.Llink = node.Llink;
            }
            if (par.Rlink == node)
            {
                par.RBit = false;
                par.Rlink = node.Rlink;
            }
            return root;
        }

        // todo：思考一般情境下回傳甚麼node比較好
        private static ThreadBinaryTree<T> DeleteSingleSibingNode(ThreadBinaryTree<T> root,
            ThreadBinaryTree<T> par,
            ThreadBinaryTree<T> node)
        {
            if (par == null)
            {
                root = null;
            }

            ThreadBinaryTree<T> child;
            if (node.RBit)
            {
                child = node.Rlink;
            }
            else
            {
                child = node.Llink;
            }

            if (par.Llink == node)
            {
                par.Llink = child;
            }
            if (par.Rlink == node)
            {
                par.Rlink = child;
            }

            var insucc = InorderSuccessor(node);
            var inpresucc = InorderPrecessor(node);

            if (node.LBit)
            {
                inpresucc.Rlink = insucc;
            }
            else if (node.RBit)
            {
                insucc.Llink = inpresucc;
            }

            return root;
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
