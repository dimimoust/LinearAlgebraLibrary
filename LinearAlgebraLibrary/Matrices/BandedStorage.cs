using System.Collections.Generic;
using System.Linq;

namespace LinearAlgebraLibrary.Matrices
{
    public class BandedStorage  //(int, int, double[,])
    {
        public BandedStorage()
        {
            _listValues = new List<double>();
        }
        public double[,] BandedStorageMethod(double[,] matrix)
        {
            double[] row = new double[matrix.GetLength(0)];
            double[] column = new double[matrix.GetLength(1)];
            int columns = matrix.GetLength(1);
            int rows = matrix.GetLength(0);
            int[] rowBand = new int[rows];
            int columnBandwidth = 0;
            int[] columnBand = new int[columns];
            int[] height = new int[columns];
            for (int indexColumn = 0; indexColumn < columns; indexColumn++)
            {
                int count = 0;
                for (int indexRow = 0; indexRow < indexColumn; indexRow++)
                {
                    if (matrix[indexRow, indexColumn] == 0)
                    {
                        count = count + 1;
                    }
                    else
                    {
                        break;
                    }

                    columnBand[indexColumn] = count;
                }

                height[indexColumn] = indexColumn - columnBand[indexColumn];
                columnBandwidth = height.Max();
            }

            for (int indexRow = 0; indexRow < rows; indexRow++)
            {
                int count = 0;
                for (int indexColumn = rows - 1; indexColumn >= indexRow + 1; indexColumn--)
                {
                    if (matrix[indexRow, indexColumn] == 0)
                    {
                        count = count + 1;
                    }
                    else
                    {
                        break;
                    }
                    rowBand[indexRow] = count;
                }
            }

            double[,] bandedMatrix = new double[columnBandwidth + 1, matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                bandedMatrix[0, i] = matrix[i, i];
            }

            for (int i = 0; i < columnBandwidth; i++)
            {
                for (int j = 0; j < matrix.GetLength(0) - i - 1; j++)
                {
                    bandedMatrix[i + 1, j] = matrix[j, j + i + 1];
                }
            }

            //for (int j = 0; j < bandedMatrix.GetLength(1); j++)
            //{
            //    for (int i = 0; i < bandedMatrix.GetLength(0); i++)
            //    {
            //        _listValues.Add(bandedMatrix[i, j]);
            //    }
            //}
            //  double[] values = _listValues.ToArray();

            double[,] values = bandedMatrix;
            return values;
        }

        public double ReturnValue(double[,] bandedMatrix, int i, int j)
        {
            double value = 0.0;
            for (int k = 0; k < bandedMatrix.GetLength(0); k++)
            {
                for (int l = 0; l < bandedMatrix.GetLength(1); l++)
                {
                    value = bandedMatrix[i, j];
                }
            }

            return value;
        }

        List<double> _listValues;
    }
}