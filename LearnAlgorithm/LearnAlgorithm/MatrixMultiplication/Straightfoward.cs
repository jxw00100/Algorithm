using System;

namespace LearnAlgorithm.MatrixMultiplication
{
    /*
     * Θ(n3)
     */
    public class Straightfoward<T>:IMatrixMuliplier<T> where T: struct
    {
        public T[,] Multiply(T[,] matrixA, T[,] matrixB)
        {
            Int32 rowsCountA = matrixA.GetLength(0), 
                rowsCountB = matrixB.GetLength(0), 
                columnsCountA = matrixA.GetLength(1), 
                columnsCountB = matrixB.GetLength(1);

            if(columnsCountA != rowsCountB) throw new ArgumentException("Matrix size between A and B are not match");

            T[,] result = new T[rowsCountA, columnsCountB];

            for (var i = 0; i < rowsCountA; i++)
            {
                for (var j = 0; j < columnsCountB; j++)
                {
                    dynamic sum = 0;
                    for (var k = 0; k < columnsCountA; k++)
                    {
                        sum += (dynamic) matrixA[i, k]*matrixB[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }
    }
}
