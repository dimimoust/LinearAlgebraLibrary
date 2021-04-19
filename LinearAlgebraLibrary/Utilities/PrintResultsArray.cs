using System;

namespace LinearAlgebraLibrary.Utilities
{
    public static class PrintResultsArray
    {
        public static void PrintArray(int n, double[] x)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(x[i]);      // or Console.Write(x[i] + " ") for 1row Print
                Console.WriteLine(" ");
            }
            Console.WriteLine();
        }


    }
}
