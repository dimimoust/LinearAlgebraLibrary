﻿namespace LinearAlgebraLibrary.Matrices
{
    public class PackedStorageUpper
    {
        //Column Major Layout 
        public double[] ColumnMajorLayout(double[,] matrix)
        {
            int length = matrix.GetLength(1);
            int entries = length * (length + 1) / 2;
            double[] array = new double[entries];

            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i <= j; i++)
                {
                    array[i + j * (j + 1) / 2] = matrix[i, j];
                }
            }

            return array;
        }

        //Row Major Layout 
        public double[] RowMajorLayout(double[,] matrix)
        {
            int length = matrix.GetLength(1);
            int entries = length * (length + 1) / 2;
            double[] array = new double[entries];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (i <= j)
                    {
                        array[j + (2 * length - i - 1) * i / 2] = matrix[i, j];
                    }
                }
            }

            return array;
        }

        //public double[] MultiplyWithVector(double[,] matrix, double[] vector)
        //{
        //    PackedStorage a = new PackedStorage();
        //   var(array,rows,columns)  = a.Row_Major_Layout(matrix);
        //    double[] y = new double[vector.Length];
        //    for (int i = 0; i < vector.Length; i++)
        //    {
        //        y[i] = 0;
        //        for (int j = 0; j < vector.Length;  j++)
        //        {
        //            if (i <= j)
        //            {
        //                y[i] = y[i] + array[columns[j]] * vector[i];
        //            }
        //            else
        //            {
        //                y[i] = y[i] + array[columns[i]]* vector[i];
        //            }
        //        }
        //    }
        //    return y;
        //}

    }
}
