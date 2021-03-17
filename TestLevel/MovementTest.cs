using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Vupa;

namespace TestLevel
{


    [TestClass]
    public class MovementTest
    {

        [TestMethod]
        public void TestMoveRight()
        {

            Point location = new Point(5, 5);

            // Arrange
            Player player = new Player(location);


            Point expectedLocation = new Point(6 * 100, 5 * 100);

            // Act
            Point actualLocation = player.Move(Keys.NumPad6);

            // Assert
            Assert.AreEqual(expectedLocation, actualLocation);

        }

        [TestMethod]
        public void TestMoveLeft()
        {

            Point location = new Point(5, 5);

            // Arrange
            Player player = new Player(location);


            Point expectedLocation = new Point(4 * 100, 5 * 100);

            // Act
            Point actualLocation = player.Move(Keys.NumPad4);

            // Assert
            Assert.AreEqual(expectedLocation, actualLocation);

        }


        [TestMethod]
        public void TestMoveUp()
        {

            Point location = new Point(5, 5);

            // Arrange
            Player player = new Player(location);


            Point expectedLocation = new Point(5 * 100, 4 * 100);

            // Act
            Point actualLocation = player.Move(Keys.NumPad8);

            // Assert
            Assert.AreEqual(expectedLocation, actualLocation);

        }

        [TestMethod]
        public void TestMoveDown()
        {

            Point location = new Point(5, 5);

            // Arrange
            Player player = new Player(location);


            Point expectedLocation = new Point(5 * 100, 6 * 100);

            // Act
            Point actualLocation = player.Move(Keys.NumPad2);

            // Assert
            Assert.AreEqual(expectedLocation, actualLocation);

        }

        [TestMethod]
        public void TestMoveDiagonalRight()
        {

            Point location = new Point(5, 5);

            // Arrange
            Player player = new Player(location);


            Point expectedLocation = new Point(6 * 100, 4 * 100);

            // Act
            Point actualLocation = player.Move(Keys.NumPad9);

            // Assert
            Assert.AreEqual(expectedLocation, actualLocation);

        }

    }
}
