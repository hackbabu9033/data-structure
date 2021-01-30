using System;
using System.Collections.Generic;
using System.Text;

namespace ch3_queue_and_stack
{
    public class CircleQueue<T>
    {
        private int Length { set; get; }
        private int Front { set; get; }
        private int Rear { set; get; }

        private T[] Items { set; get; }
        public CircleQueue(int length)
        {
            Length = length;
            Front = -1;
            Rear = -1;
            Items = new T[Length];
        }

        public void Enqueue(T item)
        {
            var nextRear = (Rear + 1) % Length;

            if (Front == -1)
            {
                Front = 0;
                Items[nextRear] = item;
                Rear = nextRear;
                return;
            }
         
            if (Front == nextRear)
            {
                Console.WriteLine("queue is full");
                return;
            }
            Items[nextRear] = item;
            Rear = nextRear;
            Console.WriteLine("item is enqueued successfully");
        }

        public void Dequeue()
        {
            var nextFront = (Front + 1) % Length;
            if (Front == -1)
            {
                Console.WriteLine("queue is empty, nothing to delete !!");
                return;
            }
            else if (Front == Rear)
            {
                Items[Front] = default;
                Front = -1;
                Rear = -1;
                return;
            }

            Items[Front] = default;
            Front = nextFront;
        }

        public void printCircleQueue()
        {
            Console.WriteLine($@"current front：{Front}");
            Console.WriteLine($@"current rear：{Rear}");
            foreach (var item in Items)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
