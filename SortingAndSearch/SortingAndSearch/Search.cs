using System;
using System.Collections.Generic;
using System.Text;

namespace SortingAndSearch
{
    public static class Search
    {
        public static int BinarySearchIndex(List<int> sortedList,int search)
        {
            int left,right,middle,value;
            left = 0;
            middle = 0;
            right = sortedList.Count - 1;
            while (right >= left)
            {
                middle = (left + right) / 2;
                value = sortedList[middle];
                if (search == value)
                {
                    return middle;
                }
                else if (search > value)
                {
                    left = middle + 1;
                }
                else
                {
                    right = middle - 1;
                }
            }
            return sortedList[middle] == search ? middle : -1;
        }
    }
}
