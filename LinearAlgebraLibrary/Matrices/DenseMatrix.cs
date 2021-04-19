using System;
using System.Diagnostics;
using LinearAlgebraLibrary.Interfaces;

namespace LinearAlgebraLibrary.Matrices
{
    public class DenseMatrix : IMatrix
    {
        private readonly double[,] _values;
        private int rows;
        private int columns;

        public DenseMatrix(double[,] values)
        {
            _values = values;
            rows = values.GetLength(0);
            columns = values.GetLength(1);
        }

        public double this[int i, int j]
        {
            get => _values[i, j];
            set => _values[i, j] = value;
        }

        public int Rows
        {
            get => rows;
            set => rows = value;
        }

        public int Columns
        {
            get => columns;
            set => columns = value;
        }
        //public double GetValue(int i, int j) => _values[i, j];
        //public void SetValue(int i, int j, double value) => _values[i, j] = value;

        public double[] Multiplication(double[] vector)
        {
            var result = new double[_values.GetLength(0)];
            var rows = _values.GetLength(0);
            var columns = _values.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                var sum = 0.0;
                for (int j = 0; j < columns; j++)
                {
                    sum += _values[i, j] * vector[j];
                }

                result[i] = sum;
            }

            return result;
        }
    }
}

