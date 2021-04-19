using System;
using System.Diagnostics;
using LinearAlgebraLibrary.Matrices;
using LinearAlgebraLibrary.Solvers;
using LinearAlgebraLibrary.Utilities;
using Xunit;

namespace TestOperations
{
    public class TestResults
    {
        [Fact]
        public void TestMultiplication()
        {

            ///Arrange
            ///
            double[,] matrix1 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            double[] vector1 = { 2, 4, 5 };

            //DenseMatrix
            DenseMatrix denseMatrix = new DenseMatrix(matrix1);

            //CSR
            CompressedSparseRows csr = new CompressedSparseRows(matrix1);

            //TransposeMatrix
            Transpose transposeMatrix = new Transpose();
            double[,] matrixT = transposeMatrix.TransposeMatrix(matrix1);

            //COOrdinateListRowMajor
            COOrdinateListRowMajor rowCOO = new COOrdinateListRowMajor(matrix1);

            ///Act
            /// 
            double[] expectedProduct1 = denseMatrix.Multiplication(vector1);
            double[] expectedProduct2 = csr.MultiplicationAx(vector1);
            double[] expectedProduct3 = csr.MultiplicationATx(vector1);
            double[] expectedProduct4 = rowCOO.Multiplication(vector1);

            ///
            double[] actualResult = new double[] { 25, 58, 91 };
            double[] actualTransMatrixMultiplic = new double[] { 53, 64, 75 };

            ///Assert
            /// 
            for (int i = 0; i < vector1.Length; i++)
            {
                Assert.Equal(actualResult[i], expectedProduct1[i]);
            }

            Assert.Equal(actualResult, expectedProduct2);
            Assert.Equal(actualTransMatrixMultiplic, expectedProduct3);
            Assert.Equal(actualResult, expectedProduct4);
        }

        [Fact]
        public void TestStorage()
        {
            double[,] matrix2 =
            {
                {9, 0, 3, 0},
                {0, 8, 0, 0},
                {0, 2, 6, 0},
                {1, 0, 0, 5},
            };
            double[] vector2 = { 1, 2, 3, 4 };

            //CSR
            CompressedSparseRows csr = new CompressedSparseRows(matrix2);
            double[] values = csr.Values;
            int[] colindices = csr.ColIndices;
            int[] rowoffsets = csr.RowOffsets;

            double[] actualValues = { 9, 3, 8, 2, 6, 1, 5 };
            int[] actualColindices = { 0, 2, 1, 1, 2, 0, 3 };
            int[] actualRowoffsets = { 0, 2, 3, 5, 7 };
            double[] actualProduct2 = { 21, 16, 22, 21 };
            Assert.Equal(values, actualValues);
            Assert.Equal(colindices, actualColindices);
            Assert.Equal(rowoffsets, actualRowoffsets);

            //COOrdinateListRowMajor
            COOrdinateListRowMajor rowCOO = new COOrdinateListRowMajor(matrix2);
            double[] valuesCOOrow = rowCOO.Values;
            int[] colindicesCOOrow = rowCOO.ColumnsArray;
            int[] rowoffsetsCOOrow = rowCOO.RowsArray;

            double[] actualValuesCOOrow = { 9, 3, 8, 2, 6, 1, 5 };
            int[] actualColindicesCOOrow = { 0, 2, 1, 1, 2, 0, 3 };
            int[] actualRowoffsetsCOOrow = { 0, 0, 1, 2, 2, 3, 3 };

            Assert.Equal(valuesCOOrow, actualValuesCOOrow);
            Assert.Equal(colindicesCOOrow, actualColindicesCOOrow);
            Assert.Equal(rowoffsetsCOOrow, actualRowoffsetsCOOrow);

            //COOrdinateListColumnMajor
            COOrdinateListColumnMajor columnCOO = new COOrdinateListColumnMajor(matrix2);
            double[] valuesCOOcolumn = columnCOO.Values;
            int[] colindicesCOOcolumn = columnCOO.ColumnsArray;
            int[] rowoffsetsCOOcolumn = columnCOO.RowsArray;

            double[] actualValuesCOOcolumn = { 9, 1, 8, 2, 3, 6, 5 };
            int[] actualColindicesCOOcolumn = { 0, 0, 1, 1, 2, 2, 3 };
            int[] actualRowoffsetsCOOcolumn = { 0, 3, 1, 2, 0, 2, 3 };
            Assert.Equal(valuesCOOcolumn, actualValuesCOOcolumn);
            Assert.Equal(colindicesCOOcolumn, actualColindicesCOOcolumn);
            Assert.Equal(rowoffsetsCOOcolumn, actualRowoffsetsCOOcolumn);


            //Banded Storage
            double[,] bandedmatrix =
            {
                {21, 1, 0, 4, 0},
                {1, 22, 2, 0, 0},
                {0, 2, 23, 1, 3},
                {4, 0, 1, 24, 2},
                {0, 0, 3, 2, 25},
            };
            BandedStorage exercise9 = new BandedStorage();
            var bandedValues = exercise9.BandedStorageMethod(bandedmatrix);

            double[,] actualBandedValues =
            {
                {21, 22, 23, 24, 25},
                {1, 2, 1, 2, 0},
                {0, 0, 3, 0, 0},
                {4, 0, 0, 0, 0},
            };
            Assert.Equal(bandedValues, actualBandedValues);

            //PackedStorageLower
            double[,] lowerTriangleMatrix =
            {
                {1, 0, 0, 0, 0},
                {2, 3, 0, 0, 0},
                {4, 5, 6, 0, 0},
                {7, 8, 9, 10, 0},
                {11, 12, 13, 14, 15}
            };
            double[,] upperTriangleMatrix =
            {
                {1, 2, 3},
                {0, 5, 7},
                {0, 0, 8}
            };

            PackedStorageLower packedLow = new PackedStorageLower();
            var lowerColumn = packedLow.ColumnMajorLayout(lowerTriangleMatrix);
            var lowerRow = packedLow.RowMajorLayout(lowerTriangleMatrix);

            PackedStorageUpper packedUp = new PackedStorageUpper();
            var upperColumn = packedUp.ColumnMajorLayout(upperTriangleMatrix);
            var upperRow = packedUp.RowMajorLayout(upperTriangleMatrix);

            double[] actualPackedLowerColumn =
            {
                1, 2, 4, 7, 11, 3, 5, 8, 12, 6, 9, 13, 10, 14, 15
            };
            double[] actualPackedLowerRow =
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15
            };

            double[] actualPackedUpperColumn =
            {
                1, 2, 5, 3, 7, 8
            };
            double[] actualPackedUpperRow =
            {
                1, 2, 3, 5, 7, 8
            };

            Assert.Equal(lowerColumn, actualPackedLowerColumn);
            Assert.Equal(lowerRow, actualPackedLowerRow);
            Assert.Equal(upperColumn, actualPackedUpperColumn);
            Assert.Equal(upperRow, actualPackedUpperRow);


            // SkyLine Storage
            double[,] skymatrix =
            {
                {1, -1, 0, -3, 0},
                {-1, 5, 0, 0, 0},
                {0, 0, 4, 6, 4},
                {-3, 0, 6, 7, 0},
                {0, 0, 4, 0, -5},
            };

            Skyline skyline = new Skyline();
            var (_, diagOffsets) = skyline.SkylineStorage(skymatrix);

            int[] actualDiagonals = { 0, 1, 3, 4, 8, 12 };

            Assert.Equal(diagOffsets, actualDiagonals);

        }

        [Fact]
        public void TestUtilities()
        {

            double[,] matrix3 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            //TransposeMatrix
            Transpose transposeMatrix = new Transpose();
            double[,] matrixT = transposeMatrix.TransposeMatrix(matrix3);

            double[,] actualTransMatrix = { { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 } };

            Assert.Equal(actualTransMatrix, matrixT);
        }

        [Fact]
        public void TestSolvers()
        {
            //LU Factorization
            double[,] matrix4 = { { 1, 2, 4 }, { 3, 8, 14 }, { 2, 6, 13 } };
            double[,] sym_posMatrix = { { 3, 4, 3 }, { 4, 8, 6 }, { 3, 6, 9 } };
            double[] b = { 3, 13, 4 };
            LUfactorization lufactorization = new LUfactorization();
            double[,] L = lufactorization.LMatrix(matrix4);
            double[,] U = lufactorization.UMatrix(matrix4);
            double[] solution1 = lufactorization.SolveSystem(L, U, b);

            //Jacobi
            //Jacobi jacobi = new Jacobi();
            //(double[] solution2, _, _) = jacobi.JacobiMethod(matrix4, b, 4, 1e-6);

            //Cholesky Factorization
            CholeskyFactorization choleskyFactorization = new CholeskyFactorization();

            var (matrixL, matrixLT) = choleskyFactorization.ComposeLLTMatrix(sym_posMatrix);
            double[] solution3 = choleskyFactorization.SolveEquationSystem(matrixL, matrixLT, b);


            //    //GaussSiedel
            //    GaussSiedel gaussSiedel = new GaussSiedel();
            //    (var solution4,_,_) = gaussSiedel.GaussSiedelMethod(matrix4, b, 30, 1e-6);

            //check
            double[] actualSolution1 = { 3, 4, -2 };

            Assert.Equal(solution1, actualSolution1);

            double[] actualSolution2 = { -3.5, 4.333, -1.2778 };

            double tol = 1E-3;

            //for (int i = 0; i < solution2.Length; i++)
            //{
            //    Debug.Assert(Math.Abs(solution2[i] - actualSolution1[i]) < tol); 
            //}

            for (int i = 0; i < solution3.Length; i++)
            {
                Debug.Assert(Math.Abs(solution3[i] - actualSolution2[i]) < tol);
            }

            //    //for (int i = 0; i < solution3.Length; i++)
            //    //{
            //    //    Debug.Assert(Math.Abs(solution4[i] - actualSolution1[i]) < tol);
            //    //}
        }
    }
}