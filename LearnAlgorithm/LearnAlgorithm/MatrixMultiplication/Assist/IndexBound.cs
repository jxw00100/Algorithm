using System;

namespace LearnAlgorithm.MatrixMultiplication
{
    public struct IndexBound
    {
        public readonly Int32 Lower;
        public readonly Int32 Upper;

        public IndexBound(Int32 lower, Int32 upper)
        {
            if (lower > upper) throw new ArgumentException("lower index is bigger than upper index.");
            Lower = lower;
            Upper = upper;
        }

        public Boolean BoundsAreSame()
        {
            return Lower == Upper;
        }

        public Int32 GetLength()
        {
            return Upper - Lower + 1;
        }
    }
}
