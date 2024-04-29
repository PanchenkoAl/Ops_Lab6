
using System.Diagnostics;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        int n = 300;
        int m = 400;
        int k = 500;
        int[,] matrixA = new int[n, m];
        int[,] matrixB = new int[m, k];
        int[,] matrixC = new int[n, k];
        int[] result = new int[n * k];

        var dataList = new List<(int[], int[], int, int)>();

        Random r = new Random();

        for (int i = 0; i < n; i++) for (int j = 0; j < m; j++) matrixA[i, j] = Math.Abs(r.Next() % 100);
        //Console.WriteLine("Matrix A: ");
        //for (int i = 0; i < n; i++)
        //{
        //    for (int j = 0; j < m; j++)
        //    {
        //        Console.Write(matrixA[i, j] + " ");
        //    }
        //    Console.WriteLine();
        //}
        for (int i = 0; i < m; i++) for (int j = 0; j < k; j++) matrixB[i, j] = Math.Abs(r.Next() % 100);
        //Console.WriteLine("Matrix B: ");
        //for (int i = 0; i < m; i++)
        //{
        //    for (int j = 0; j < k; j++)
        //    {
        //        Console.Write(matrixB[i, j] + " ");
        //    }
        //    Console.WriteLine();
        //}


        for (int i = 0; i < n; i ++)
        {
            for(int j = 0; j < k; j ++)
            {
                int[] tempA = new int[m];
                int[] tempB = new int[m];

                for (int d = 0; d < m; d ++)
                {
                    tempA[d] = matrixA[i, d];
                    tempB[d] = matrixB[d, j];
                }

                dataList.Add((tempA, tempB, i, j));
            }
        }

        Stopwatch stopwatch1 = new Stopwatch();

        stopwatch1.Start();
        Parallel.For(0, dataList.Count, i =>
            {
                matrixC[dataList[i].Item3, dataList[i].Item4] = ProcessVectors(dataList[i].Item1, dataList[i].Item2);
            });
        stopwatch1.Stop();

        Console.WriteLine("Parallel processing resulted with: " + stopwatch1.Elapsed.TotalMilliseconds);

        Stopwatch stopwatch2 = new Stopwatch();

        stopwatch2.Start();
        for (int i = 0; i < dataList.Count; i ++)
        {
            matrixC[dataList[i].Item3, dataList[i].Item4] = ProcessVectors(dataList[i].Item1, dataList[i].Item2);
        }
        stopwatch2.Stop();

        Console.WriteLine("Parallel processing resulted with: " + stopwatch2.Elapsed.TotalMilliseconds);

        //Console.WriteLine("Matrix C: ");
        //for(int i = 0; i < n; i ++)
        //{
        //    for(int j = 0; j < k; j ++)
        //    {
        //        Console.Write(matrixC[i, j] + " ");
        //    }
        //    Console.WriteLine();
        //}
    }

    static int ProcessVectors(int[] v1, int[] v2)
    {
        int result = 0;
        for(int i = 0; i < v1.Length; i++)
        {
            result += v1[i] * v2[i];
        }
        return result;
    }
}