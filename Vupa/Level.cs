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
        /// <summary>
        /// sets the starting position of the player for every level
        /// </summary>
        /// <returns></returns>
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
            if (LvlNumber == 5)
            {
                return new Point(7, 3);
            }
            if (LvlNumber == 6)
            {
                return new Point(6, 9);
            }
            if (LvlNumber == 7)
            {
                return new Point(2, 9);
            }
            else
            {
                return new Point();

            }

        }

        /// <summary>
        /// sets the goal position for every level
        /// </summary>
        /// <returns></returns>
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
            if (LvlNumber == 5)
            {
                return new Point(6, 9);
            }
            if (LvlNumber == 6)
            {
                return new Point(1, 9);
            }
            if (LvlNumber == 7)
            {
                return new Point(1, 1);
            }
            else
            {
                return new Point(5, 5);

            }
        }
        /// <summary>
        /// makes all cells in the new level walkable
        /// clears all walls from previous level
        /// sets all walls for individual levels
        /// </summary>
        public void SetWalls()
        {
            foreach (Cell cell in Game1.visualManager.grid)
            {
                cell.WalkAble = true;
            }
            Notwalkables.Clear();

            if (LvlNumber == 1)
            {
                Notwalkables.Add(Game1.visualManager.grid[11]);

            }
            if (LvlNumber == 2)
            {
                Notwalkables.Add(Game1.visualManager.grid[85]);
                Notwalkables.Add(Game1.visualManager.grid[84]);
                Notwalkables.Add(Game1.visualManager.grid[75]);

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
            if (LvlNumber == 5) 
            {
                Notwalkables.Add(Game1.visualManager.grid[52]);
                Notwalkables.Add(Game1.visualManager.grid[53]);
                Notwalkables.Add(Game1.visualManager.grid[54]);
                Notwalkables.Add(Game1.visualManager.grid[51]);
                Notwalkables.Add(Game1.visualManager.grid[41]);
                Notwalkables.Add(Game1.visualManager.grid[31]);
                Notwalkables.Add(Game1.visualManager.grid[65]);
                Notwalkables.Add(Game1.visualManager.grid[75]);
                Notwalkables.Add(Game1.visualManager.grid[85]);
                Notwalkables.Add(Game1.visualManager.grid[67]);
                Notwalkables.Add(Game1.visualManager.grid[78]); 
                Notwalkables.Add(Game1.visualManager.grid[88]);
                Notwalkables.Add(Game1.visualManager.grid[98]);
            }
            if (LvlNumber == 6)
            {
                Notwalkables.Add(Game1.visualManager.grid[7]);
                Notwalkables.Add(Game1.visualManager.grid[13]);
                Notwalkables.Add(Game1.visualManager.grid[24]);
                Notwalkables.Add(Game1.visualManager.grid[25]);
                Notwalkables.Add(Game1.visualManager.grid[35]);
                Notwalkables.Add(Game1.visualManager.grid[36]);
                Notwalkables.Add(Game1.visualManager.grid[37]);
                Notwalkables.Add(Game1.visualManager.grid[38]);
                Notwalkables.Add(Game1.visualManager.grid[39]);
                Notwalkables.Add(Game1.visualManager.grid[40]);
                Notwalkables.Add(Game1.visualManager.grid[45]);
                Notwalkables.Add(Game1.visualManager.grid[55]);
                Notwalkables.Add(Game1.visualManager.grid[65]);

            }
            if (LvlNumber == 7)
            {
                Notwalkables.Add(Game1.visualManager.grid[3]);
                Notwalkables.Add(Game1.visualManager.grid[7]);
                Notwalkables.Add(Game1.visualManager.grid[17]);
                Notwalkables.Add(Game1.visualManager.grid[22]);
                Notwalkables.Add(Game1.visualManager.grid[24]);
                Notwalkables.Add(Game1.visualManager.grid[26]);
                Notwalkables.Add(Game1.visualManager.grid[35]);
                Notwalkables.Add(Game1.visualManager.grid[40]);
                Notwalkables.Add(Game1.visualManager.grid[41]);
                Notwalkables.Add(Game1.visualManager.grid[42]);
                Notwalkables.Add(Game1.visualManager.grid[43]);
                Notwalkables.Add(Game1.visualManager.grid[45]);
                Notwalkables.Add(Game1.visualManager.grid[48]);
                Notwalkables.Add(Game1.visualManager.grid[49]);
                Notwalkables.Add(Game1.visualManager.grid[55]);
                Notwalkables.Add(Game1.visualManager.grid[64]);
                Notwalkables.Add(Game1.visualManager.grid[65]);
                Notwalkables.Add(Game1.visualManager.grid[66]);
                Notwalkables.Add(Game1.visualManager.grid[73]);
                Notwalkables.Add(Game1.visualManager.grid[77]);
                Notwalkables.Add(Game1.visualManager.grid[96]);
            }
            
            foreach (Cell cell in Notwalkables)
            {
                cell.WalkAble = false;
            }
        }
        #endregion
    }
}
