using System.Collections.Generic;
using LinearAlgebraLibrary.Interfaces;

namespace LinearAlgebraLibrary.Matrices
{
    public class COOrdinateListColumnMajor : IMatrix
    {

        private double[] values;
        private int rows;
        private int columns;
        private double[,] _valuesArray;
        private int[] rowsArray;
        private int[] columnsArray;

        public double[] Values
        {
            get => values;
            set => values = value;
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
        public int[] RowsArray
        {
            get => rowsArray;
            set => rowsArray = value;
        }

        public int[] ColumnsArray
        {
            get => columnsArray;
            set => columnsArray = value;
        }
        public COOrdinateListColumnMajor(double[,] matrix)
        {
            _listvalues = new List<double>();
            _listrows = new List<int>();
            _listcolumns = new List<int>();

            var (values, rowsArray, columnsArray) = COOrdinateListStorageColumnMajor(matrix);

            this.Values = values;
            this.RowsArray = rowsArray;
            this.ColumnsArray = columnsArray;
            this.Rows = rows;
            this.Columns = columns;
        }

        public (double[] values, int[] rowsArray, int[] columnsArray) COOrdinateListStorageColumnMajor(double[,] matrix)
        {
            int row = matrix.GetLength(0);
            int column = matrix.GetLength(1);
            for (int indexColumn = 0; indexColumn < column; indexColumn++)
            {
                for (int indexRow = 0; indexRow < row; indexRow++)
                {
                    if (matrix[indexRow, indexColumn] != 0)
                    {
                        _listvalues.Add(matrix[indexRow, indexColumn]);
                        _listrows.Add(indexRow);
                        _listcolumns.Add(indexColumn);
                    }
                }
            }

            double[] values = _listvalues.ToArray();
            int[] rowsArray = _listrows.ToArray();
            int[] columnsArray = _listcolumns.ToArray();

            return (values, rowsArray, columnsArray);
        }

        List<double> _listvalues;
        List<int> _listrows;
        List<int> _listcolumns;

        public double[] Multiplication(double[] vector)
        {
            //CoordinateList coordinatelist = new CoordinateList();
            //(double[] values, int[] rows, int[] columns) = coordinatelist.CoordinateListStorage(matrix);

            double[] product = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                product[i] = 0;
            }
            for (int k = 0; k < values.Length; k++)
            {
                product[rowsArray[k]] = product[rowsArray[k]] + values[k] * vector[columnsArray[k]];
            }

            return product;
        }

    }
}
