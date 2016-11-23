using System;
using System.Reflection;

namespace LearnAlgorithm.MatrixMultiplication
{
    public static class MatrixUtil
    {
        public static void Add<T>(ref T[,] matrixA, ref T[,] matrixB, ref T[,] result, MatrixIndices indicesResult)
        {
            Calc(ref matrixA, ref matrixB, ref result, indicesResult, true);
        }

        public static void Add<T>(ref T[,] matrixA, ref T[,] matrixB, ref T[,] result, MatrixIndices indicesA,
            MatrixIndices indicesB, MatrixIndices indicesResult)
        {
            CalcPartion(ref matrixA, ref matrixB, ref result, indicesA, indicesB, indicesResult, true);
        }

        public static T[,] Add<T>(ref T[,] matrixA, ref T[,] matrixB)
        {
            return CalcReturn(ref matrixA, ref matrixB, true);
        }

        public static T[,] Add<T>(ref T[,] matrixA, ref T[,] matrixB, MatrixIndices indicesA,
            MatrixIndices indicesB)
        {
            return CalcReturnPartition(ref matrixA, ref matrixB, indicesA, indicesB, true);
        }

        public static void Substract<T>(ref T[,] matrixA, ref T[,] matrixB, ref T[,] result, MatrixIndices indicesResult)
        {
            Calc(ref matrixA, ref matrixB, ref result, indicesResult, false);
        }

        public static void Substract<T>(ref T[,] matrixA, ref T[,] matrixB, ref T[,] result, MatrixIndices indicesA,
            MatrixIndices indicesB, MatrixIndices indicesResult)
        {
            CalcPartion(ref matrixA, ref matrixB, ref result, indicesA, indicesB, indicesResult, false);
        }

        public static T[,] Substract<T>(ref T[,] matrixA, ref T[,] matrixB)
        {
            return CalcReturn(ref matrixA, ref matrixB, false);
        }

        public static T[,] Substract<T>(ref T[,] matrixA, ref T[,] matrixB, MatrixIndices indicesA,
            MatrixIndices indicesB)
        {
            return CalcReturnPartition(ref matrixA, ref matrixB, indicesA, indicesB, false);
        }

        private static void Calc<T>(ref T[,] matrixA, ref T[,] matrixB, ref T[,] result, MatrixIndices indicesResult, Boolean isAdd)
        {
            if(matrixA.GetLength(0) != matrixB.GetLength(0) || matrixA.GetLength(1) != matrixB.GetLength(1) || 
                matrixA.GetLength(0) != indicesResult.RowIndices.GetLength() || matrixA.GetLength(1) != indicesResult.ColumnIndices.GetLength())
                throw new ArgumentException("Matches size between A, B and Result are not match.");

            Int32 rowLength = matrixA.GetLength(0), columnLength = matrixA.GetLength(1);

            for (var i = 0; i < rowLength; i++)
            {
                for (var j = 0; j < columnLength; j++)
                {
                    result[indicesResult.RowIndices.Lower + i, indicesResult.ColumnIndices.Lower + j] =
                        isAdd ? (dynamic)matrixA[i, j] + matrixB[i, j] : (dynamic)matrixA[i, j] - matrixB[i, j];
                }
            }
        }

        private static void CalcPartion<T>(ref T[,] matrixA, ref T[,] matrixB, ref T[,] result, MatrixIndices indicesA,
            MatrixIndices indicesB, MatrixIndices indicesResult, Boolean isAdd)
        {
            if (indicesA.RowIndices.GetLength() != indicesB.RowIndices.GetLength() || indicesA.ColumnIndices.GetLength() != indicesB.ColumnIndices.GetLength() ||
                indicesA.RowIndices.GetLength() != indicesResult.RowIndices.GetLength() || indicesA.ColumnIndices.GetLength() != indicesResult.ColumnIndices.GetLength())
                throw new ArgumentException("Matches size between A, B and Result are not match.");

            Int32 rowLength = indicesA.RowIndices.GetLength(), columnLength = indicesA.ColumnIndices.GetLength();

            for (var i = 0; i < rowLength; i++)
            {
                for (var j = 0; j < columnLength; j++)
                {
                    result[indicesResult.RowIndices.Lower + i, indicesResult.ColumnIndices.Lower + j] =
                        isAdd
                            ? (dynamic) matrixA[indicesA.RowIndices.Lower + i, indicesA.ColumnIndices.Lower + j] +
                              matrixB[indicesB.RowIndices.Lower + i, indicesB.ColumnIndices.Lower + j]
                            : (dynamic) matrixA[indicesA.RowIndices.Lower + i, indicesA.ColumnIndices.Lower + j] -
                              matrixB[indicesB.RowIndices.Lower + i, indicesB.ColumnIndices.Lower + j];
                }
            }
        }

        private static T[,] CalcReturn<T>(ref T[,] matrixA, ref T[,] matrixB, Boolean isAdd)
        {
            if (matrixA.GetLength(0) != matrixB.GetLength(0) || matrixA.GetLength(1) != matrixB.GetLength(1))
                throw new ArgumentException("Matches size between A, B are not match.");

            Int32 rowLength = matrixA.GetLength(0), columnLength = matrixA.GetLength(1);
            T[,] result = new T[rowLength, columnLength];

            for (var i = 0; i < rowLength; i++)
            {
                for (var j = 0; j < columnLength; j++)
                {
                    result[i, j] = isAdd ? (dynamic) matrixA[i, j] + matrixB[i, j] : (dynamic) matrixA[i, j] - matrixB[i, j];
                }
            }

            return result;
        }

        private static T[,] CalcReturnPartition<T>(ref T[,] matrixA, ref T[,] matrixB, MatrixIndices indicesA,
            MatrixIndices indicesB, Boolean isAdd)
        {
            if (indicesA.RowIndices.GetLength() != indicesB.RowIndices.GetLength() || indicesA.ColumnIndices.GetLength() != indicesB.ColumnIndices.GetLength())
                throw new ArgumentException("Matches size between A, B are not match.");

            Int32 rowLength = indicesA.RowIndices.GetLength(), columnLength = indicesA.ColumnIndices.GetLength();

            T[,] result = new T[rowLength, columnLength];

            for (var i = 0; i < rowLength; i++)
            {
                for (var j = 0; j < columnLength; j++)
                {
                    result[i, j] = isAdd
                            ? (dynamic)matrixA[indicesA.RowIndices.Lower + i, indicesA.ColumnIndices.Lower + j] +
                              matrixB[indicesB.RowIndices.Lower + i, indicesB.ColumnIndices.Lower + j]
                            : (dynamic)matrixA[indicesA.RowIndices.Lower + i, indicesA.ColumnIndices.Lower + j] -
                              matrixB[indicesB.RowIndices.Lower + i, indicesB.ColumnIndices.Lower + j];
                }
            }

            return result;
        }

        public static void Copy<T>(ref T[,] source, ref T[,] target, MatrixIndices indicesTarget)
        {
            Int32 rowLength = source.GetLength(0), columnLength = source.GetLength(1);
            for (var i = 0; i < rowLength; i++)
            {
                for (var j = 0; j < columnLength; j++)
                {
                    target[indicesTarget.RowIndices.Lower + i, indicesTarget.ColumnIndices.Lower + j] = source[i, j];
                }
            }
        }
    }
}
