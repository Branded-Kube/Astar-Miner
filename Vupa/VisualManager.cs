using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Vupa
{
   public class VisualManager
    {
        #region Fields & Properties
        private Rectangle displayRectangle;

        //Handeling of nodes
        public static Point goal;
        public static Point start;

        //Handeling of cells
        private int cellCount;

        AStar aStar;

        private Texture2D sprite;
        private Texture2D wallSprite;
        private Texture2D playerSprite;
        private Texture2D goalSprite;
        //Collections
        public List<Cell> grid;

        public static List<Node> finalPath;


        #endregion

        #region Constructor
        public VisualManager(SpriteBatch spriteBatch, Rectangle displayRectangle)
        {
            this.displayRectangle = displayRectangle;

            aStar = new AStar();

            cellCount = 10;

            CreateGrid();
        }
        #endregion

        #region Methods
        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Cell cell in grid)
            {
                cell.Draw(spriteBatch);
            }

        }
      

        //colors in the nodes of the final path so the player can see the optimal route
        public void FindPath()
        {
            finalPath = aStar.FindPath(start, goal, CreateNodes());
            if (Game1.level.LvlNumber == 1)
            {
                ColorNodes();

            }


        }

        /// <summary>
        /// creates a grid from the ammount of cells we have
        /// </summary>
        public void CreateGrid()
        {
            grid = new List<Cell>();

            grid.Clear();

            int cellSize = displayRectangle.Width / cellCount;

            for (int x = 0; x < cellCount; x++)
            {
                for (int y = 0; y < cellCount; y++)
                {
                    grid.Add(new Cell(new Point( x+1 , y+1), cellSize));
                }
            }
        }
       
    //creates a node for each individual cell in the grid, excluding the cells marked unwalkable
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
                ChangeTexture(cell);

            }

            return allNodes;
        }

        //colors in the nodes with  different colors to represent the open nodes, and the closed nodes that were evaluated, and the final path
        public void ColorNodes()
        {
            foreach (Cell cell in grid)
            {

                if (cell.MyPos != start && cell.MyPos != goal)
                {
                    cell.MyColor = Color.White;
                }
                if (aStar.Open.Exists(x => x.Position == cell.MyPos) && cell.MyPos != start && cell.MyPos != goal)
                    {
                        cell.MyColor = Color.CornflowerBlue;
                    }
                    if (aStar.Closed.Exists(x => x.Position == cell.MyPos) && cell.MyPos != start && cell.MyPos != goal)
                    {
                        cell.MyColor = Color.BlueViolet;
                    }
                    if (finalPath.Exists(x => x.Position == cell.MyPos) && cell.MyPos != start && cell.MyPos != goal)
                    {
                        cell.MyColor = Color.Green;
                    }
                
                
            }
        }

        /// <summary>
        /// put the correct textures on the walls, start cell, goal cell
        /// </summary>
        /// <param name="cell"></param>
        public void ChangeTexture(Cell cell)
        {
            cell.Sprite = sprite;
            if (!cell.WalkAble)
            {
                cell.Sprite = wallSprite;

            }
            if (cell.MyPos == VisualManager.start)
            {
                cell.Sprite = playerSprite;
                cell.MyColor = Color.Gray;

            }
            if (cell.MyPos == VisualManager.goal)
            {
                cell.Sprite = goalSprite;
                cell.MyColor = Color.White;

            }
        }
        /// <summary>
        /// Loads / sets cell textures
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            
            sprite = Game1.content.Load<Texture2D>("GameTextures/tile");

            wallSprite = Game1.content.Load<Texture2D>("GameTextures/wall");

            playerSprite = Game1.content.Load<Texture2D>("GameTextures/player");

            goalSprite = Game1.content.Load<Texture2D>("GameTextures/goal");
        }
        #endregion
    }
}
