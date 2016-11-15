using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnAlgorithm;

namespace LearnAlgorithm.Test
{
    [TestClass]
    public class BubbleTest
    {
        private Int32[] _int32List = new[] { 7, 2, 33, 71, 256, 1, 31, 2, -1, 0, -22 };
        private Sort.ISorter<Int32> _sorter = new Sort.Selection<Int32>();

        [TestMethod]
        public void NormalSort()
        {
            var list = _sorter.Sort(_int32List);

            Assert.AreEqual(list[0], -22);
            Assert.AreEqual(list[10], 256);
            Assert.AreEqual(list.Count, 11);
        }
    }
}
