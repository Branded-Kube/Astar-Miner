using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static Vupa.A_Star;

namespace Vupa
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Grid[,] grid;

        private A_Star a_star;

        private List<listitem> val;

        private Unit agent;

        private MouseState mClick;

        public static Cell[,] cellInfo = new Cell[10, 10];

        public Point startLoc = new Point(3, 4);
        public Point endLoc = new Point(9, 9);

        private SpriteFont font;
        int hWeight = 2;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            grid = new Grid[10, 10];

            cellInfo[7, 6].cellType = Cell.CellType.Wall;
            cellInfo[7, 7].cellType = Cell.CellType.Wall;
            cellInfo[7, 8].cellType = Cell.CellType.Wall;
            cellInfo[7, 9].cellType = Cell.CellType.Wall;


            // Pass the tile information and a weight for the H.
            // The lower the H weight value shorter the path
            // the higher it is the less number of checks it take to determine
            // a path.  Less checks might be useful for a very large number of agents.
            a_star = new A_Star(cellInfo, hWeight);


            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            cellImage = Content.Load<Texture2D>("dirt");
            agentTexture = Content.Load<Texture2D>("worker");
            font = Content.Load<SpriteFont>("font");


            agent = new Unit(agentTexture, startLoc, a_star);

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = new Grid(Content.Load<Texture2D>("dirt2"), new Rectangle());
                }
            }
            // TODO: use this.Content to load your game content here

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            for (int x = 0; x < cellInfo.GetLength(0); x++)
            {
                for (int y = 0; y < cellInfo.GetLength(0); y++)
                {
                    if (cellInfo[x, y].cellType == Cell.CellType.Floor)
                        spriteBatch.DrawString(font, $"H {hWeight}", new Vector2(x * 50, y * 50), Color.White);
                    // spriteBatch.Draw(cellImage, new Vector2(x * 50, y * 50), Color.White); 

                    else if (cellInfo[x, y].cellType == Cell.CellType.Wall)
                        spriteBatch.Draw(cellImage, new Vector2(x * 50, y * 50), Color.DarkGray);
                }
            }

            for (int i = 0; i < agent.a_star.Path.Count; i++)
            {
                spriteBatch.Draw(cellImage, new Vector2(agent.a_star.Path[i].X * 50, agent.a_star.Path[i].Y * 50), Color.Yellow);
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] != null)
                    {
                        int size = 36;
                        Vector2 position = new Vector2(x * size, y * size);
                        grid[x, y].Draw(_spriteBatch, position);
                        Console.WriteLine("something");

                    }
                }
                spriteBatch.Draw(cellImage, new Vector2(agent.a_star.Path[i].X * 50, agent.a_star.Path[i].Y * 50), Color.Yellow);
            }

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
