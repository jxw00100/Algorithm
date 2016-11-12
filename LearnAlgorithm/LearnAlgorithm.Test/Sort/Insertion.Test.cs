using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnAlgorithm;

namespace LearnAlgorithm.Test
{
    [TestClass]
    public class InsertionTest
    {
        Sort.Insertion<Int32> _sorterInt32 = new Sort.Insertion<Int32>();
        private Int32[] _int32List = new[] {7, 2, 33, 71, 256, 1, 31, 2, -1, 0, -22};
        private Int32[] _emptyList = new Int32[] { };
        private Int32[] _oneElementList = new[] { 98 };

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void NullArgument()
        {
            _sorterInt32.Sort(null);
        }

        [TestMethod]
        public void NormalSortForInt32()
        {
            var list = _sorterInt32.Sort(_int32List);

            Assert.AreEqual(-22, list[0]);
            Assert.AreEqual(256, list[10]);
        }

        [TestMethod]
        public void EmptySort()
        {
            var list = _sorterInt32.Sort(_emptyList);
            Assert.AreEqual(list.Count, 0);
        }

        [TestMethod]
        public void OneElementSort()
        {
            var list = _sorterInt32.Sort(_oneElementList);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], 98);
        }
    }
}
