namespace LinearAlgebraLibrary.Solvers
{
    public class LUfactorization
    {
        public double[,] LMatrix(double[,] matrix)
        {
            double[,] unitMatrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                unitMatrix[i, i] = 1.0;
            }

            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                for (int j = i + 1; j < matrix.GetLength(0); j++)
                {
                    unitMatrix[j, i] = matrix[j, i] / matrix[i, i];
                    for (int k = i; k < matrix.GetLength(0); k++)
                    {
                        matrix[j, k] = matrix[j, k] - unitMatrix[j, i] * matrix[i, k];
                    }
                }

            }

            return unitMatrix;
        }

        public double[,] UMatrix(double[,] matrix)
        {
            double product = 0.0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, i] != 0)
                {
                    for (int j = i + 1; j < matrix.GetLength(0); j++)
                    {
                        product = matrix[j, i] / matrix[i, i];
                        for (int k = 0; k < matrix.GetLength(0); k++)
                        {
                            matrix[j, k] = matrix[j, k] - product * matrix[i, k];
                        }
                    }
                }
            }

            return matrix;
        }

        public double[] SolveSystem(double[,] L, double[,] U, double[] vector)
        {//FORWARD
            double[] solution = new double[vector.Length];
            double[] y = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                double product1 = 0.0;
                for (int j = 0; j < i; j++)
                {
                    product1 = product1 + L[i, j] * y[j];
                }
                y[i] = (vector[i] - product1) / L[i, i];
            }
            //BACK
            for (int i = y.Length - 1; i >= 0; i--)
            {
                double product2 = 0.0;
                for (int j = i + 1; j < y.Length; j++)
                {
                    product2 = product2 + U[i, j] * solution[j];
                }
                solution[i] = (y[i] - product2) / U[i, i];
            }

            return solution;
        }
    }
}
