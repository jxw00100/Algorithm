using System;
using System.Collections.Generic;

namespace LearnAlgorithm.Sort
{
    public class Insertion<T>: ISorter<T> where T : IComparable<T>
    {
        public IList<T> Sort(IList<T> list)
        {
            for (var i = 1; i < list.Count; i++)
            {
                var key = list[i];
                var j = i - 1;

                for (;j > 0 && list[j].CompareTo(key) > 0; j--)
                {
                    list[j + 1] = list[j];
                    j--;
                }

                list[j] = key;
            }
            return list;
        }
    }
}
