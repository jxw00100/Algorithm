using System;
using System.Collections.Generic;

namespace LearnAlgorithm.MaxSumSubarray
{
    /*
     * Θ(n lgn)
     */
    public class DivideConquer<T>: IMaxSumSubarray<T> where T: IComparable<T>
    {
        public SubarraySumIndication<T> GetSubarray(IList<T> array)
        {
            if (array == null || array.Count == 0) throw new ArgumentException();

            return DivideThenGetMaxSumSubarray(array, 0, array.Count - 1);
        }

        private SubarraySumIndication<T> DivideThenGetMaxSumSubarray(IList<T> array, Int32 startIndex, Int32 endIndex)
        {
            if (startIndex == endIndex)
            {
                return new SubarraySumIndication<T>(startIndex, endIndex, array[startIndex]);
            }
            else
            {
                var middleIndex = (Int32) Math.Floor((Double) (endIndex + startIndex)/2);
                var leftMaxSumInfo = DivideThenGetMaxSumSubarray(array, startIndex, middleIndex);
                var rightMaxSumInfo = DivideThenGetMaxSumSubarray(array, middleIndex + 1, endIndex);
                var crossingMaxSumInfo = GetCrossingMaxSumSubarray(array, startIndex, middleIndex, endIndex);

                if (leftMaxSumInfo.Sum.CompareTo(rightMaxSumInfo.Sum) >= 0 && leftMaxSumInfo.Sum.CompareTo(crossingMaxSumInfo.Sum) >= 0)
                {
                    return leftMaxSumInfo;
                }
                else if (rightMaxSumInfo.Sum.CompareTo(leftMaxSumInfo.Sum) >= 0 &&
                         rightMaxSumInfo.Sum.CompareTo(crossingMaxSumInfo.Sum) >= 0)
                {
                    return rightMaxSumInfo;
                }
                else
                {
                    return crossingMaxSumInfo;
                }
            }
        }

        private SubarraySumIndication<T> GetCrossingMaxSumSubarray(IList<T> array, Int32 startIndex, Int32 middleIndex,
            Int32 endIndex)
        {
            dynamic leftMaxSum = array[middleIndex];
            dynamic sum = leftMaxSum;
            Int32 leftIndex = middleIndex;
            for (var i = middleIndex - 1; i >= startIndex; i--)
            {
                sum += array[i];
                if (sum.CompareTo(leftMaxSum) > 0)
                {
                    leftMaxSum = sum;
                    leftIndex = i;
                }
            }

            dynamic rightMaxSum = array[middleIndex + 1];
            sum = rightMaxSum;
            Int32 rightIndex = middleIndex + 1;
            for (var i = middleIndex + 2; i <= endIndex; i++)
            {
                sum += array[i];
                if (sum.CompareTo(rightMaxSum) > 0)
                {
                    rightMaxSum = sum;
                    rightIndex = i;
                }
            }

            return new SubarraySumIndication<T>(leftIndex, rightIndex, leftMaxSum + rightMaxSum);
        }
    }
}
