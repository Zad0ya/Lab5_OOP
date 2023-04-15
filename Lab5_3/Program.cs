using System;
using System.Threading;

class Program
{
    static void Main()
    {
        int[] arr = { 3, 7, 1, 4, 10, 6, 115 };
        QuickSort(arr, 0, arr.Length - 1);
        Console.WriteLine(string.Join(" ", arr));
    }

    static void QuickSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(arr, left, right);

            Thread t1 = new Thread(() => QuickSort(arr, left, pivot - 1));
            Thread t2 = new Thread(() => QuickSort(arr, pivot + 1, right));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }
    }

    static int Partition(int[] arr, int left, int right)
    {
        int pivot = arr[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        int temp2 = arr[i + 1];
        arr[i + 1] = arr[right];
        arr[right] = temp2;

        return i + 1;
    }
}

