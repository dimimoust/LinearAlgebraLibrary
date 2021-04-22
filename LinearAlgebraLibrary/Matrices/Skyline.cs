using System.Collections.Generic;
using LinearAlgebraLibrary.Interfaces;

namespace LinearAlgebraLibrary.Matrices
{
    public class Skyline //: IMatrixSym
    {
        private double[] valuesArray;
        private int[] diagOffsets;
        private double[] activeColumn;
        private double _value;
        public double[] ValuesArray
        {
            get => valuesArray;
            set => valuesArray = value;
        }
        public double Value
        {
            get => _value;
            set => _value = value;
        }
        
        //public double this[int i, int j]
        //{
        //    get => Values[i, j];
        //    set => Values[i, j] = value;
        //}
        public int Length
        {
            get => valuesArray.Length;
        }
        public int[] DiagOffsets
        {
            get => diagOffsets;
            set => diagOffsets = value;
        }
        public double[] ActiveColumn
        {
            get => activeColumn;
            set => activeColumn = value;
        }
        public Skyline(double[,] matrix, int i, int j)
        {
            _listValues = new List<double>();
            
            var (values, diagOffsets, activeColumn) = SkylineStorage(matrix);
         
            this.DiagOffsets = diagOffsets;
            this.ValuesArray = values;
            this.ActiveColumn = activeColumn;

            double _value = ReturnValue(i, j);
            this.Value = _value;
        }
        public (double[],int[], double[]) SkylineStorage(double[,] matrix)
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
            
            return (values,diagOffsets, activeColumn);
        }

        List<double> _listValues;
        public double ReturnValue(int i, int j)
        {
            double value = 0.0;
            if (j - i < ActiveColumn[j])
            {
                value = ValuesArray[DiagOffsets[j] + j - i];
            }

            return value;
        }
    }

}