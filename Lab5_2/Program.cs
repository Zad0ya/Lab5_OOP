using System;
using System.Threading;

class Program
{
    static Semaphore semaphore = new Semaphore(2, 2);
    static object locker = new object();

    static void Main()
    {
        Thread northThread = new Thread(() => TrafficLights("Північний"));
        Thread eastThread = new Thread(() => TrafficLights("Східний"));
        Thread southThread = new Thread(() => TrafficLights("Південний"));
        Thread westThread = new Thread(() => TrafficLights("Західний"));

        northThread.Start();
        eastThread.Start();
        southThread.Start();
        westThread.Start();

        northThread.Join();
        eastThread.Join();
        southThread.Join();
        westThread.Join();
    }

    static void TrafficLights(string direction)
    {
        while (true)
        {
            lock (locker)
            {
                Console.WriteLine($"{direction} світлофор зелений.");
                semaphore.WaitOne();
                Thread.Sleep(5000);
                Console.WriteLine($"{direction} світлофор жовтий.");
                Thread.Sleep(2000);
                Console.WriteLine($"{direction} світлофор красний.");
                semaphore.Release();
            }
            Thread.Sleep(1000);
        }
    }
}

