using Ch6_tree_data_structure.Extension;
using Ch6_tree_data_structure.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ch6_tree_data_structure.AVLTreeBalance
{
    public class LRTreeBalance<T> : IAVLTreeBalancer<T> where T : IComparable<T>
    {
        public AVLTree<T> BalanceALVTree(AVLTree<T> root)
        {
            var parent = root.Left;
            var pivot = parent.Right;
            var rotatedTree = parent.LeftRotation(pivot);
            root.Left = rotatedTree;
            return root.RightRotation(rotatedTree);
        }
    }
}
