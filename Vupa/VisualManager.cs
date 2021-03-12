using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vupa
{
    class VisualManager
    {
        //Handeling of graphics
        //private BufferedGraphics backBuffer;
        //private Graphics dc;
     
        private Rectangle displayRectangle;

        //Handeling of nodes
        public static Point goal;
        public static Point start;

        //Handeling of cells
        private int cellCount;

        AStar aStar;

        private MouseState mouseCurrent;
        private MouseState mouseLast;
        private Rectangle mouseRectangle;

        private CellType clickType;

        //Collections
        private List<Cell> grid;

        private List<Node> finalPath;

        public List<Cell> Grid
        {
            get { return grid; }
            set { grid = value; }
        }

        public VisualManager(SpriteBatch spriteBatch, Rectangle displayRectangle)
        {
            //Create's (Allocates) a buffer in memory with the size of the display
           // this.backBuffer = BufferedGraphicsManager.Current.Allocate(dc, displayRectangle);

            //Sets the graphics context to the graphics in the buffer
            //this.dc = backBuffer.Graphics;

            //Sets the displayRectangle
            this.displayRectangle = displayRectangle;

            aStar = new AStar();

            cellCount = 10;

            CreateGrid();
        }

        public void Render(SpriteBatch spriteBatch)
        {
            //dc.Clear(Color.White);

            foreach (Cell cell in grid)
            {
                cell.Draw(spriteBatch);
            }

            //Renders the content of the buffered graphics context to the real context(Swap buffers)
            //backBuffer.Render();
        }

      

        public void FindPath()
        {
            finalPath = aStar.FindPath(start, goal, CreateNodes());

            ColorNodes();

        }

        public void Update()
        {
            mouseLast = mouseCurrent;
            mouseCurrent = Mouse.GetState();
            mouseRectangle = new Rectangle(mouseCurrent.X, mouseCurrent.Y, 1, 1);

            foreach (Cell cell in grid)
            {
                //if (cell.BoundingRectangle.IntersectsWith(new Rectangle(mousePos, new Size(1, 1))))
                if (cell.BoundingRectangle.Intersects(mouseRectangle))
                {
                    if (mouseLast.LeftButton == ButtonState.Pressed && mouseCurrent.LeftButton == ButtonState.Released)
                    {
                        cell.Click(ref clickType, Game1.content);

                    }
                }


            }
        }
        
        //public void ClickCell(Point mousePos)
        //{

        //    foreach (Cell cell in grid)
        //    {
        //        //if (cell.BoundingRectangle.IntersectsWith(new Rectangle(mousePos, new Size(1, 1))))
        //        if (cell.BoundingRectangle.Intersects(new Rectangle(mousePos.X,mousePos.Y, 1, 1)))
        //        {
        //            cell.Click(ref clickType);
        //        }

        //    }
        //}

        public void CreateGrid()
        {
            grid = new List<Cell>();

            grid.Clear();

            int cellSize = displayRectangle.Width / cellCount;

            for (int x = 0; x < cellCount; x++)
            {
                for (int y = 0; y < cellCount; y++)
                {
                    grid.Add(new Cell(new Point(x, y), cellSize));
                }
            }
        }

        public List<Node> CreateNodes()
        {
            List<Node> allNodes = new List<Node>();

            foreach (Cell cell in grid)
            {
                if (cell.WalkAble)
                {
                    cell.MyNode = new Node(cell.MyPos);
                    allNodes.Add(cell.MyNode);
                }
            }

            return allNodes;
        }

        public void ColorNodes()
        {


            foreach (Cell cell in grid)
            {
                if (aStar.Open.Exists(x => x.Position == cell.MyPos) && cell.MyPos != start && cell.MyPos != goal)
                {
                    cell.MyColor = Color.CornflowerBlue;
                }
                if (aStar.Closed.Exists(x => x.Position == cell.MyPos) && cell.MyPos != start && cell.MyPos != goal)
                {
                    cell.MyColor = Color.Orange;
                }
                if (finalPath.Exists(x => x.Position == cell.MyPos) && cell.MyPos != start && cell.MyPos != goal)
                {
                    cell.MyColor = Color.Green;
                }
            }
        }
    }
}
