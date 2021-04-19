using System;
using System.Collections.Generic;
using System.Text;
using LinearAlgebraLibrary.Interfaces;
using LinearAlgebraLibrary.Matrices;

namespace LinearAlgebraLibrary.Utilities
{
    public static class MatrixExtensions
    {
        public static bool IsDiagonallyDominant(this IMatrix matrix, int a)
        {
            int rows = matrix.Rows;
            int columns = matrix.Columns;
            double[] sumRow = new double[rows];
            double diagonals = 0.0;


            for (int i = 0; i < rows; i++)
            {
                diagonals = diagonals + matrix[i, i];
            }

            for (int i = 0; i < rows; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < columns; j++)
                {
                    if (i != j)
                    {
                        sum = sum + matrix[i, j];
                    }
                }

                sumRow[i] = Math.Abs(sum);

                if (diagonals < sumRow[i])
                {
                    return false;
                }
            }
         

            return true;
        }
    }
}
