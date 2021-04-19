using System;

namespace LinearAlgebraLibrary.Solvers
{
    public class CholeskyFactorization
    {
        public (double[,], double[,]) ComposeLLTMatrix(double[,] matrix)
        {

            double[,] matrixL = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    double sum = 0.0;
                    if (j == i)
                    {
                        for (int k = 0; k < j; k++)
                        {
                            sum += Math.Pow(matrixL[j, k], 2);
                        }

                        matrixL[j, j] = Math.Sqrt(matrix[j, j] - sum);
                    }
                    else
                    {
                        for (int k = 0; k < j; k++)
                        {
                            sum += (matrixL[i, k] * matrixL[j, k]);
                        }

                        matrixL[i, j] = (matrix[i, j] - sum) / matrixL[j, j];
                    }
                }
            }

            double[,] Ltransposed = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Ltransposed[i, j] = matrixL[j, i];
                }
            }

            return (matrixL, Ltransposed);
        }

        public double[] SolveEquationSystem(double[,] matrixL, double[,] Ltransposed,double[] vector)
        {
            double[] y = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                double product = 0.0;
                for (int j = 0; j < i; j++)
                {
                    product = product + matrixL[i, j] * y[j];
                }
                y[i] = (vector[i] - product) / matrixL[i, i];
            }
            double[] solution = new double[vector.Length];

            for (int i = vector.Length - 1; i >= 0; i--)
            {
                double product = 0.0;
                for (int j = i + 1; j < vector.Length; j++)
                {
                    product = product + Ltransposed[i, j] * solution[j];
                }
                solution[i] = (y[i] - product) / Ltransposed[i, i];
            }

            return solution;
        }
    }
}
