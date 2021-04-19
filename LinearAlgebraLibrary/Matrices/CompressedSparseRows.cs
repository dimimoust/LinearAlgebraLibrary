using System.Collections.Generic;

namespace LinearAlgebraLibrary.Matrices
{
    public class CompressedSparseRows
    {
        private double[] values;
        private int[] colIndices;
        private int[] rowoffsets;

        public double[] Values
        {
            get => values;
            set => values = value;
        }

        public int[] ColIndices
        {
            get => colIndices;
            set => colIndices = value;
        }

        public int[] RowOffsets
        {
            get => rowoffsets;
            set => rowoffsets = value;
        }

        public int Rows;
        public int Columns;
        
        public CompressedSparseRows(double[,] matrix)
        {
            _listValues = new List<double>();
            _listColIndices = new List<int>();

            var (values, colindices, rowoffsets) = CSRstorage(matrix);

            this.Values = values;
            this.ColIndices = colindices;
            this.RowOffsets = rowoffsets;
            this.Columns = matrix.GetLength(1);
            this.Rows = matrix.GetLength(0);
        }

        public (double[] values, int[] colindices, int[] rowoffsets) CSRstorage(double[,] matrix)
        {
            int row = matrix.GetLength(0);
            int column = matrix.GetLength(1);
            int[] rowoffsets = new int[row + 1];
            for (int indexRow = 0; indexRow < row; indexRow++)
            {
                for (int indexColumn = 0; indexColumn < column; indexColumn++)
                {
                    if (matrix[indexRow, indexColumn] != 0)
                    {
                        _listValues.Add(matrix[indexRow, indexColumn]);
                        _listColIndices.Add(indexColumn);
                    }

                    if (matrix[0, indexColumn] != 0)
                    {
                        rowoffsets[0] = 0;
                    }
                }

                rowoffsets[indexRow + 1] = _listValues.Count;
            }

            rowoffsets[row] = _listValues.Count;

            double[] values = _listValues.ToArray();
            int[] colIndices = _listColIndices.ToArray();


            return (values, colIndices, rowoffsets);

        }

        List<double> _listValues;
        List<int> _listColIndices;

        public double[] MultiplicationAx(double[] vector)
        {
            int size = rowoffsets.Length-1 ;
            double[] product = new double[size];
            for (int i = 0; i < size; i++)
            {
                product[i] = 0;
                for (int k = rowoffsets[i]; k < rowoffsets[i + 1]; k++)
                {
                    product[i] = product[i] + values[k] * vector[colIndices[k]];
                }
            }
            return product;
        }

        public double[] MultiplicationATx(double[] vector)
        {
            int rowsAT = Columns;
            int columnsAT = Rows;
            double[] product = new double[rowsAT];
            for (int i = 0; i < columnsAT; i++)
            {
                product[i] = 0;
            }

            for (int i = 0; i < columnsAT; i++)
            {
                for (int k = rowoffsets[i]; k < rowoffsets[i + 1]; k++)
                {
                    product[colIndices[k]] = product[colIndices[k]] + values[k] * vector[i];
                }
            }

            return product;

        }
    }
}
