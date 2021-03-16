using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Vupa;

namespace TestLevel
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSetStart()
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
        public void TestSetStartWrongValue()
        {
            //Arrange
            Level level = new Level(1);
            Point expected = new Point(1, 1);
            //Act
            Point actual = level.SetStart();
            //Assert
            Assert.AreNotEqual(expected, actual);
        }
    }
}
