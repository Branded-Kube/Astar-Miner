using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Vupa
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static ContentManager content;
        private VisualManager visualManager;

        Grid[,] grid;




        public static SpriteFont font;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //grid = new Grid[10, 10];

            visualManager = new VisualManager(_spriteBatch  , new Rectangle(0, 0, 1000,1000));
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();
            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //cellImage = Content.Load<Texture2D>("dirt");
            //agentTexture = Content.Load<Texture2D>("worker");
            font = Content.Load<SpriteFont>("font");



            //for (int x = 0; x < grid.GetLength(0); x++)
            //{
            //    for (int y = 0; y < grid.GetLength(1); y++)
            //    {
            //        grid[x, y] = new Grid(Content.Load<Texture2D>("dirt2"), new Rectangle());
            //    }
            //}
            // TODO: use this.Content to load your game content here

           

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                visualManager.FindPath();


            // TODO: Add your update logic here
            visualManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            //for (int x = 0; x < grid.GetLength(0); x++)
            //{
            //    for (int y = 0; y < grid.GetLength(1); y++)
            //    {
            //        if (grid[x, y] != null)
            //        {
            //            int size = 36;
            //            Vector2 position = new Vector2(x * size, y * size);
            //            grid[x, y].Draw(_spriteBatch, position);
            //            //Console.WriteLine("something");

            //        }
            //    }
            //}
            visualManager.Render(_spriteBatch);


            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
