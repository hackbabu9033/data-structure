using System;
using System.Collections.Generic;

namespace Ch6_tree_data_structure
{
    public class _23Tree<T> where T:IComparable<T>
    {
        public T LeftData { get; set; }
        public T RightData { get; set; }

        public _23Tree<T> Right { get; set; }

        public _23Tree<T> Left { get; set; }

        public _23Tree<T> Middle { get; set; }

        public _23Tree<T> Add(Stack<_23Tree<T>> stackTree,T data)
        {
            if (stackTree.Count <= 0)
            {
                
            }
            var tree = stackTree.Pop();
            if (tree.LeftData == null && tree.RightData == null)
            {
                return new _23Tree<T>()
                {
                    LeftData = data
                };
            }

            var compareRightResult = data.CompareTo(tree.LeftData);

            if (tree.RightData == null)
            {
                if (compareRightResult == 0)
                {
                    return this;
                }
                if (compareRightResult > 0)
                {
                    tree.RightData = data;
                }
                return this;
            }
            else
            {
                // todo:確認
                var compareLeftResult = data.CompareTo(tree.RightData);
            }

            return null;
        }

    }

    
}
