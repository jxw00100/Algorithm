using System;
using System.Collections.Generic;

namespace LearnAlgorithm.MaxSumSubarray
{
    /*
     * Based on this fact:
     * 
     * Knowing a maximum subarray of A[1...j] , extend the answer to find a maximum subarray ending at index
     * j+1 by using the following observation: a maximum subarray of A[1 ...j+1]
     * is either a maximum subarray of A[1...j] or a subarray A[i...j+1], for some
     * 1 =< i < j+1. Determine a maximum subarray of the form A[i...j+1] in
     * constant time based on knowing a maximum subarray ending at index j.
     * 
     * Θ(n)
     */
    public class LinearRuntime<T> : IMaxSumSubarray<T> where T : IComparable<T> 
    {
        public SubarraySumIndication<T> GetSubarray(IList<T> array)
        {
            if (array == null || array.Count == 0) throw new ArgumentException();

            SubarraySumIndication<T> maxSumSubarrayOfFull = new SubarraySumIndication<T>(0, 0, array[0]);
            SubarraySumIndication<T> maxSumSubarrayEndingAtRight = new SubarraySumIndication<T>(0, 0, array[0]);

            dynamic fullMaxSum, endingAtRightSum, newElement;
            for (var i = 1; i < array.Count; i++)
            {
                fullMaxSum = maxSumSubarrayOfFull.Sum;
                endingAtRightSum = maxSumSubarrayEndingAtRight.Sum;
                newElement = array[i];

                if (maxSumSubarrayOfFull.EndIndex == i - 1)
                {
                    if (newElement >= 0)
                    {
                        fullMaxSum += newElement;
                        maxSumSubarrayOfFull = new SubarraySumIndication<T>(maxSumSubarrayOfFull.StartIndex, i,
                            fullMaxSum);
                        maxSumSubarrayEndingAtRight = new SubarraySumIndication<T>(maxSumSubarrayOfFull.StartIndex, i,
                            fullMaxSum);
                    }
                    else
                    {
                        endingAtRightSum += newElement;
                        maxSumSubarrayEndingAtRight = new SubarraySumIndication<T>(maxSumSubarrayEndingAtRight.StartIndex, i,
                            endingAtRightSum);
                    }
                }
                else
                {
                    if (newElement >= 0)
                    {
                        if (endingAtRightSum >= 0)
                        {
                            endingAtRightSum += newElement;
                            maxSumSubarrayEndingAtRight =
                                new SubarraySumIndication<T>(maxSumSubarrayEndingAtRight.StartIndex,
                                    i, endingAtRightSum);
                        }
                        else
                        {
                            endingAtRightSum = newElement;
                            maxSumSubarrayEndingAtRight = new SubarraySumIndication<T>(i, i, endingAtRightSum);
                        }

                        if (fullMaxSum < endingAtRightSum)
                        {
                            fullMaxSum = endingAtRightSum;
                            maxSumSubarrayOfFull = new SubarraySumIndication<T>(maxSumSubarrayEndingAtRight.StartIndex,
                                maxSumSubarrayEndingAtRight.EndIndex, endingAtRightSum);
                        }
                    }
                    else
                    {
                        endingAtRightSum += newElement;
                        maxSumSubarrayEndingAtRight =
                            new SubarraySumIndication<T>(maxSumSubarrayEndingAtRight.StartIndex, i, endingAtRightSum);
                    }
                }

            }
            return maxSumSubarrayOfFull;
        }
    }
}
