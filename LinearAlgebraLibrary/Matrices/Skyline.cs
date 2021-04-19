using System.Collections.Generic;

namespace LinearAlgebraLibrary.Matrices
{
    public class Skyline
    {
        public Skyline()
        {
            _listValues = new List<double>();
        }
        public (double[],int[]) SkylineStorage(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

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
                }
                height[indexColumn] = count;
            }


            for (int indexColumn = 0; indexColumn < columns; indexColumn++)
            {
                for (int indexRow = indexColumn; indexRow - height[indexColumn] >= 0; indexRow--)
                {
                    _listValues.Add(matrix[indexRow, indexColumn]);
                }
            }
            double[] values = _listValues.ToArray();
            int size = values.Length;

            int[] diagOffsets = new int[columns + 1];
            diagOffsets[columns] = size + 1;

            for (int indexRow = 1; indexRow < rows; indexRow++)
            {
                diagOffsets[indexRow] = diagOffsets[indexRow - 1] + (indexRow - height[indexRow - 1]);
            }

            
            double[] activeColumn = new double[columns];

            for (int j = 0; j < columns; j++)
            {
                activeColumn[j] = diagOffsets[j + 1] - diagOffsets[j] - 1;
            }

            return (values,diagOffsets);
        }

        List<double> _listValues;
        
    }
}
