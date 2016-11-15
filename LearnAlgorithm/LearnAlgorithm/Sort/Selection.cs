using System;
using System.Collections.Generic;
using System.Globalization;
using LearnAlgorithm.Assist;

namespace LearnAlgorithm.Sort
{
    public class Selection<T>: ISorter<T> where T : IComparable<T>
    {
        public IList<T> Sort(IList<T> list)
        {
            if (list == null) throw new ArgumentNullException();
            IList<T> newList = list.CopyCollection();

            for (Int32 i = 0; i < list.Count - 1; i++)
            {
                Int32 minIndex = i;
                for (Int32 j = i + 1; j < list.Count; j++)
                {
                    if (newList[j].CompareTo(newList[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    T temp = newList[i];
                    newList[i] = newList[minIndex];
                    newList[minIndex] = temp;
                }
            }

            return newList;
        }
    }
}
