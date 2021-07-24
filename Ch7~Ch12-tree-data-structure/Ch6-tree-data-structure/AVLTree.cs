using Ch6_Ch7_tree_data_structure;
using Ch6_Ch7_tree_data_structure.Extension;
using Ch6_tree_data_structure.AVLTreeBalance;
using Ch6_tree_data_structure.Enum;
using Ch6_tree_data_structure.Extension;
using Ch6_tree_data_structure.Interface;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ch6_tree_data_structure
{
    public class AVLTree<T> where T: IComparable<T>
    {
        private static Dictionary<AVLTreeUnBalanceType, IAVLTreeBalancer<T>> _AVLTreeBalanceLogic =
            new Dictionary<AVLTreeUnBalanceType, IAVLTreeBalancer<T>>()
            {
                {AVLTreeUnBalanceType.LL,new LLTreeBalance<T>()},
                {AVLTreeUnBalanceType.RR,new RRTreeBalance<T>()},
                {AVLTreeUnBalanceType.LR,new LRTreeBalance<T>()},
                {AVLTreeUnBalanceType.RL,new RLTreeBalance<T>()}
            };

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
            AVLTree<T> reBalanceTree = null;
            //先以正常的tree加入方式加入節點
            this.AddTreeNode(data);

            // 加入完後若有不平衡狀況出現再重新平衡
            return GetReBalanceTree(this);
            //var unbalanceNode = GetDeepestUnbalanceNode(this);
            //if (unbalanceNode == null)
            //{
            //    return this;
            //}
            //var unbalanceType = GetUnbalanceTreeType(unbalanceNode.Node);
            //var root = unbalanceNode.Node;
            //if (!unbalanceType.HasValue)
            //{
            //    return this;
            //}
            //if (unbalanceType.HasValue)
            //{
            //    _AVLTreeBalanceLogic.TryGetValue(unbalanceType.Value, out IAVLTreeBalancer<T> balanceLogic);
            //    reBalanceTree = balanceLogic.BalanceALVTree(root);
            //}

            //// 如果取得最深非平衡節點的parent是null代表是整個樹有動
            //// 直接回傳平衡完的結果
            //if (unbalanceNode?.Parent == null)
            //{
            //    return reBalanceTree;
            //}
            //else
            //{
            //    // 若最深非平衡節點有parent
            //    // 找出哪一個節點的子節點是最深的非平衡節點
            //    // 將它的子節點用旋轉平衡完的樹reassign給它
            //    var queue = new Queue<AVLTree<T>>();
            //    queue.Enqueue(this);
            //    while (queue.Count > 0)
            //    {
            //        var node = queue.Dequeue();
            //        if (node.Left == unbalanceNode.Node)
            //        {
            //            node.Left = reBalanceTree;
            //        }
            //        if (node.Right == unbalanceNode.Node)
            //        {
            //            node.Right = reBalanceTree;
            //        }
            //        if (node.Left == null)
            //        {
            //            queue.Enqueue(node.Left);
            //        }
            //        if (node.Right == null)
            //        {
            //            queue.Enqueue(node.Right);
            //        }
            //    }
            //}

            //return this;
        }

        private AVLTree<T> GetReBalanceTree(AVLTree<T> tree)
        {
            AVLTree<T> reBalanceTree = null;

            // 加入完後若有不平衡狀況出現再重新平衡
            var unbalanceNode = GetDeepestUnbalanceNode(tree);
            if (unbalanceNode == null)
            {
                return tree;
            }
            var unbalanceType = GetUnbalanceTreeType(unbalanceNode.Node);
            var root = unbalanceNode.Node;
            if (!unbalanceType.HasValue)
            {
                return tree;
            }
            if (unbalanceType.HasValue)
            {
                _AVLTreeBalanceLogic.TryGetValue(unbalanceType.Value, out IAVLTreeBalancer<T> balanceLogic);
                reBalanceTree = balanceLogic.BalanceALVTree(root);
            }

            // 如果取得最深非平衡節點的parent是null代表是整個樹有動
            // 直接回傳平衡完的結果
            if (unbalanceNode?.Parent == null)
            {
                return reBalanceTree;
            }
            else
            {
                // 若最深非平衡節點有parent
                // 找出哪一個節點的子節點是最深的非平衡節點
                // 將它的子節點用旋轉平衡完的樹reassign給它
                var queue = new Queue<AVLTree<T>>();
                queue.Enqueue(tree);
                while (queue.Count > 0)
                {
                    var node = queue.Dequeue();
                    if (node.Left != null)
                    {
                        if (node.Left.Data.CompareTo(unbalanceNode.Node.Data) == 0)
                        {
                            node.Left = reBalanceTree;
                            break;
                        }
                        queue.Enqueue(node.Left);
                    }
                    if (node.Right != null)
                    {
                        if (node.Right.Data.CompareTo(unbalanceNode.Node.Data) == 0)
                        {
                            node.Right = reBalanceTree;
                            break;
                        }
                        queue.Enqueue(node.Right);
                    }
                }
            }

            return tree;
        }

        public AVLTree<T> Delete(T data)
        {
            this.DeleteTreeNode(data);
            return GetReBalanceTree(this);
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

    public class RebalanceTreeResult<T> where T: IComparable<T>
    {
        /// <summary>
        /// 被平衡的root
        /// </summary>
        public AVLTree<T> BalancedRootNode { get; set; }
        public AVLTree<T> ReBalancedTree { get; set; }
    }
}
