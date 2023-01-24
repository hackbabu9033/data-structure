using System;
using System.Collections.Generic;
using System.Text;

namespace Alg4Exercise.Ch2.MinPQ
{
    public interface IIndexMinPQ<T> where T : IComparable
    {
        void Insert(int k, T element);

        void Change(int k, T element);
        bool Contains(int k);
        void Delete(int k);
        T Min();
        int MinIndex();
        bool IsEmpty();
        int Size();
        int MaxLength();
    }
}
