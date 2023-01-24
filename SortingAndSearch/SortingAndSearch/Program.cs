using System;
using System.Collections.Generic;

namespace SortingAndSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> test = new List<int>() { 1, 14, 63, 88, 100, 145, 170 };
            List<int> result = new List<int>();
            result.Add(Search.FindInsertIndexForSortedList(test,0));
            result.Add(Search.FindInsertIndexForSortedList(test,13));
            result.Add(Search.FindInsertIndexForSortedList(test,20));
            result.Add(Search.FindInsertIndexForSortedList(test,70));
            result.Add(Search.FindInsertIndexForSortedList(test,99));
            result.Add(Search.FindInsertIndexForSortedList(test,120));
            result.Add(Search.FindInsertIndexForSortedList(test,150));
            result.Add(Search.FindInsertIndexForSortedList(test,1111));
        }
    }
}
