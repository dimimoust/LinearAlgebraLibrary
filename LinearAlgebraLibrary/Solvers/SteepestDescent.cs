using System;
using LinearAlgebraLibrary.Matrices;

namespace LinearAlgebra.Solvers
{
    public class SteepestDescent
    {

        public (double[], int, double) SD(double[,] A, double[] b, double accuracy, int maxIterations)
        {
            CompressedSparseRows csr = new CompressedSparseRows(A);

            int rows = csr.Rows;
            int columns = csr.Columns;
            int[] rowoffsets = csr.RowOffsets;
            int[] colindices = csr.ColIndices;
            double[] values = csr.Values;

            double[] x = new double[columns];
            double[] residual = new double[b.Length];
            double[] r0 = new double[b.Length];
            double[] d = new double[b.Length];
            double rTr = 0.0;
            double rTAr = 0.0;
            int iterations = 0;

            double norm = 0.0;

            for (int i = 0; i < rows; i++)
            {
                var sum = 0.0;
                for (int j = 0; j < columns; j++)
                {
                    sum += A[i, j] * x[j];
                    d[i] = b[i] - sum;
                    residual[i] = d[i];
                    r0 = residual;
                }
            }

            for (int t = 0; t < maxIterations; t++)
            {
                for (int i = 0; i < rows; i++)
                {
                    rTr += residual[i] * residual[i];
                }

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        rTAr += A[i, j] * residual[i] * residual[j];
                    }
                }

                double alpha = rTr / rTAr;

                double[] rn = new double[columns];
                for (int i = 0; i < columns; i++)
                {
                    x[i] += alpha * residual[i];
                    rn[i] = residual[i];
                    for (int j = 0; j < columns; j++)
                    {
                        rn[i] -= alpha * A[i, j] * residual[j];
                    }
                }

                var product = 0.0;
                for (int i = 0; i < columns; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        product += x[j] * A[i, j] * x[j];
                    }
                }

                norm = Math.Sqrt(product);

                if (norm < 1e-10)
                {
                    break;
                }

                for (int i = 0; i < columns; i++)
                {
                    residual[i] = rn[i];
                }

                iterations = t;
            }

            return (x, iterations, norm);
        }
    }
}
