using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Alg4Exercise.Ch2.MaxPQ
{
    /// <summary>
    /// use stack(base on binary tree) to implement priority queue
    /// </summary>
    public class StackPQ<T> : IMaxPQ<T> where T: IComparable
    {
        private T[] _datas;
        public StackPQ(IEnumerable<T> datas)
        {
            _datas = new T[datas.Count() + 1];
            var i = 1;
            foreach (var item in datas)
            {
                _datas[i] = item;
                i++;
            }
        }
        public T DeleteMax()
        {
            // first element is the largest in binary tree
            var max = _datas[1];
            var size = _datas.Count();
            exch(1, size--);
            _datas[size + 1] = default;
            // recovert the order of binary tree
            Sink(1);
            return max;
        }

        public void Insert(T data)
        {
            // append element at least 
            _datas.Append(data);

            // then use swim to reorder the tree
            Swim(_datas.Length);
        }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        public T Max()
        {
            throw new NotImplementedException();
        }

        public void MaxPQ(int count)
        {
            _datas = new T[count];
        }

        public void MaxPQ(IEnumerable<T> collection)
        {
            _datas = collection.ToArray();
        }

        public int Size()
        {
            throw new NotImplementedException();
            var result = new List<T>();
        }

        /// <summary>
        /// change element with given two index
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void Exch(int i,int j) 
        {
            T key = _datas[i];
            _datas[i] = _datas[j];
            _datas[j] = key;
        }

        /// <summary>
        /// 當某個節點比parentNode大時，會需要進行交換的動作
        /// </summary>
        /// <param name="i"></param>
        private void Swim(int i)
        {
            // 因為資料結構是以binary tree的方式處理，所以節點父節點在陣列的索引值必為其節點本身的1/2
            while (i > 1 && _datas[i].CompareTo(_datas[i/2]) > 0)
            {
                Exch(i, i / 2);
                i = i / 2;
            }
            return;
        }
        
        /// <summary>
        /// 當某個節點比其子節點還小的，會需要將此節點下放
        /// </summary>
        /// <param name="i"></param>
        private void Sink(int i) 
        {
            var size = Size();
            // 當此節點索引值為N時，下沉時兩個子節點索引值必為2N,2N+1
            // 因此會以2*i <= size的判斷方式確認該節點是否還有子節點需要做判斷
            while (2*i <= size)
            {
                int j = 2 * i;
                // 判斷下沉時要往左邊還是右邊進行下沉
                // 因為若要以binary heap實現priority queue，且binary heap以array儲存時
                // 會需要確保父節點比兩個子節點都大
                // 因此這一段是要取得兩個子節點中值最大的索引
                if (j < size && _datas[i].CompareTo(_datas[i+1])<0) 
                {
                    j++;
                }
                if (_datas[i].CompareTo(_datas[j]) >= 0) break;
                Exch(i, j);
                i = j;
            }
        }

        private void exch(int i,int j)
        {
            var temp = _datas[i];
            _datas[i] = _datas[j];
            _datas[j] = temp;
        }
    }
}
