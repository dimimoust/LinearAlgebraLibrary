using System;
using LinearAlgebraLibrary.Utilities;

namespace LinearAlgebraLibrary.Solvers
{
    public class SOR
    {
        public (double[] x, double residual, int iterations) SORMethod(double[,] A, double[] b, int maxIterations, double accuracy, double w)
        {
            int rows = A.GetLength(0);
            int columns = A.GetLength(1);
            double residual = 0.0;

            Determinant det = new Determinant();
            double[] determinats = det.DETcalculator(A);
            for (int i = 0; i < rows; i++)
            {
                if (determinats[i] <= 0)
                {
                    throw new ArgumentException("The matrix ISNOT positive definite");
                }
            }

            double[] initialGuess = new double[b.Length];
            double[] x = initialGuess;
            double[] y = new double[b.Length];

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

                        x[i] = (1 - w) * x[i] + w * (b[i] - y[i]) / A[i, i];
                    }

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
