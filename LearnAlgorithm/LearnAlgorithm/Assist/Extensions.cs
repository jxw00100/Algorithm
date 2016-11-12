using System.Collections.Generic;

namespace LearnAlgorithm.Assist
{
    public static class Extensions
    {
        public static IList<T> CopyCollection<T>(this IList<T> olist)
        {
            IList<T> list = new List<T>();
            foreach (T item in olist)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
