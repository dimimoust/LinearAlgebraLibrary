namespace LinearAlgebraLibrary.Utilities
{
    public class Determinant
    {
        public double[] DETcalculator(double[,] matrix)
        {
            int i, j, k;

            int size = matrix.GetLength(0);
            double det = 0.0;
            double[] determinant = new double[size];
            for (i = 0; i < size - 1; i++)
            {
                for (j = i + 1; j < size; j++)
                {
                    det = matrix[j, i] / matrix[i, i];
                    for (k = i; k < size; k++)
                    {
                        matrix[j, k] = matrix[j, k] - det * matrix[i, k];
                    }
                }
            }

            det = 1;
            for (i = 0; i < size; i++)
            {
                det = det * matrix[i, i];
                determinant[i] = det;
            }

            return determinant;
        }
    }
}