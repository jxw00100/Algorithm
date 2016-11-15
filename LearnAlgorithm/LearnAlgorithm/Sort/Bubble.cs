using System;
using System.Collections.Generic;
using LearnAlgorithm.Assist;

namespace LearnAlgorithm.Sort
{
    public class Bubble<T>:ISorter<T> where T : IComparable<T>
    {
        public IList<T> Sort(IList<T> list)
        {
            if (list == null) throw new ArgumentNullException();
            IList<T> newList = list.CopyCollection();

            for (int i = 0; i < newList.Count - 1; i++)
            {
                for (int j = newList.Count - 1; j > i; j--)
                {
                    if (newList[j].CompareTo(newList[j - 1]) < 0)
                    {
                        T temp = newList[j];
                        newList[j] = newList[j - 1];
                        newList[j - 1] = temp;
                    }
                }
            }
            
            return newList;
        }
    }
}
