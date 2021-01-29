using System;

namespace ch3_queue_and_stack
{
    class Program
    {
        static void Main(string[] args)
        {
            var intCircleQueue = new CircleQueue<int>(5);
            intCircleQueue.Enqueue(1);
            intCircleQueue.Enqueue(2);
            intCircleQueue.Enqueue(3);
            intCircleQueue.Enqueue(4);
            intCircleQueue.Enqueue(5);
            intCircleQueue.Dequeue();
            intCircleQueue.Dequeue();
            intCircleQueue.Dequeue();
        }
    }
}
