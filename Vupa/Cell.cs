using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Vupa
{


    public class Cell
    {
        public enum CellType { Floor, Wall };
        //public enum CellType { Floor, Wall, Door, OpenDoor };

        public CellType cellType = CellType.Floor;
        public Unit unitOnCell;
    }

}