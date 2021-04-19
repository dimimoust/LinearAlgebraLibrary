using System;
using LinearAlgebraLibrary.Matrices;

namespace LinearAlgebraLibrary.Solvers
{
    public class ConjugateGradient
    {
        public (double[],int,double) CG(double[,] A, double[] b, double accuracy, int maxIterations)
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
            double dTAd = 0.0;
            int iterations = 0;

            var res = 0.0;

            for (int i = 0; i < rows; i++)
            {
                var sum = 0.0;
                for (int j = 0; j < columns; j++)
                {
                    sum += A[i, j] * x[j];
                    d[i] = b[i]-sum;
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
                        dTAd += A[i, j] * d[i] * d[j];
                    }
                }

                double alpha = rTr / dTAd;

                double[] rn = new double[columns];
                for (int i = 0; i < columns; i++)
                {
                    x[i] += alpha * d[i];
                    rn[i] = residual[i];
                    for (int j = 0; j < columns; j++)
                    {
                        rn[i] -= alpha * A[i, j] * d[j];
                    }
                }

                double rnTrn = 0.0;
                double norm = 0.0;
                var sum = 0.0;
                for (int i = 0; i < columns; i++)
                {
                    rnTrn += rn[i] * rn[i];
                    norm += rn[i] * rn[i];
                    norm = Math.Sqrt(norm);
                    sum += r0[i]* r0[i];
                    sum = Math.Sqrt(sum);
                  
                }
                res = norm / sum;

                if (res < 1e-10)
                {
                    break;
                }

                double beta = rnTrn / rTr;
                for (int i = 0; i < columns; i++)
                {
                    d[i] = beta * d[i] + rn[i];
                    residual[i] = rn[i];
                }

                iterations = t;
            }

            return (x, iterations, res);
        }
    }
}