using System;

namespace LinearAlgebraLibrary.Solvers
{
    public class Jacobi
    {
        public (double[] x, double residual, int iterations) JacobiMethod(double[,] A, double[] b, int maxIterations, double accuracy)
        {
            int rows = A.GetLength(0);
            int columns = A.GetLength(1);
            double[,] D = new double[rows, columns];
            double[,] L = new double[rows, columns];
            double[,] U = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                D[i, i] = A[i, i];
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    L[i, j] = -A[i, j];
                }
            }


            for (int i = 0; i < rows; i++)
            {
                for (int j = i + 1; j < columns; j++)
                {
                    U[i, j] = -A[i, j];
                }
            }

            double[] initialGuess = new double[b.Length];
            double[] x = initialGuess;
            double[] y = new double[b.Length];
            double residual = 0.0;
            int iterations = 0;
            while (residual <= accuracy)
            {
                for (int t = 1; t < maxIterations; t++)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        y[i] = 0;
                        for (int j = 0; j < columns; j++)
                        {
                            if (j != i)
                            {
                                y[i] = y[i] + A[i, j] * x[j];
                            }
                        }

                        y[i] = (b[i] - y[i]) / A[i, i];
                    }

                    x = y;
                    iterations = t;
                }


                double[] result = new double[b.Length];
                for (int i = 0; i < rows; i++)
                {
                    var sum = 0.0;
                    for (int j = 0; j < columns; j++)
                    {
                        sum += A[i, j] * x[j];
                        result[i] = b[i] - sum;
                    }
                }

                for (int i = 0; i < columns; i++)
                {
                    residual += Math.Sqrt(Math.Pow(result[i], 2));
                }

            }

            return (x, residual, iterations);
        }
    }
}
