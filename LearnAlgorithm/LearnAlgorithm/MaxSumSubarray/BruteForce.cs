using System;
using System.Collections.Generic;

namespace LearnAlgorithm.MaxSumSubarray
{
    /*
     * Θ(n²)
     */
	public class BruteForce<T>: IMaxSumSubarray<T> where T: IComparable<T>
	{
	    public SubarraySumIndication<T> GetSubarray(IList<T> array)
	    {
	        if (array == null || array.Count == 0) throw new ArgumentException();

	        var arrayTotal = array.Count;
	        var sumCollectionTotal = arrayTotal*(arrayTotal - 1)/2;
            SubarraySumIndication<T>[] subarraySums = new SubarraySumIndication<T>[sumCollectionTotal];
	        var k = 0;
	        dynamic sum;
	        for (var i = 0; i < array.Count - 1; i++)
	        {
                sum = array[i];
	            for (var j = i + 1; j < array.Count; j++)
	            {
	                sum = sum + array[j];
                    subarraySums[k] = new SubarraySumIndication<T>(i, j, sum);
	                k++;
	            }
	        }

	        var maxSumIndex = 0;
            sum = subarraySums[0].Sum;
	        for (k = 0; k < sumCollectionTotal - 1; k++)
	        {
	            if (subarraySums[k + 1].Sum.CompareTo(sum) > 0)
	            {
                    maxSumIndex = k + 1;
	                sum = subarraySums[maxSumIndex].Sum;
	            }
	        }

	        return subarraySums[maxSumIndex];
	    }
	}
}
