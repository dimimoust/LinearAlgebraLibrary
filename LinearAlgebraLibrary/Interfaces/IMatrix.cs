using System.Runtime.CompilerServices;

namespace LinearAlgebraLibrary.Interfaces
{
    public interface IMatrix
    {
        //double this[int i, int j] { get; set; }
        int Rows { get; }
        int Columns { get; }
        double[] Multiplication(double[] vector);
    }
}