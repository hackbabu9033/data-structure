using System;
using System.Collections.Generic;
using System.Text;

namespace Ch6_tree_data_structure
{
    public class BinaryTreeNode<T> where T:IComparable<T>
    {
        private static int Id = 0;

        private static int NextId
        {
            get
            {
                return Id++;
            }
        }
        public BinaryTreeNode()
        {
            NodeId = NextId;
        }

        public int NodeId { get; set; }
        public T Data { get; set; }

        /// <summary>
        /// left node
        /// </summary>
        public BinaryTreeNode<T> Llink { get; set; }

        /// <summary>
        /// right node
        /// </summary>
        public BinaryTreeNode<T> Rlink { get; set; }

        /// <summary>
        /// travel node from cur Node
        /// </summary>
        public static void PreOrderTravel(BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                Console.WriteLine($"{node.Data}");
                PreOrderTravel(node.Llink);
                PreOrderTravel(node.Rlink);
            }
        }

        public static void InOrderTravel(BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                InOrderTravel(node.Llink);
                Console.WriteLine($"node value : {node.Data}, nodeId : {node.NodeId}");
                InOrderTravel(node.Rlink);
            }
        }

        /// <summary>
        /// 這裡的不使用遞迴本質上只是把Recursive拆成兩個判斷去跑infi loop，並不是真的沒有用到遞迴
        /// 因為stack本身的概念(last in first out)跟遞迴是一樣的
        /// </summary>
        /// <param name="node"></param>
        public static void InOrderTravelNoRecursive(BinaryTreeNode<T> node)
        {
            var stack = new Stack<BinaryTreeNode<T>>();
            var curDepth = -1;
            while (1==1)
            {
                // when left node is not null
                // keep push it into stack
                while (node != null)
                {
                    stack.Push(node);
                    node = node.Llink;
                    curDepth++;
                }

                // when popuped node has right node
                // push it into stack
                if (curDepth >= 0)
                {
                    var popup = stack.Pop();
                    Console.WriteLine($"{popup.Data}");
                    node = popup.Rlink;
                    curDepth--;
                }
                else
                {
                    return;
                }
            }
        }

        public static void PostOrderTravel(BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                PostOrderTravel(node.Llink);
                PostOrderTravel(node.Rlink);
                Console.WriteLine($"{node.Data}");
            }
        }

        /// <summary>
        /// 這裡的不使用遞迴本質上只是把Recursive拆成兩個判斷去跑infi loop，並不是真的沒有用到遞迴
        /// 因為stack本身的概念(last in first out)跟遞迴是一樣的
        /// </summary>
        /// <param name="node"></param>
        public static void PostOrderTravelNoRecursive(BinaryTreeNode<T> node)
        {
            var stack = new Stack<BinaryTreeNode<T>>();
            var curDepth = -1;
            while (1 == 1)
            {
                // when left node is not null
                // keep push it into stack
                while (node != null)
                {
                    stack.Push(node);
                    node = node.Llink;
                    curDepth++;
                }

                // when popuped node has right node
                // push it into stack
                if (curDepth >= 0)
                {
                    var popup = stack.Pop();
                    node = popup.Rlink;
                    curDepth--;
                }
                else
                {
                    return;
                }
            }
        }

        public static void GetInOrdertList(BinaryTreeNode<T> tree, List<BinaryTreeNode<T>> result)
        {
            if (tree != null)
            {
                GetInOrdertList(tree.Llink, result);
                result.Add(tree);
                GetInOrdertList(tree.Rlink, result);
            }
        }
    }

}
