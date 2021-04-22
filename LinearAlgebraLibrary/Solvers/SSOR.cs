using System;
using System.Collections.Generic;
using System.Text;

namespace LinearAlgebraLibrary.Solvers
{
    public class SSOR
    {
        public (double[] x, double residual, int iterations) SSORMethod(double[,] A, double[] b, int maxIterations,
            double accuracy, double w)
        {

            int rows = A.GetLength(0);
            int columns = A.GetLength(1);
            double residual = 0.0;

            double[] initialGuess = new double[b.Length];
            double[] x = initialGuess;
            double[] x1_2 = initialGuess;
            double[] y = new double[b.Length];
            int iterations = 0;

            while (residual <= accuracy)
                //for (int t = 1; t < maxIterations; t++)
                //{
                for (int i = 0; i < rows; i++)
                {
                    y[i] = 0;
                    for (int j = 0; j < i; j++)
                    {
                        y[i] = y[i] + A[i, j] * x1_2[j];
                    }

                    for (int j = i + 1; j < columns; j++)
                    {
                        y[i] = y[i] + A[i, j] * x[j];
                    }

                    y[i] = (b[i] - y[i]) / A[i, i];

                    x1_2[i] = x[i] + w * (y[i] - x[i]);
                }

            for (int i = rows - 1; i >= 0; i--)
            {
                y[i] = 0;
                for (int j = 0; j < i; j++)
                {
                    y[i] = y[i] + A[i, j] * x1_2[j];
                }

                for (int j = i + 1; j < columns; j++)
                {
                    y[i] = y[i] + A[i, j] * x[j];
                }

                y[i] = (b[i] - y[i]) / A[i, i];

                x[i] = x1_2[i] + w * (y[i] - x1_2[i]);
            }
            // iterations = t;

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
                residual += Math.Pow(result[i], 2);
            }
            residual = Math.Sqrt(residual);
            //}

            return (x, residual, iterations);
        }
    }
}

