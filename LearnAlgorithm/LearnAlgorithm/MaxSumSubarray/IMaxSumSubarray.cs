using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LearnAlgorithm.MaxSumSubarray
{
    public interface IMaxSumSubarray<T> where T : IComparable<T>
    {
        SubarraySumIndication<T> GetSubarray(IList<T> array);
    }
}
