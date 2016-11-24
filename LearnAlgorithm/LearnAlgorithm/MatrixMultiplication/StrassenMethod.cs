using System;

namespace LearnAlgorithm.MatrixMultiplication
{
    /*
     * Θ(n power of lg7)
     *  Faster than straight forward method 
     *  Also Faster than square matrix recursive method, because it only uses 7 recursive muliplications, eliminate 1 recusive muliplication.
     */
    public class StrassenMethod<T>:IMatrixMuliplier<T> where T: struct
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
                    (Int32)Math.Floor((indicesA.RowIndices.Lower + indicesA.RowIndices.Upper) / 2.0),
                    middleColumnIndexA =
                        (Int32)Math.Floor((indicesA.ColumnIndices.Lower + indicesA.ColumnIndices.Upper) / 2.0),
                    middleRowIndexB =
                        (Int32)Math.Floor((indicesB.RowIndices.Lower + indicesB.RowIndices.Upper) / 2.0),
                    middleColumnIndexB =
                        (Int32)Math.Floor((indicesB.ColumnIndices.Lower + indicesB.ColumnIndices.Upper) / 2.0),
                    middleIndexC = (Int32)Math.Floor((length - 1) / 2.0);

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

                #region Create 10 Assistant Matrixes S1 - S10
                //S1 = B12 - B22
                var S1 = MatrixUtil.Substract(ref matrixB, ref matrixB, indicesB12, indicesB22);
                //S2 = A11 + A12
                var S2 = MatrixUtil.Add(ref matrixA, ref matrixA, indicesA11, indicesA12);
                //S3 = A21 + A22
                var S3 = MatrixUtil.Add(ref matrixA, ref matrixA, indicesA21, indicesA22);
                //S4 = B21 - B11
                var S4 = MatrixUtil.Substract(ref matrixB, ref matrixB, indicesB21, indicesB11);
                //S5 = A11 + A22
                var S5 = MatrixUtil.Add(ref matrixA, ref matrixA, indicesA11, indicesA22);
                //S6 = B11 + B22
                var S6 = MatrixUtil.Add(ref matrixB, ref matrixB, indicesB11, indicesB22);
                //S7 = A12 - A22
                var S7 = MatrixUtil.Substract(ref matrixA, ref matrixA, indicesA12, indicesA22);
                //S6 = B21 + B22
                var S8 = MatrixUtil.Add(ref matrixB, ref matrixB, indicesB21, indicesB22);
                //S9 = A11 - A21
                var S9 = MatrixUtil.Substract(ref matrixA, ref matrixA, indicesA11, indicesA21);
                //S10 = B11 + B12
                var S10 = MatrixUtil.Add(ref matrixB, ref matrixB, indicesB11, indicesB12);
                #endregion

                #region Create 7 Further Assistant Matrixes P1 - P7
                //P1 = A11 * S1 = A11 * B12 - A11 * B22
                var P1 = RecursivelyCalculate(ref matrixA, ref S1, 
                    indicesA11,
                    new MatrixIndices(new IndexBound(0, S1.GetLength(0) - 1), new IndexBound(0, S1.GetLength(1) - 1)));
                //P2 = S2 * B22 = A11 * B22 + A12 * B22
                var P2 = RecursivelyCalculate(ref S2, ref matrixB,
                    new MatrixIndices(new IndexBound(0, S2.GetLength(0) - 1), new IndexBound(0, S2.GetLength(1) - 1)),
                    indicesB22);
                //P3 = S3 * B11 = A21 * B11 + A22 * B11
                var P3 = RecursivelyCalculate(ref S3, ref matrixB,
                    new MatrixIndices(new IndexBound(0, S3.GetLength(0) - 1), new IndexBound(0, S3.GetLength(1) - 1)),
                    indicesB11);
                //P4 = A22 * S4 = A22 * B21 - A22 * B11
                var P4 = RecursivelyCalculate(ref matrixA, ref S4, 
                    indicesA22,
                    new MatrixIndices(new IndexBound(0, S4.GetLength(0) - 1), new IndexBound(0, S4.GetLength(1) - 1)));
                //P5 = S5 * S6 = A11 * B11 + A11 * B22 + A22 * B11 + A22 * B22
                var P5 = RecursivelyCalculate(ref S5, ref S6,
                    new MatrixIndices(new IndexBound(0, S5.GetLength(0) - 1), new IndexBound(0, S5.GetLength(1) - 1)),
                    new MatrixIndices(new IndexBound(0, S6.GetLength(0) - 1), new IndexBound(0, S6.GetLength(1) - 1)));
                //P6 = S7 * S8 = A12 * B21 + A12 * B22 - A22 * B21 - A22 * B22
                var P6 = RecursivelyCalculate(ref S7, ref S8,
                    new MatrixIndices(new IndexBound(0, S7.GetLength(0) - 1), new IndexBound(0, S7.GetLength(1) - 1)),
                    new MatrixIndices(new IndexBound(0, S8.GetLength(0) - 1), new IndexBound(0, S8.GetLength(1) - 1)));
                //P7 = S9 * S10 = A11 * B11 + A11 * B12 - A21 * B11 - A21 * B12
                var P7 = RecursivelyCalculate(ref S9, ref S10,
                    new MatrixIndices(new IndexBound(0, S9.GetLength(0) - 1), new IndexBound(0, S9.GetLength(1) - 1)),
                    new MatrixIndices(new IndexBound(0, S10.GetLength(0) - 1), new IndexBound(0, S10.GetLength(1) - 1)));
                #endregion

                #region Calculate the four subparts of matrix C
                /*
                 * C11 = P5 + P4 - P2 + P6
                 * 
                 * As follows:
                 *   P5:   A11 * B11 + A11 * B22 + A22 * B11 + A22 * B22
                 * + P4:                         - A22 * B11             + A22 * B21 
                 * - P2:             - A11 * B22                                     - A12 * B22
                 * + P6:                                     - A22 * B22 - A22 * B21 + A12 * B22 + A12 * B21   
                 * ------------------------------------------------------------------------------------------
                 * =       A11 * B11                                                             + A12 * B21
                 */
                var tempresult = MatrixUtil.Add(ref P5, ref P4);
                tempresult = MatrixUtil.Substract(ref tempresult, ref P2);
                tempresult = MatrixUtil.Add(ref tempresult, ref P6);
                MatrixUtil.Copy(ref tempresult, ref matrixC, indicesC11);
                
                /*
                 * C12 = P1 + P2
                 * 
                 * As follows:
                 *   P1:   A11 * B12 - A11 * B22
                 * + P2:             + A11 * B22 + A12 * B22 
                 * ------------------------------------------------------------------------------------------
                 * =       A11 * B12             + A12 * B22
                 */
                tempresult = MatrixUtil.Add(ref P1, ref P2);
                MatrixUtil.Copy(ref tempresult, ref matrixC, indicesC12);

                /*
                 * C21 = P3 + P4
                 * 
                 * As follows:
                 *   P3:   A21 * B11 - A22 * B11
                 * + P4:             + A22 * B11 + A22 * B21 
                 * ------------------------------------------------------------------------------------------
                 * =       A21 * B11             + A22 * B21
                 */
                tempresult = MatrixUtil.Add(ref P3, ref P4);
                MatrixUtil.Copy(ref tempresult, ref matrixC, indicesC21);

                /*
                 * C22 = P5 + P1 - P3 - P7
                 * 
                 * As follows:
                 *   P5:   A11 * B11 + A11 * B22 + A22 * B11 + A22 * B22
                 * + P1:             - A11 * B22                         + A11 * B12 
                 * - P3:                         - A22 * B11                         - A21 * B11
                 * - P7: - A11 * B11                                     - A11 * B12 + A21 * B11 + A21 * B12   
                 * ------------------------------------------------------------------------------------------
                 * =                                           A22 * B22                         + A21 * B12
                 */
                tempresult = MatrixUtil.Add(ref P5, ref P1);
                tempresult = MatrixUtil.Substract(ref tempresult, ref P3);
                tempresult = MatrixUtil.Substract(ref tempresult, ref P7);
                MatrixUtil.Copy(ref tempresult, ref matrixC, indicesC22);

                #endregion
            }
            return matrixC;
        }

    }
}
