using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Alg4Exercise.Ch2
{
    public static class ExercisePart
    {
        /// <summary>
        /// ch2 練習題2.1.8
        /// </summary>
        public static void Exercise8_ProveBigOIsLinear()
        {
            var stopWatch = new Stopwatch();
            long costTime;
            List<long> costTimes = new List<long>();
            for (int i = 6; i <= 9 ; i++)
            {
                var array = ArrayTool.CreateTestArray((int)Math.Pow(10, i),1,3);
                stopWatch.Restart();
                stopWatch.Start();
                Array.Sort(array);
                stopWatch.Stop();
                costTime = stopWatch.ElapsedMilliseconds;
                costTimes.Add(costTime);
            }
            // 比例差不多在10左右 ->確實是O(N)
            for (int i = costTimes.Count-1; i >0 ; i--)
            {
                Console.WriteLine(costTimes[i] / costTimes[i - 1]);
            }
        }

        /// <summary>
        /// ch2 2.1.24
        /// </summary>
        public static void InsertSortWithSential(int[] array)
        {
            var length = array.Length;
            int smallestValue = array[0];
            int smallestIndex = 0;
            // 將最小元素放在最左邊
            for (int i = 1; i < array.Length; i++)
            {
                smallestIndex = array[i] < smallestValue ? i : smallestIndex;
                smallestValue = array[smallestIndex];
            }
            for (int i = 1; i < length; i++)
            {
                // 不需要判斷j > 0，因為起始元素必為最小值
                for (int j = i; array[j] < array[j - 1]; j--)
                {
                    SortTool.Swap(array, j, j - 1);
                }
            }
        }

        /// <summary>
        /// 2.2.11
        /// </summary>
        public static void MergeSortWithPerformanceUpdate(int[] array)
        {
            MergeSort(array,0,array.Length-1);
        }

        /// <summary>
        /// 2.1.11 針對三個地方優化效能
        /// </summary>
        /// <param name="array"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private static void MergeSort(int[] array,int left,int right)
        {
            // 1. 當陣列長度小時以insert sort做排序
            var length = right - left + 1;
            if (length <= 15)
            {
                InsertSort(array,left,right);
                return;
            }
            var middle = (left + right) / 2;
            MergeSort(array, left, middle);
            MergeSort(array, middle + 1, right);
            // 2. 當a[mid] <= a[mid+1]可以直接跳過將兩個陣列合併的動作
            if (array[middle] <= array[middle + 1])
            {
                return;
            }
            SortTool.MergeTwoArray(array, left, middle, right);
        }

        /// <summary>
        /// 針對index範圍內的進行插入排序
        /// </summary>
        /// <param name="array"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private static void InsertSort(int[] array, int left, int right)
        {
            for (int i = left; i <= right; i++)
            {
                var ithElement = array[i];
                var j = i;
                for (j = i; j > 0 && ithElement < array[j - 1]; j--)
                {
                    array[j] = array[j - 1];
                }
                array[j] = ithElement;
            }
        }

        /// <summary>
        /// 2.1.11 -> 3.不將陣列全部複製到輔助陣列中
        /// </summary>
        public static void MergeWithNoCopyStep(int[] array)
        {
            var length = array.Length;
            // 建立輔助陣列
            var aux = new int[length];
            SortWithNoCopy(array, aux, 0, length-1);
        }
        private static void SortWithNoCopy(int[] array, int[] aux, int left, int right)
        {
            if (right <= left)
            {
                aux[right] = aux[right] == 0 ? array[right] : aux[right];
                array[right] = array[right] == 0 ? aux[right] : array[right];
                return;
            }
            var mid = (right + left) / 2;
            // 將input array跟aux做對調
            //if (left == mid)
            //{
            //    aux[left] = array[left];
            //}
            //else
            //{
            //    SortWithNoCopy(aux, array, left, mid);
            //}

            //if (mid + 1 == right)
            //{
            //    aux[right] = array[right];
            //}
            //else
            //{
            //    SortWithNoCopy(aux, array, mid + 1, right);
            //}
            SortWithNoCopy(aux, array, left, mid);
            
            SortWithNoCopy(aux, array, mid + 1, right);
            MergeSortAux(array, aux, left, mid, right);
        }

        private static void MergeSortAux(int[] array, int[] aux, int left, int middle, int right)
        {
            var i = left;
            var j = middle + 1;
            for (int k = left; k <= right; k++)
            {
                if (i > middle)
                {
                    array[k] = aux[j++];
                }
                else if (j > right)
                {
                    array[k] = aux[i++];
                }
                else
                {
                    array[k] = aux[i] < aux[j] ? aux[i++] : aux[j++];
                }
            }
        }
    }
}
