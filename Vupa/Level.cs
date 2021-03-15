using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Vupa
{
    class Level
    {
        Point startLoc;
        public Point goalLoc;
        int lvlNumber;


        public Level(int lvlnumber)
        {
            this.lvlNumber = lvlnumber;

        }

        public Point SetStart(Point start)
        {
            if (lvlNumber == 1)
            {
                start = new Point(0, 0);

            }
            if (lvlNumber == 2)
            {
                start = new Point(2, 2);
            }
            return start;
        }
        public Point SetGoal(Point goal)
        {
            if (lvlNumber == 1)
            {
                goal = new Point(9, 9);
            }
            if (lvlNumber == 2)
            {
                goal = new Point(2, 2);
            }
            return goal;
        }

    }
}
