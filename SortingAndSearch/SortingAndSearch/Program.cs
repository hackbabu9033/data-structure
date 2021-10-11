using System;
using System.Collections.Generic;

namespace SortingAndSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> test = new List<int>() { 1, 14, 63, 88, 100, 145 };
            List<int> result = new List<int>();
            result.Add(Search.BinarySearchIndex(test,1));
            result.Add(Search.BinarySearchIndex(test,14));
            result.Add(Search.BinarySearchIndex(test,63));
            result.Add(Search.BinarySearchIndex(test,88));
            result.Add(Search.BinarySearchIndex(test,100));
            result.Add(Search.BinarySearchIndex(test,145));
            result.Add(Search.BinarySearchIndex(test,1111));
            
        }
    }
}
