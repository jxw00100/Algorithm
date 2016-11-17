using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAlgorithm.MaxSumSubarray
{
    public struct SubarraySumIndication<T>
    {
        public readonly Int32 StartIndex;
        public readonly Int32 EndIndex;
        public readonly T Sum;

        public SubarraySumIndication(Int32 startIndex, Int32 endIndex, T sum)
        {
            if (startIndex < 0 || endIndex < 0 || endIndex < startIndex) throw new ArgumentException();

            StartIndex = startIndex;
            EndIndex = endIndex;
            Sum = sum;
        }
    }
}
