using System;
using LearnAlgorithm.MatrixMultiplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LearnAlgorithm.Test.MatrixMultiplication
{
    [TestClass]
    public class StrassenMethodTest
    {
        IMatrixMuliplier<Double> _multiplier = new StrassenMethod<Double>();

        private Double[,] _matrixA = new Double[,]
        {
            {3, -2, 5, 2},
            {4, 3, 22, 5},
            {1, 8, 3, 9},
            {73, 2, 1, 51}
        };

        private Double[,] _matrixB = new Double[,]
        {
            {9, 3, -5, 3},
            {-2, 1, 4, 10},
            {5, 0, 25, 71},
            {7, 12, -3, 4}
        };

        [TestMethod]
        public void Multiply()
        {
            var result = _multiplier.Multiply(_matrixA, _matrixB);

            Assert.AreEqual(result[0, 0], 70);
            Assert.AreEqual(result[0, 1], 31);
            Assert.AreEqual(result[0, 2], 96);
            Assert.AreEqual(result[0, 3], 352);
            Assert.AreEqual(result[1, 0], 175);
            Assert.AreEqual(result[1, 1], 75);
            Assert.AreEqual(result[1, 2], 527);
            Assert.AreEqual(result[1, 3], 1624);
            Assert.AreEqual(result[2, 0], 71);
            Assert.AreEqual(result[2, 1], 119);
            Assert.AreEqual(result[2, 2], 75);
            Assert.AreEqual(result[2, 3], 332);
            Assert.AreEqual(result[3, 0], 1015);
            Assert.AreEqual(result[3, 1], 833);
            Assert.AreEqual(result[3, 2], -485);
            Assert.AreEqual(result[3, 3], 514);
        }
    }
}
