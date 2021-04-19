using System;
using LinearAlgebra.Solvers;
using LinearAlgebraLibrary.Matrices;
using LinearAlgebraLibrary.Utilities;

namespace LinearAlgebraLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            
            double[,] matrix3 =  { { 10, 1, 1 },
                                 { 1, 10, 1 },
                                 { 1, 1, 10 } };
            var matrix1 = new DenseMatrix(matrix3);
            var isDominant=matrix1.IsDiagonallyDominant(0);
        }
    }
}
