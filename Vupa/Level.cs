using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Vupa
{
    class Level
    {
        int lvlNumber;

        public Level(int lvlnumber)
        {
            this.lvlNumber = lvlnumber;
            //SetWalls();
        }

        public Point SetStart(Point start)
        {
            if (lvlNumber == 1)
            {
                start = new Point(1, 1);


            }
            if (lvlNumber == 2)
            {
                start = new Point(9, 7);
            }

            return start;
        }
        public Point SetGoal(Point goal)
        {
            if (lvlNumber == 1)
            {
                goal = new Point(9, 7);
            }
            if (lvlNumber == 2)
            {
                goal = new Point(9, 1);
            }
            return goal;
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
