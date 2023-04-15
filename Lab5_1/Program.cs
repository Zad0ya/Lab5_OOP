using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static Queue<int> queue = new Queue<int>();
    static readonly Random random = new Random();

    static void Main()
    {
        Thread producerThread = new Thread(Producer);
        Thread consumerThread = new Thread(Consumer);

        producerThread.Start();
        consumerThread.Start();

        producerThread.Join();
        consumerThread.Join();
    }

    static void Producer()
    {
        while (true)
        {
            int num = random.Next(10);
            queue.Enqueue(num);
            Console.WriteLine($"Виробник додав до спільної черги {num}.");
            Thread.Sleep(random.Next(1000));
        }
    }

    static void Consumer()
    {
        while (true)
        {
            if (queue.Count > 0)
            {
                int num = queue.Dequeue();
                Console.WriteLine($"Споживач прибрав з спільної черги {num}.");
            }
            Thread.Sleep(random.Next(2500));
        }
    }
}

