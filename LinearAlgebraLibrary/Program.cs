using System;
using System.Diagnostics;
using LinearAlgebra.Solvers;
using LinearAlgebraLibrary.Matrices;
using LinearAlgebraLibrary.Solvers;
using LinearAlgebraLibrary.Utilities;

namespace LinearAlgebraLibrary
{
    class Program
    {
        static void Main()
        {

            double[] actualSolution1 = { -2.875, 0.1875, 1.375 };
            double[] b = { 3, 4, 2 };
            double[,] matrix3 = { { 1, 2, 4 }, { 2, 8, 6 }, { 4, 6, 9 } };
            SSOR ssor = new SSOR();
            var (solution, _, _) = ssor.SSORMethod(matrix3, b, 100000, 10e-4, 1.3);

            //for (int i = 0; i < b.Length; i++)
            //{
            //    Debug.Assert(Math.Abs(solution[i] - actualSolution1[i]) < 10e-4);
            //}
        }
    }
}