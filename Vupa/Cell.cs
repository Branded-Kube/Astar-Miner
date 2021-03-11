using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AStarExample
{
    enum CellType { START, GOAL, WALL, EMPTY };

    class Cell
    {
        private Image sprite;

        private Point myPos;

        private int cellSize;

        private bool walkAble;

        CellType myType = CellType.EMPTY;

        public bool WalkAble
        {
            get { return walkAble; }
            set { walkAble = value; }
        }

        private Node myNode;

        internal Node MyNode
        {
            get { return myNode; }
            set { myNode = value; }
        }

        private Color myColor;

        public Color MyColor
        {
            get { return myColor; }
            set { myColor = value; }
        }



        public Point MyPos
        {
            get { return myPos; }
            set { myPos = value; }
        }


        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(myPos.X * cellSize, myPos.Y * cellSize, cellSize, cellSize);
            }
        }

        public Cell(Point pos, int size)
        {
            this.myPos = pos;

            this.cellSize = size;

            walkAble = true;

            myColor = Color.White;
        }

        public void Render(Graphics dc)
        {
            dc.FillRectangle(new SolidBrush(myColor), BoundingRectangle);
            dc.DrawRectangle(new Pen(Color.Black), BoundingRectangle);
            if (sprite != null)
            {
                dc.DrawImage(sprite, BoundingRectangle);
            }
            if (myNode != null)
            {
                if (myNode.Parent != null)
                {
                    dc.DrawString(string.Format("{0}", "P: " + myNode.Parent.Position.ToString()), new Font("Arial", 7, FontStyle.Bold), new SolidBrush(Color.Black), myPos.X * cellSize, myPos.Y * cellSize);
                }

                dc.DrawString(string.Format("{0}", "F:" + myNode.F), new Font("Arial", 7, FontStyle.Bold), new SolidBrush(Color.Black), myPos.X * cellSize, (myPos.Y * cellSize) + 25);
                dc.DrawString(string.Format("{0}", "G:" + myNode.G), new Font("Arial", 7, FontStyle.Bold), new SolidBrush(Color.Black), myPos.X * cellSize, (myPos.Y * cellSize) + 40);
                dc.DrawString(string.Format("{0}", "H:" + myNode.H), new Font("Arial", 7, FontStyle.Bold), new SolidBrush(Color.Black), myPos.X * cellSize, (myPos.Y * cellSize) + 55);

            }

            dc.DrawString(string.Format("{0}", myPos), new Font("Arial", 7, FontStyle.Bold), new SolidBrush(Color.Black), myPos.X * cellSize, (myPos.Y * cellSize) + 10);


        }

        public void Click(ref CellType clickType)
        {

            if (clickType == CellType.START)
            {
                sprite = Image.FromFile(@"Images\Start.png");
                myType = clickType;
                clickType = CellType.GOAL;
                VisualManager.start = MyPos;
                myColor = Color.MediumSeaGreen;
            }
            else if (clickType == CellType.GOAL && myType != CellType.START)
            {
                sprite = Image.FromFile(@"Images\Goal.png");
                clickType = CellType.WALL;
                myType = CellType.GOAL;
                VisualManager.goal = MyPos;
                myColor = Color.OrangeRed;
            }
            else if (clickType == CellType.WALL && myType != CellType.START && myType != CellType.GOAL)
            {
                sprite = Image.FromFile(@"Images\Wall.png");
                myType = CellType.WALL;
                WalkAble = false;
            }
        }
    }
}
