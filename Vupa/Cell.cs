using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Vupa
{
    //enum CellType { START, GOAL, WALL, EMPTY };

    public class Cell
    {
        private Texture2D sprite;
        public Texture2D Sprite { get { return sprite; } set { sprite = value; } }
       
        private Point myPos;

        private int cellSize;

        private bool walkAble;

        //CellType myType = CellType.EMPTY;

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

        public void Draw(SpriteBatch spriteBatch)
        {
            if (sprite != null)
            {
                spriteBatch.Draw(sprite, BoundingRectangle, MyColor);
            }
            if (myNode != null)
            {
                if (myNode.Parent != null)
                {
                    spriteBatch.DrawString(Game1.font, string.Format("{0}", "P: " + myNode.Parent.Position.ToString()), new Vector2(myPos.X * cellSize, (myPos.Y * cellSize) + 15), Color.White);

                }

                spriteBatch.DrawString(Game1.font, string.Format("{0}", "F:" + myNode.F), new Vector2(myPos.X * cellSize, (myPos.Y * cellSize) + 30), MyColor);
                spriteBatch.DrawString(Game1.font, string.Format("{0}", "G:" + myNode.G), new Vector2(myPos.X * cellSize, (myPos.Y * cellSize) + 45), MyColor);
                spriteBatch.DrawString(Game1.font, string.Format("{0}", "H:" + myNode.H), new Vector2(myPos.X * cellSize, (myPos.Y * cellSize) + 60), MyColor);


            }

            spriteBatch.DrawString(Game1.font, string.Format("{0}", myPos), new Vector2(myPos.X * cellSize, (myPos.Y * cellSize)), MyColor);


        }
    }
}
