using System;
using System.Collections.Generic;
using LearnAlgorithm.Assist;

namespace LearnAlgorithm.Sort
{
    /*
     * Θ(nlgn)
     */
    public class Merge<T>: ISorter<T> where T: IComparable<T>
    {
        public IList<T> Sort(IList<T> list)
        {
             IList<T> newList = list.CopyCollection();
             Divide(newList, 0, newList.Count - 1);
             return newList;
        }

        private void Divide(IList<T> list, Int32 lowerBoundIndex, Int32 upperBoundIndex)
        {
            if(lowerBoundIndex < upperBoundIndex){
                Int32 dividedIndex = lowerBoundIndex + (Int32) Math.Floor((Double)(upperBoundIndex - lowerBoundIndex) / 2);
                Divide(list, lowerBoundIndex, dividedIndex);
                Divide(list, dividedIndex + 1, upperBoundIndex);
                MergeSort(list, lowerBoundIndex, dividedIndex, upperBoundIndex);
            }
        }

        private void MergeSort(IList<T> list, Int32 lowerBoundIndex, Int32 dividedIndex, Int32 upperBoundIndex)
        {
            Int32 leftCount = dividedIndex - lowerBoundIndex + 1;
            Int32 rightCount = upperBoundIndex - dividedIndex;
            T[] leftArray = CopyArrayFrom(list, lowerBoundIndex, dividedIndex);
            T[] rightArray = CopyArrayFrom(list, dividedIndex + 1, upperBoundIndex);

            Int32 mainCursor, leftCursor = 0, rightCursor = 0;
            for (mainCursor = lowerBoundIndex; mainCursor <= upperBoundIndex; mainCursor++)
            {
                if (leftCursor < leftCount 
                    && (rightCursor >= rightCount 
                        || leftArray[leftCursor].CompareTo(rightArray[rightCursor]) <= 0))
                {
                    list[mainCursor] = leftArray[leftCursor];
                    leftCursor++;
                }
                else if(rightCursor < rightCount)
                {
                    list[mainCursor] = rightArray[rightCursor];
                    rightCursor++;
                }
            }

        }

        private T[] CopyArrayFrom(IList<T> list, Int32 startIndex, Int32 endIndex)
        {
            Int32 length = endIndex - startIndex + 1;
            T[] array = new T[length];
            for (var i = 0; i < length; i++)
            {
                array[i] = list[startIndex + i];
            }
            return array;
        }
    }
}
