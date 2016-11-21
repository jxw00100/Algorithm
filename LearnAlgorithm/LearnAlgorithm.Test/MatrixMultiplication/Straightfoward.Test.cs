using System;
using System.ComponentModel;
using LearnAlgorithm.MatrixMultiplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LearnAlgorithm.Test.MatrixMultiplication
{
    [TestClass]
    public class StraightfowardTest
    {
        IMatrixMuliplier<Double> _multiplier =  new Straightfoward<Double>();

        private Double[,] _matrixA = new Double[,]
        {
            {3, -2, 5, 2, 1},
            {4, 3, 22, 5, 3},
            {1, 8, 3, 9, -3}
        };

        private Double[,] _matrixB = new Double[,]
        {
            {9, 3, -5, 3},
            {-2, 1, 4, 10},
            {5, 0, 25, 71},
            {7, 12, -3, 4},
            {2, 8, 99, 21}
        };

        [TestMethod]
        public void Multiply()
        {
            var result = _multiplier.Multiply(_matrixA, _matrixB);

            Assert.AreEqual(result[0, 0], 72);
            Assert.AreEqual(result[0, 1], 39);
            Assert.AreEqual(result[0, 2], 195);
            Assert.AreEqual(result[0, 3], 373);

            Assert.AreEqual(result[1, 0], 181);
            Assert.AreEqual(result[1, 1], 99);
            Assert.AreEqual(result[1, 2], 824);
            Assert.AreEqual(result[1, 3], 1687);

            Assert.AreEqual(result[2, 0], 65);
            Assert.AreEqual(result[2, 1], 95);
            Assert.AreEqual(result[2, 2], -222);
            Assert.AreEqual(result[2, 3], 269);
        }
    }
}
