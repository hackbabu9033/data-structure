using Ch6_tree_data_structure.Extension;
using Ch6_tree_data_structure.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ch6_tree_data_structure.AVLTreeBalance
{
    public class RRTreeBalance<T> : IAVLTreeBalancer<T> where T : IComparable<T>
    {
        public AVLTree<T> BalanceALVTree(AVLTree<T> root)
        {
            return root.LeftRotation(root.Right);
        }
    }
}
