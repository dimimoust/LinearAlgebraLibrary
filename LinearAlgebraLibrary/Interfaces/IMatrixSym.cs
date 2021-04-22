using System;
using System.Collections.Generic;
using System.Text;

namespace LinearAlgebraLibrary.Interfaces
{
    public interface IMatrixSym
    {
        double this[int i, int j] { get; set; }
        int Length { get; }
    }
}
