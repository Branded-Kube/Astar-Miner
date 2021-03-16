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
        Point startLoc;
        public Point goalLoc;
        int lvlNumber;
        ContentManager content;

        public Level(int lvlnumber)
        {
            this.lvlNumber = lvlnumber;
            //SetWalls();
        }

        public Point SetStart()
        {
            if (lvlNumber == 1)
            {
                return new Point(0, 0);


            }
            if (lvlNumber == 2)
            {
                return new Point(9, 7);
            }

            return new Point();
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
