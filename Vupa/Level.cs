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
        #region Fields & Properties
        public int LvlNumber { get; set; }
        public List<Cell> Notwalkables { get; set; }

        #endregion

        #region Constructor
        public Level(int lvlnumber)
        {
            this.LvlNumber = lvlnumber;
            Notwalkables = new List<Cell>();
        }
        #endregion

        #region Methods
        public Point SetStart()
        {
            if (LvlNumber == 1)
            {
                return new Point(1, 1);
            }
            if (LvlNumber == 2)
            {
                return new Point(9, 7);
            }
            if (LvlNumber == 3)
            {
                return new Point(9, 1);
            }
            if (LvlNumber == 4)
            {
                return new Point(2, 10);
            }
            else
            {
                return new Point();

            }

        }
        public Point SetGoal()
        {
            if (LvlNumber == 1)
            {
                return new Point(9, 7);
            }
            if (LvlNumber == 2)
            {
                return new Point(9, 1);
            }
            if (LvlNumber == 3)
            {
                return new Point(2, 10);
            }
            if (LvlNumber == 4)
            {
                return new Point(8, 3);
            }
            else
            {
                return new Point(5, 5);

            }
        }
        public void SetWalls()
        {
            Notwalkables.Clear();
            foreach (Cell cell in Game1.visualManager.grid)
            {
                cell.WalkAble = true;
            }

            if (LvlNumber == 1)
            {
                Notwalkables.Add(Game1.visualManager.grid[11]);

            }
            if (LvlNumber == 2)
            {
                Notwalkables.Add(Game1.visualManager.grid[85]);
                Notwalkables.Add(Game1.visualManager.grid[84]);

            }
            if (LvlNumber == 3)
            {
                Notwalkables.Add(Game1.visualManager.grid[60]);
                Notwalkables.Add(Game1.visualManager.grid[61]);
                Notwalkables.Add(Game1.visualManager.grid[62]);
                Notwalkables.Add(Game1.visualManager.grid[39]);
                Notwalkables.Add(Game1.visualManager.grid[38]);
                Notwalkables.Add(Game1.visualManager.grid[37]);
            }
            if (LvlNumber == 4)
            {
                Notwalkables.Add(Game1.visualManager.grid[61]);
                Notwalkables.Add(Game1.visualManager.grid[62]);
                Notwalkables.Add(Game1.visualManager.grid[63]);
                Notwalkables.Add(Game1.visualManager.grid[64]);
                Notwalkables.Add(Game1.visualManager.grid[74]);
                Notwalkables.Add(Game1.visualManager.grid[30]);
                Notwalkables.Add(Game1.visualManager.grid[29]);
                Notwalkables.Add(Game1.visualManager.grid[28]);
                Notwalkables.Add(Game1.visualManager.grid[27]);
                Notwalkables.Add(Game1.visualManager.grid[17]);
            }

            foreach (Cell cell in Notwalkables)
            {
                cell.WalkAble = false;
            }
        }
        #endregion
    }
}
