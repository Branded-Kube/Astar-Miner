using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Vupa
{
    public class Level
    {
        int lvlNumber;

        public Level(int lvlnumber)
        {
            this.lvlNumber = lvlnumber;
            //SetWalls();
        }

        public Point SetStart()
        {
            if (lvlNumber == 1)
            {
                return new Point(1, 1);


            }
            if (lvlNumber == 2)
            {
                return new Point(9, 7);
            }

            return new Point();
        }
        public Point SetGoal()
        {
            if (lvlNumber == 1)
            {
                return new Point(9, 7);
            }
            if (lvlNumber == 2)
            {
                return new Point(9, 1);
            }
            return new Point(5, 5);
        }
        public void SetWalls()
        {
            if (lvlNumber == 1)
            {
                VisualManager.grid[11].WalkAble = false;

            }
            if (lvlNumber == 2)
            {
                VisualManager.grid[95].WalkAble = false;
                VisualManager.grid[94].WalkAble = false;
            }
        }


    }
}
