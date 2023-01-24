using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace SortCompare
{
    public static class ExperimentExercise
    {
        private static Random _rnd = new Random();
        public static void SortAlgorithmCompare()
        {
            var stopWatch = new Stopwatch();
            for (int i = 15; i < 18; i++)
            {
                var length = (int)Math.Pow(2, i);
                var testArray = CreateTestArray(length);
                // make array become DESC
                Array.Sort(testArray);
                Array.Reverse(testArray);
                var testArray2 = new int[testArray.Length];
                var testArray3 = new int[testArray.Length];
                var testArray4 = new int[testArray.Length];
                testArray.CopyTo(testArray2, 0);
                testArray.CopyTo(testArray3, 0);
                testArray.CopyTo(testArray4, 0);

                // test bubble sort time
                stopWatch.Start();
                BubbleSort(testArray);
                stopWatch.Stop();
                Console.WriteLine($"bubble sort time cost：{stopWatch.ElapsedMilliseconds}");

                // test insert sort Time
                stopWatch.Reset();
                stopWatch.Start();
                InsertSort(testArray2);
                stopWatch.Stop();
                Console.WriteLine($"insert sort time cost：{stopWatch.ElapsedMilliseconds}");


                // test Shell sort time
                stopWatch.Reset();
                stopWatch.Start();
                ShellSort(testArray3);
                stopWatch.Stop();
                Console.WriteLine($"Shell sort time cost：{stopWatch.ElapsedMilliseconds}");

                // test .net sort
                stopWatch.Reset();
                stopWatch.Start();
                Array.Sort(testArray4);
                stopWatch.Stop();
                Console.WriteLine($"Shell array sort time cost：{stopWatch.ElapsedMilliseconds}");
            }

        }

        public static int[] CreateTestArray(int length)
        {
            var testArray = new int[length];
            
            for (int i = 0; i < length; i++)
            {
                testArray[i] = _rnd.Next(1,1000000000);
            }
            return testArray;
        }

        private static void BubbleSort(int[] array)
        {
            var length = array.Length;
            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (array[i] > array[j])
                    {
                        Swap(array, i, j);
                    }
                }
            }
        }

        private static void InsertSort(int[] array)
        {
            var length = array.Length;
            for (int i = 1; i < length; i++)
            {
                var j = i;
                while (j > 0 && array[i] < array[j-1] )
                {
                    j--;
                }
                for (int a = i; a > j; a--)
                {
                    Swap(array, a, a-1);
                }
            }
        }


        private static void ShellSort(int[] array)
        {
            var lenght = array.Length;
            var h = 1;
            while (h < lenght/3)
            {
                h = 3 * h + 1;
            }
            while (h >= 1)
            {
                for (int i = h; i < lenght; i++)
                {
                    for (int j = i; j >= h && array[j] < array[j - h]; j -= h)
                    {
                        Swap(array, j, j - h);
                    }
                }
                h = h / 3;
            }
        }

        private static void Swap(int[] array,int i,int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

       
    }
}
