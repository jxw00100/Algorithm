using System;
using System.Text.RegularExpressions;

namespace LearnAlgorithm.MatrixMultiplication
{
    /*
     * Θ(n3) no faster than straight forward way.
     */
    public class SquareMatrixRecursive<T>: IMatrixMuliplier<T> where T: struct 
    {
        public T[,] Multiply(T[,] matrixA, T[,] matrixB)
        {
            Int32 rowsCountA = matrixA.GetLength(0),
                rowsCountB = matrixB.GetLength(0),
                columnsCountA = matrixA.GetLength(1),
                columnsCountB = matrixB.GetLength(1);

            if (columnsCountA != rowsCountB) throw new ArgumentException("Matrix size between A and B are not match");
            if (rowsCountA != columnsCountA || rowsCountB != columnsCountB) throw new ArgumentException("Matrix is not a square.");
            
            var logarithm = Math.Log(rowsCountA, 2);
            if (logarithm <= 0 || logarithm != Math.Floor(logarithm)) throw new ArgumentException("Matrix size is not positive power of 2");

            return RecursivelyCalculate(ref matrixA, ref matrixB,
                                 new MatrixIndices(new IndexBound(0, rowsCountA - 1), new IndexBound(0, rowsCountA - 1)),
                                 new MatrixIndices(new IndexBound(0, rowsCountA - 1), new IndexBound(0, rowsCountA - 1)));
        }

        private T[,] RecursivelyCalculate(ref T[,] matrixA, 
                                          ref T[,] matrixB,
                                          MatrixIndices indicesA,
                                          MatrixIndices indicesB)
        {
            var length = indicesA.RowIndices.GetLength();
            T[,] matrixC = new T[length, length];

            if (indicesA.HasOnlyOneElement() && indicesB.HasOnlyOneElement())
            {
                //C11 = A11 * B11
                matrixC[0, 0] = (dynamic) matrixA[indicesA.RowIndices.Lower, indicesA.ColumnIndices.Lower]*
                               matrixB[indicesB.RowIndices.Lower, indicesB.ColumnIndices.Lower];
            }
            else
            {
                Int32 middleRowIndexA =
                    (Int32) Math.Floor((indicesA.RowIndices.Lower + indicesA.RowIndices.Upper)/2.0),
                    middleColumnIndexA =
                        (Int32) Math.Floor((indicesA.ColumnIndices.Lower + indicesA.ColumnIndices.Upper)/2.0),
                    middleRowIndexB =
                        (Int32) Math.Floor((indicesB.RowIndices.Lower + indicesB.RowIndices.Upper)/2.0),
                    middleColumnIndexB =
                        (Int32) Math.Floor((indicesB.ColumnIndices.Lower + indicesB.ColumnIndices.Upper)/2.0),
                    middleIndexC = (Int32) Math.Floor((length - 1)/2.0);

                #region Sub Matrix Bound Indices
                //A11 index bound
                var indicesA11 = new MatrixIndices(
                    new IndexBound(indicesA.RowIndices.Lower, middleRowIndexA),
                    new IndexBound(indicesA.ColumnIndices.Lower, middleColumnIndexA));
                //A12 index bound
                var indicesA12 = new MatrixIndices(
                    new IndexBound(indicesA.RowIndices.Lower, middleRowIndexA),
                    new IndexBound(middleColumnIndexA + 1, indicesA.ColumnIndices.Upper));
                //A21 index bound
                var indicesA21 = new MatrixIndices(
                    new IndexBound(middleRowIndexA + 1, indicesA.RowIndices.Upper),
                    new IndexBound(indicesA.ColumnIndices.Lower, middleColumnIndexA));
                //A22 index bound
                var indicesA22 = new MatrixIndices(
                    new IndexBound(middleRowIndexA + 1, indicesA.RowIndices.Upper),
                    new IndexBound(middleColumnIndexA + 1, indicesA.ColumnIndices.Upper));
                //B11 index bound
                var indicesB11 = new MatrixIndices(
                    new IndexBound(indicesB.RowIndices.Lower, middleRowIndexB),
                    new IndexBound(indicesB.ColumnIndices.Lower, middleColumnIndexB));
                //B12 index bound
                var indicesB12 = new MatrixIndices(
                    new IndexBound(indicesB.RowIndices.Lower, middleRowIndexB),
                    new IndexBound(middleColumnIndexB + 1, indicesB.ColumnIndices.Upper));
                //B21 index bound
                var indicesB21 = new MatrixIndices(
                    new IndexBound(middleRowIndexB + 1, indicesB.RowIndices.Upper),
                    new IndexBound(indicesB.ColumnIndices.Lower, middleColumnIndexB));
                //B22 index bound
                var indicesB22 = new MatrixIndices(
                    new IndexBound(middleRowIndexB + 1, indicesB.RowIndices.Upper),
                    new IndexBound(middleColumnIndexB + 1, indicesB.ColumnIndices.Upper));
                //C11 index bound
                var indicesC11 = new MatrixIndices(
                    new IndexBound(0, middleIndexC),
                    new IndexBound(0, middleIndexC));
                //C12 index bound
                var indicesC12 = new MatrixIndices(
                    new IndexBound(0, middleIndexC),
                    new IndexBound(middleIndexC + 1, length - 1));
                //C21 index bound
                var indicesC21 = new MatrixIndices(
                    new IndexBound(middleIndexC + 1, length - 1),
                    new IndexBound(0, middleIndexC));
                //C22 index bound
                var indicesC22 = new MatrixIndices(
                    new IndexBound(middleIndexC + 1, length - 1),
                    new IndexBound(middleIndexC + 1, length - 1));
                #endregion
                
                var matrixA11B11 = RecursivelyCalculate(ref matrixA, ref matrixB, indicesA11, indicesB11);
                var matrixA12B21 = RecursivelyCalculate(ref matrixA, ref matrixB, indicesA12, indicesB21);
                var matrixA11B12 = RecursivelyCalculate(ref matrixA, ref matrixB, indicesA11, indicesB12);
                var matrixA12B22 = RecursivelyCalculate(ref matrixA, ref matrixB, indicesA12, indicesB22);
                var matrixA21B11 = RecursivelyCalculate(ref matrixA, ref matrixB, indicesA21, indicesB11);
                var matrixA22B21 = RecursivelyCalculate(ref matrixA, ref matrixB, indicesA22, indicesB21);
                var matrixA21B12 = RecursivelyCalculate(ref matrixA, ref matrixB, indicesA21, indicesB12);
                var matrixA22B22 = RecursivelyCalculate(ref matrixA, ref matrixB, indicesA22, indicesB22);

                //C11 = A11*B11 + A12*B21
                MatrixUtil.Add(ref matrixA11B11, ref matrixA12B21, ref matrixC, indicesC11);
                //C12 = A11*B12 + A12*B22
                MatrixUtil.Add(ref matrixA11B12, ref matrixA12B22, ref matrixC, indicesC12);
                //C21 = A21*B11 + A22*B21
                MatrixUtil.Add(ref matrixA21B11, ref matrixA22B21, ref matrixC, indicesC21);
                //C22 = A21*B12 + A22*B22
                MatrixUtil.Add(ref matrixA21B12, ref matrixA22B22, ref matrixC, indicesC22);
            }

            return matrixC;
        }
    }
}
