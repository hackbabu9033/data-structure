using System;
using System.Collections.Generic;
using System.Text;

namespace Ch6_tree_data_structure.Extension
{
    public static class AVLTreeExtension
    {
        public static AVLTree<T> RightRotation<T>(this AVLTree<T> parent, AVLTree<T> pivot) where T:IComparable<T>
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

        public static AVLTree<T> LeftRotation<T>(this AVLTree<T> parent, AVLTree<T> pivot) where T : IComparable<T>
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

        // find the inorder successor for given node tree
        public static InorderSuccessorTrackContent<T> GetInOrderSuccessor<T>(this AVLTree<T> root) where T: IComparable<T>
        {
            var successorParenet = root;
            var successor = root.Right;
            // find the most left node in subtree
            while (successor.Left != null)
            {
                successorParenet = successor;
                successor = successor.Left;
            }
            return new InorderSuccessorTrackContent<T>() 
            { 
                Successor = successor,
                SuccessorParenet = successorParenet
            };
        }

        public static void AddTreeNode<T>(this AVLTree<T> avlTree, T data) where T : IComparable<T>
        {
            if (avlTree == null)
            {
                avlTree.Data = data;
                return;
            }
            var curNode = avlTree;
            AVLTree<T> preNode = null;
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
                    curNode = curNode.Left;
                }
                else
                {
                    curNode = curNode.Right;
                }
            }

            var newAddNode = new AVLTree<T>()
            {
                Data = data
            };
            if (data.CompareTo(preNode.Data) > 0)
            {
                preNode.Right = newAddNode;
            }
            else
            {
                preNode.Left = newAddNode;
            }
        }

        /// <summary>
        /// delete tree node
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static AVLTree<T> DeleteTreeNode<T>(this AVLTree<T> root, T data) where T : IComparable<T>
        {
            if (root == null)
            {
                return root;
            }

            var compareResult = data.CompareTo(root.Data);
            if (compareResult < 0)
            {
                root.Left = DeleteTreeNode(root.Left, data);
                return root;
            }
            else if (compareResult > 0)
            {
                root.Right = DeleteTreeNode(root.Right, data);
                return root;
            }

            if (root.Left == null)
            {
                return root.Right;
            }
            else if (root.Right == null)
            {
                return root.Left;
            }
            else
            {
                // deleted node has both left and right child node
                // -> find inorder successor
                var inOrderTrackResult = GetInOrderSuccessor(root);

                // replace root use inorder successor data
                //inOrderTrackResult.SuccessorParenet.Left = inOrderTrackResult.Successor.Left;
                root.Data = inOrderTrackResult.Successor.Data;

                // todo:這裡要消化一下
                // since the inorder successor of delete node is
                // the smallest node in the right sub tree
                root.Right = DeleteTreeNode(root.Right, root.Data);
            }
            return root;
        }
    }

    public class InorderSuccessorTrackContent<T> where T:IComparable<T>
    {
        public AVLTree<T> SuccessorParenet { get; set; }
        public AVLTree<T> Successor { get; set; }
    }
}
