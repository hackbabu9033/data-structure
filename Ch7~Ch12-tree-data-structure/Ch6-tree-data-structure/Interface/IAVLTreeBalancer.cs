using System;
using System.Collections.Generic;
using System.Text;

namespace Ch6_tree_data_structure.Interface
{
    public interface IAVLTreeBalancer<T> where T:IComparable<T>
    {
        public AVLTree<T> BalanceALVTree(AVLTree<T> root);
    }
}
