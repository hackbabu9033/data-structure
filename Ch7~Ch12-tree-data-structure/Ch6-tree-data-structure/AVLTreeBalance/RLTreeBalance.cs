using Ch6_tree_data_structure.Extension;
using Ch6_tree_data_structure.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ch6_tree_data_structure.AVLTreeBalance
{
    public class RLTreeBalance<T> : IAVLTreeBalancer<T> where T : IComparable<T>
    {
        public AVLTree<T> BalanceALVTree(AVLTree<T> root)
        {
            var parent = root.Right;
            var rotatedTree = parent.RightRotation(parent.Left);
            root.Right = rotatedTree;
            return root.LeftRotation(rotatedTree);
        }
    }
}
