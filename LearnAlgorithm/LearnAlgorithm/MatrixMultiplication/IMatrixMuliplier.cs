using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAlgorithm.MatrixMultiplication
{
    public interface IMatrixMuliplier<T> where T: struct
    {
        T[,] Multiply(T[,] matrixA, T[,] matrixB);
    }
}
