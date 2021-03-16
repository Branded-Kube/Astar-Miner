using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using System;
using Vupa;

namespace TestLevel
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSetStart1()
        {
            //Arrange
            Level level = new Level(1);
            Point expected = new Point(0, 0);
            //Act
            Point actual = level.SetStart();
            //Assert
            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod]
        public void TestSetStart2()
        {
            //Arrange
            Level level = new Level(2);
            Point expected = new Point(9, 7);
            //Act
            Point actual = level.SetStart();
            //Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestSetStartWrongValue()
        {
            //Arrange
            Level level = new Level(3);
            Point expected = new Point(1, 1);
            //Act
            Point actual = level.SetStart();
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSetGoal1()
        {
            //Arrange
            Level level = new Level(1);
            Point expected = new Point(9, 7);

            //Act
            Point actual = level.SetGoal();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestZero()
        {
            //Arrange
            Level level = new Level(1);
            level.SetStart();
        }
    }
}
