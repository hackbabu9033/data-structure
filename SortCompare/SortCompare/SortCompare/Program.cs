using Alg4Exercise;
using Alg4Exercise.Ch2;
using Alg4Exercise.Ch2.MaxPQ;
using System;
using System.Diagnostics;

namespace SortCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1000; i <= 100000; i = i * 10)
            {
                var arraySize = i;
                var testArray = ExperimentExercise.CreateTestArray(arraySize);
                var testArray2 = (int[])testArray.Clone();
                // 比較merge sort的優化差異
                Console.WriteLine($"curLoop Batch array size:{arraySize}");
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                ExercisePart.MergeWithNoCopyStep(testArray);
                stopWatch.Stop();
                Console.WriteLine($"MergeSortBottomUp:{stopWatch.ElapsedMilliseconds}");
                stopWatch.Reset();
                stopWatch.Start();
                ExercisePart.MergeSortWithPerformanceUpdate(testArray2);
                stopWatch.Stop();
                Console.WriteLine($"MergeSortTopdown:{stopWatch.ElapsedMilliseconds}");
            }
            //var test = new int[] {50,18,13,5,17,13,19,21,1,11 };
            //ExercisePart.MergeWithNoCopyStep(test);

            #region MaxPQ usage example
            // 如果只需要取得資料中最大or最小的幾筆，可以使用
            // Priority queue 進行排序，可以在不需要所以資料排序完即可得到結果

            // ex:從10000000中取得前50筆最大的資料
            var datas = ExperimentExercise.CreateTestArray(1000);
            var intMaxPQ = new UnOrderPQ<int>();
            intMaxPQ.MaxPQ(50 + 1);
            foreach (var data in datas)
            {
                intMaxPQ.Insert(data);
                if (intMaxPQ.Size() > 50)
                {
                    intMaxPQ.DeleteMin();
                }
            }
            #endregion
        }

        private static void MultiWayInput(string[] inputs)
        {
            var N = inputs.Length;
            
        }
    }
}
