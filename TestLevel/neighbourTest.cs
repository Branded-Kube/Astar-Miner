using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Vupa;
using Microsoft.Xna.Framework;

namespace TestLevel
{
    [TestClass]
    public class neighbourTest
    {
     
            [TestMethod]
            public void TestStartNeighbour()
            {
                //Arrange
                Level level = new Level(1);
                Point tmp = new Point(0, 0);
                AStar aStar = new AStar();


            Node currentNode = new Node(tmp);
               currentNode = aStar.open.Find(x => x.Position == tmp);

                int actual = 1;



            //Act
            int expected = currentNode.G;

            //Assert
            Assert.AreEqual(expected, actual);

            }

        }
    
}
