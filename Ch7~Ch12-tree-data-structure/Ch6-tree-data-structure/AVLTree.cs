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
            AVLTree<T> reBalanceTree = null;
            //先以正常的tree加入方式加入節點
            this.AddTreeNode(data);

            // 加入完後若有不平衡狀況出現再重新平衡
            var unbalanceNode = GetDeepestUnbalanceNode(this);
            if (unbalanceNode == null)
            {
                return this;
            }
            var unbalanceType = GetUnbalanceTreeType(unbalanceNode.Node);
            var root = unbalanceNode.Node;
            if (!unbalanceType.HasValue)
            {
                return this;
            }
            switch (unbalanceType)
            {
                // note：因為無論哪種情況的rotation都會導致根節點改變
                // 因此回傳新的樹
                case AVLTreeUnBalanceType.LL:
                    pivot = root.Left;
                    reBalanceTree = RightRotation(root, pivot);
                    break;
                case AVLTreeUnBalanceType.LR:
                    parent = root.Left;
                    pivot = parent.Right;
                    rotatedTree = LeftRotation(root, pivot);
                    root.Left = rotatedTree;
                    reBalanceTree = RightRotation(root, rotatedTree);
                    break;
                case AVLTreeUnBalanceType.RR:
                    pivot = root.Right;
                    reBalanceTree = LeftRotation(root, pivot);
                    break;
                case AVLTreeUnBalanceType.RL:
                    parent = root.Right;
                    pivot = parent.Left;
                    rotatedTree = RightRotation(parent, pivot);
                    reBalanceTree = LeftRotation(rotatedTree, rotatedTree.Right);
                    break;
            }

            // 如果取得最深非平衡節點的parent是null代表是整個樹有動
            // 直接回傳平衡完的結果
            if (unbalanceNode.Parent == null)
            {
                return reBalanceTree;
            }

            // 若最深非平衡節點有parent
            // 找出哪一個節點的子節點是最深的非平衡節點
            // 將它的子節點用旋轉平衡完的樹reassign給它
            if (unbalanceNode.Parent == root.Left)
            {
                var queue = new Queue<AVLTree<T>>();
                queue.Enqueue(this);
                while (queue.Count > 0)
                {
                    var node = queue.Dequeue();
                    if (node.Left == unbalanceNode.Node)
                    {
                        node.Left = reBalanceTree;
                    }
                    if (node.Right == unbalanceNode.Node)
                    {
                        node.Right = reBalanceTree;
                    }
                    if (node.Left == null)
                    {
                        queue.Enqueue(node.Left);
                    }
                    if (node.Right == null)
                    {
                        queue.Enqueue(node.Right);
                    }
                }
            }
            return this;
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

        public TreeNodeTrack<T> GetDeepestUnbalanceNode(AVLTree<T> root)
        {
            AVLTree<T> deepestUnbalanceNode = null;
            AVLTree<T> parent = null;
            //var queue = new Queue<AVLTree<T>>();
            var queue = new Queue<TreeNodeTrack<T>>();
            //queue.Enqueue(root);
            queue.Enqueue(new TreeNodeTrack<T>() { Parent = null, Node = root });
            // 用廣度優先，深度由上往下搜尋
            // 確保找到的平衡因子會是比最深的那個
            while (queue.Count > 0)
            {
                var track = queue.Dequeue();
                var nodeBalanceFactor = CaculateNodebalanceFactor(track.Node);
                if (Math.Abs(nodeBalanceFactor) > 1)
                {
                    deepestUnbalanceNode = track.Node;
                    parent = track.Parent;
                }
                if (track.Node.Left != null)
                {
                    queue.Enqueue(new TreeNodeTrack<T>() { Parent = track.Node,Node = track.Node.Left });
                }
                if (track.Node.Right != null)
                {
                    queue.Enqueue(new TreeNodeTrack<T>() { Parent = track.Node, Node = track.Node.Right });
                }
            }

            if (deepestUnbalanceNode == null)
            {
                return null;
            }

            return new TreeNodeTrack<T>
            {
                Parent = parent,
                Node = deepestUnbalanceNode
            };

        }

        /// <summary>
        /// 計算該節點的balance refactor
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public AVLTreeUnBalanceType? GetUnbalanceTreeType(AVLTree<T> node)
        {
            if (node == null) return null;

            var rootBalanceFactor = CaculateNodebalanceFactor(node);
            var leftNodeBalanceFactor = CaculateNodebalanceFactor(node.Left);
            var rightNodeBalanceFactor = CaculateNodebalanceFactor(node.Right);

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

    public class TreeNodeTrack<T> where T : IComparable<T>
    {
        public AVLTree<T> Node { get; set; }
        public AVLTree<T> Parent { get; set; }

    }
}
