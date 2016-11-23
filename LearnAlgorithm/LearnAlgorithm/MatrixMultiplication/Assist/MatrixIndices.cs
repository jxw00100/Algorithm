using System;

namespace LearnAlgorithm.MatrixMultiplication
{
    public struct MatrixIndices
    {
        public readonly IndexBound RowIndices;
        public readonly IndexBound ColumnIndices;

        public MatrixIndices(IndexBound rowIndices, IndexBound columnIndices)
        {
            RowIndices = rowIndices;
            ColumnIndices = columnIndices;
        }

        public Boolean HasOnlyOneElement()
        {
            return RowIndices.BoundsAreSame() && ColumnIndices.BoundsAreSame();
        }
    }
}
