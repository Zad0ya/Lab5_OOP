using System;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        int[,] matrix1 = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        int[,] matrix2 = new int[3, 3] { { 9, 8, 7 }, { 6, 5, 4 }, { 3, 2, 1 } };
        int[,] result = new int[3, 3];

        ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();


        Parallel.For(0, result.GetLength(0), i =>
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                int sum = 0;
                for (int k = 0; k < matrix2.GetLength(0); k++)
                {
                    sum += matrix1[i, k] * matrix2[k, j];
                }
                rwl.EnterWriteLock();
                try
                {
                    result[i, j] = sum;
                }
                finally
                {
                    rwl.ExitWriteLock();
                }
            }
        });
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                Console.Write(result[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
