using System;
using System.Collections.Generic;
using System.Text;

namespace Alg4Exercise
{
    public static class SortTool
    {
        public static void BubbleSort(int[] array)
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

        public static void InsertSort(int[] array)
        {
            var length = array.Length;
            int curIndex;
            for (int i = 1; i < length; i++)
            {

                //for (int j = i; j > 0 && array[j] < array[j - 1]; j--)
                //{
                //    Swap(array, j, j - 1);
                //}

                // exercise 2.1.25 -> Insertion sort without exchanges
                var ithElement = array[i];
                var j = i;
                for (j = i; j > 0 && ithElement < array[j - 1]; j--)
                {
                    array[j] = array[j - 1];
                }
                array[j] = ithElement;      
            }
        }

        public static void ShellSort(int[] array)
        {
            var lenght = array.Length;
            var h = 1;
            while (h < lenght / 3)
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

        public static void Swap(int[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        /// <summary>
        /// 自下而上的merge sort
        /// 從1開始22合併 -> 44合併 ...
        /// </summary>
        /// <param name="array"></param>
        public static void MergeSortBottomUp(int[] array)
        {
            var curBatchSize = 1;
            var length = array.Length;
            int rightBorder;
            int middle;
            var count = 0;
            for (int i = curBatchSize; i < length ; i+=i)
            {
                for (int j = 0; j < length - i; j+= (i*2))
                {
                    rightBorder = Math.Min(j+(i*2)-1,length-1);
                    middle = j + i - 1;
                    MergeTwoArray(array, j, middle, rightBorder);
                    count++;
                }
            }
        }

        /// <summary>
        /// 由上而下的merge sort
        /// 每次都recursive把左右排序
        /// </summary>
        /// <param name="array"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void MergeSortTopdown(int[] array,int left,int right)
        {
            if (right <= left)
            {
                return;
            }
            var middle = (left + right) / 2;
            MergeSortTopdown(array, left, middle);
            MergeSortTopdown(array, middle + 1, right);
            MergeTwoArray(array, left, middle, right);
        }

      
        /// <summary>
        /// 快速排序法
        /// </summary>
        /// <param name="array"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void QuickSort(int[] array)
        {
            QuickSort(array);
        }

        public static void QuickSort(int[] array,int left,int right)
        {
            var diviedIndex = Partition(array, 0, array.Length - 1);
            QuickSort(array, 0, diviedIndex - 1);
            QuickSort(array, diviedIndex + 1, array.Length - 1);
        }

        /// <summary>
        /// 切分陣列
        /// </summary>
        /// <param name="array"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private static int Partition(int[] array,int left,int right)
        {
            var i = left;
            var j = right;
            var divideElement = array[i];
            while (true)
            {
                while(array[++i] < divideElement)
                {
                    if (i >= right)
                    {
                        break;
                    }
                }
                while (array[--j] > divideElement)
                {
                    if (j <= left)
                    {
                        break;
                    }
                }

                if (right <= left)
                {
                    break;
                }

                Swap(array, i, j);
            }
            return j;
        }

        /// <summary>
        /// merge sort使用的遞迴function
        /// </summary>
        /// <param name="array"></param>
        /// <param name="left"></param>
        /// <param name="middle"></param>
        /// <param name="right"></param>
        public static void MergeTwoArray(int[] array,int left,int middle,int right)
        {
            var length = array.Length;
            var i = left;
            var j = middle + 1;
            var aux = new int[length];

           
            for (int s = 0; s < length; s++)
            {
                aux[s] = array[s];
            }
            for (int k = left; k <= right; k++)
            {
                if (j > right)
                {
                    array[k] = aux[i++];
                }
                else if (i > middle)
                {
                    array[k] = aux[j++];
                }
                else if (aux[i] < aux[j])
                {
                    array[k] = aux[i++];
                }
                else
                {
                    array[k] = aux[j++];
                }
            }
        }
    }
}
