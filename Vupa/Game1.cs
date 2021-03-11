using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Vupa
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D cellImage;

        private Texture2D agentTexture;

        private A_Star a_star;

        private Unit agent;

        private MouseState mClick;

        public static Cell[,] cellInfo = new Cell[10, 10];

        public Point startLoc = new Point(3, 4);
        public Point endLoc = new Point(9, 9);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            this.IsMouseVisible = true;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // Create the tile objects in the array
            for (int x = 0; x < cellInfo.GetLength(0); x++)
            {
                for (int y = 0; y < cellInfo.GetLength(0); y++)
                {
                    cellInfo[x, y] = new Cell();
                }
            }

            // Change some tiles to walls
            cellInfo[4, 0].cellType = Cell.CellType.Wall;
            cellInfo[4, 1].cellType = Cell.CellType.Wall;
            cellInfo[4, 2].cellType = Cell.CellType.Wall;
            cellInfo[4, 3].cellType = Cell.CellType.Wall;
            cellInfo[4, 4].cellType = Cell.CellType.Wall;
            cellInfo[4, 5].cellType = Cell.CellType.Wall;
            cellInfo[3, 5].cellType = Cell.CellType.Wall;
            cellInfo[2, 5].cellType = Cell.CellType.Wall;
            cellInfo[1, 5].cellType = Cell.CellType.Wall;
            cellInfo[1, 4].cellType = Cell.CellType.Wall;
            cellInfo[1, 3].cellType = Cell.CellType.Wall;
            cellInfo[1, 2].cellType = Cell.CellType.Wall;

            cellInfo[7, 6].cellType = Cell.CellType.Wall;
            cellInfo[7, 7].cellType = Cell.CellType.Wall;
            cellInfo[7, 8].cellType = Cell.CellType.Wall;
            cellInfo[7, 9].cellType = Cell.CellType.Wall;


            // Pass the tile information and a weight for the H.
            // The lower the H weight value shorter the path
            // the higher it is the less number of checks it take to determine
            // a path.  Less checks might be useful for a very large number of agents.
            int hWeight = 2;
            a_star = new A_Star(cellInfo, hWeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            cellImage = Content.Load<Texture2D>("dirt");
            agentTexture = Content.Load<Texture2D>("worker");



            agent = new Unit(agentTexture, startLoc, a_star);

        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            mClick = Mouse.GetState();
            if (mClick.LeftButton == ButtonState.Pressed)
            {
                startLoc.X = (int)mClick.X / 50;
                startLoc.Y = (int)mClick.Y / 50;
            }
            if (mClick.RightButton == ButtonState.Pressed)
            {
                endLoc.X = (int)mClick.X / 50;
                endLoc.Y = (int)mClick.Y / 50;
            }
            agent.setDestination(startLoc.X, startLoc.Y, endLoc.X, endLoc.Y);
            base.Update(gameTime);
            agent.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            for (int x = 0; x < cellInfo.GetLength(0); x++)
            {
                for (int y = 0; y < cellInfo.GetLength(0); y++)
                {
                    if (cellInfo[x, y].cellType == Cell.CellType.Floor)
                        spriteBatch.Draw(cellImage, new Vector2(x * 50, y * 50), Color.White);
                    else if (cellInfo[x, y].cellType == Cell.CellType.Wall)
                        spriteBatch.Draw(cellImage, new Vector2(x * 50, y * 50), Color.DarkGray);
                }
            }

            for (int i = 0; i < agent.a_star.Path.Count; i++)
            {
                spriteBatch.Draw(cellImage, new Vector2(agent.a_star.Path[i].X * 50, agent.a_star.Path[i].Y * 50), Color.Yellow);
            }

            spriteBatch.Draw(cellImage, new Vector2(startLoc.X * 50, startLoc.Y * 50), Color.Green);
            spriteBatch.Draw(cellImage, new Vector2(endLoc.X * 50, endLoc.Y * 50), Color.Red);
            agent.Draw(spriteBatch);


            spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}