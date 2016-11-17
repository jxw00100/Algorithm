using System;
using LearnAlgorithm.MaxSumSubarray;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LearnAlgorithm.Test.MaxSumSubarray
{
    [TestClass]
    public class LinearRuntimeTest
    {
        private IMaxSumSubarray<Int32> _runner = new LinearRuntime<Int32>();
        private Int32[] _fullArray = new[] { 13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7 };

        [TestMethod]
        public void GetMaxSubarray()
        {
            SubarraySumIndication<Int32> subarrayInfo = _runner.GetSubarray(_fullArray);

            Assert.AreEqual(subarrayInfo.StartIndex, 7);
            Assert.AreEqual(subarrayInfo.EndIndex, 10);
            Assert.AreEqual(subarrayInfo.Sum, 43);
        }
    }
}
