using System;
using System.Collections.Generic;
using System.Text;

namespace Alg4Exercise.Ch2.MaxPQ
{
    public interface IMaxPQ<T> where T : IComparable
    {
        void MaxPQ(int count);
        void MaxPQ(IEnumerable<T> collection);
        void Insert(T data);
        T Max();
        T DeleteMax();
        bool IsEmpty();
        int Size();
    }
}
