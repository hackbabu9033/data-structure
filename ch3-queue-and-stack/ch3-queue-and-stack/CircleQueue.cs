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

        private bool Isempty { set; get; }

        private T[] Items { set; get; }
        public CircleQueue(int length)
        {
            Length = length;
            Front = -1;
            Rear = -1;
            Isempty = true; // when Front is equal to Rear, check whether the queue is empty
            Items = new T[Length];
        }

        public void Enqueue(T item)
        {
            var nextRear = (Rear + 1) % Length;

            // for first element set front and rear to 0
            if (Front == -1)
            {
                Front = 0;
                Items[Front] = item;
                Rear = 0;
                Isempty = false;
                this.PrintCircleQueue();
                return;
            }

            if (Isempty)
            {
                Items[Rear] = item;
                Isempty = !Isempty;
                this.PrintCircleQueue();
                return;
            }
            
            if (nextRear == Front)
            {
                Console.WriteLine("Queue is full");
                return;
            }
            Items[nextRear] = item;
            Rear = nextRear;
            Isempty = false;
            Console.WriteLine("item is enqueued successfully");
            this.PrintCircleQueue();
        }

        public void Dequeue()
        {
            if (Front == Rear)
            {
                if (Isempty)
                {
                    Console.WriteLine("Queue is empty and nothing to delete");
                    return;
                }
                Items[Front] = default;
                Isempty = !Isempty;
                this.PrintCircleQueue();
                return;
            }
            var nextFront = (Front + 1) % Length;
            Items[Front] = default;
            Front = nextFront;
            this.PrintCircleQueue();
        }

        public void PrintCircleQueue()
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
