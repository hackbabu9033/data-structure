using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Alg4Exercise.Ch2.MaxPQ
{
    /// <summary>
    /// 以未排序過的集合實作priority queue
    /// </summary>
    public class UnOrderPQ<T> : IMaxPQ<T> where T : IComparable
    {
        private List<T> _datas { get; set; }

        
        public T DeleteMax()
        {
            var maxIndex = GetMaxIndex();
            T max = _datas[maxIndex];
            _datas.RemoveAt(maxIndex);
            return max;
        }

        public T DeleteMin()
        {
            var minIndex = GetMinIndex();
            T min = _datas[minIndex];
            _datas.RemoveAt(minIndex);
            return min;
        }

        public void Insert(T data)
        {
            _datas.Add(data);
        }

        public bool IsEmpty()
        {
            return _datas == null || _datas.Count == 0;
        }

        public void MaxPQ()
        {
            _datas = new List<T>();
        }

        public void MaxPQ(int count)
        {
            _datas = new T[count].ToList();
        }

        public void MaxPQ(IEnumerable<T> collection)
        {
            _datas = collection.ToList();
        }

        public int Size()
        {
            return _datas == null ? 0 : _datas.Count();
        }

        public T Max()
        {
            int maxIndex = GetMaxIndex();
            return _datas[maxIndex];
        }

        private int GetMinIndex()
        {
            int minIndex = 0;
            for (int i = 1; i < _datas.Count; i++)
            {
                minIndex = _datas[minIndex].CompareTo(_datas[i]) < 0 ? minIndex : i;
            }
            return minIndex;
        }

        private int GetMaxIndex()
        {
            int maxIndex = 0;
            for (int i = 1; i < _datas.Count; i++)
            {
                maxIndex = _datas[maxIndex].CompareTo(_datas[i]) > 0 ? maxIndex : i;
            }
            return maxIndex;
        }
    }
}
