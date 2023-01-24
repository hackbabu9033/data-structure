using System;
using System.Collections.Generic;
using System.Text;

namespace Alg4Exercise.Ch2.MinPQ
{
    public class IndexMinPQ<T> : IIndexMinPQ<T> where T : IComparable
    {

        private int N;
        private int[] pq;// 紀錄索引在樹的順序(每個節點存放的是keys的索引)
        private int[] qp;// 紀錄索引在樹的位置
        private T[] Keys;
        private int Max;

        public IndexMinPQ(int maxN)
        {
            Keys = new T[maxN + 1];
            pq = new int[maxN + 1];
            qp = new int[maxN + 1];
            Max = maxN;
            for (int i = 0; i <= maxN; i++)
            {
                qp[i] = -1;
            }
        }

        public int MaxLength()
        {
            return Max;
        }

        public void Change(int k, T element)
        {
            Keys[k] = element;
            // 因為更新元素時，所有的位置都已排序
            // 更完後要嘛這個元素要往上swim或往下sink(只有一種情況)
            // 所以同時呼叫sink跟swim是OK的
            Swim(qp[k]);
            Sink(qp[k]);
        }

        public bool Contains(int k)
        {
            return qp[k] != -1;
        }

        public void DeleteMin()
        {
            var minIndex = pq[1];
            Keys[minIndex] = default;

            // 刪除元素後重排
            Exch(1,N--);
            Sink(1);

            // 刪除該索引在樹中位置的紀錄
            qp[minIndex] = -1;
        }

        public void Delete(int k)
        {
            var deletedEleIndex = qp[k]; // 先取得欲刪除的索引元素在樹中的位置
            Exch(deletedEleIndex, N--);
            Swim(deletedEleIndex);
            Sink(deletedEleIndex);
            Keys[k] = default;
            qp[k] = -1;
        }

        public void Insert(int k,T element)
        {
            N++;
            Keys[k] = element;
            pq[N] = k;
            qp[k] = N;
            Swim(N);
        }

        public bool IsEmpty()
        {
            return N == 0;
        }

        public T Min()
        {
            return Keys[pq[1]];
        }

        public int MinIndex()
        {
            return pq[1];
        }

        public int Size()
        {
            return N;
        }

        #region Swim跟Sink方法不用調整，需調整Exch和Less方法
        /// <summary>
        /// 當某個節點比parentNode大時，會需要進行交換的動作
        /// </summary>
        /// <param name="i"></param>
        private void Swim(int i)
        {
            // 因為資料結構是以binary tree的方式處理，所以節點父節點在陣列的索引值必為其節點本身的1/2
            while (i > 1 && less(i / 2, i))
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
            while (2 * i <= size)
            {
                int j = 2 * i;
                // 判斷下沉時要往左邊還是右邊進行下沉
                // 因為若要以binary heap實現priority queue，且binary heap以array儲存時
                // 會需要確保父節點比兩個子節點都大
                // 因此這一段是要取得兩個子節點中值最大的索引
                if (j < size && less(j, j + 1))
                {
                    j++;
                }
                if (less(i, j)) break;
                Exch(i, j);
                i = j;
            }
        }

        private void Exch(int i, int j)
        {
            // 先換倒敘的值
            qp[pq[i]] = j;
            qp[pq[j]] = i;

            var pqTemp = pq[i];
            pq[i] = pq[j];
            pq[j] = pqTemp;
        }

        private bool less(int i, int j)
        {
            return Keys[pq[i]].CompareTo(Keys[pq[j]]) < 0;
        }
        #endregion
    }
}
