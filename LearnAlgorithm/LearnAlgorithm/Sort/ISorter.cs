using System;
using System.Collections.Generic;

namespace LearnAlgorithm.Sort
{
    public interface ISorter<T> where T : IComparable<T>
    {
        IList<T> Sort(IList<T> list);
    }
}
