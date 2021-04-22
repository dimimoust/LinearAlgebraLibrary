using System.Collections.Generic;

namespace LinearAlgebraLibrary.Utilities
{
    public class Determinant
    {
        public Determinant()
        {
            _cofactors = new List<double>();
        }
        public double[] getFirstRowFactors(double[,] matrix)
        {
            int size = matrix.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                _cofactors.Add(matrix[0,i]);
            }

            double[] coFactors = _cofactors.ToArray();
            return coFactors;
        }
        List<double> _cofactors;
    }
}