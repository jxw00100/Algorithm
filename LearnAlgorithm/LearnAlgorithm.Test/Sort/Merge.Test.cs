using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LearnAlgorithm;

namespace LearnAlgorithm.Test
{
	[TestClass]
    public class MergeTest
    {
        private Int32[] _int32List = new[] { 7, 2, 33, 71, 256, 1, 31, 2, -1, 0, -22 };
        private Int32[] _emptyList = new Int32[] {};
        private Sort.ISorter<Int32> _sorter = new Sort.Merge<Int32>();

		[TestMethod]
        public void NormalSort()
        {
            var list = _sorter.Sort(_int32List);

            Assert.AreEqual(list[0], -22);
            Assert.AreEqual(list[10], 256);
            Assert.AreEqual(list.Count, 11);
        }

        [TestMethod]
        public void EmptySort()
        {
            var list = _sorter.Sort(_emptyList);
            Assert.AreEqual(list.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArgument()
        {
            _sorter.Sort(null);
        }
    }
}
