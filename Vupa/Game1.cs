using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Vupa
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static SpriteFont font;

        private List<Button> buttonlist;
        private List<Button> buttonlistDel;
        private List<Button> buttonlistAdd;
        private Button buttonDFS;
        private Button buttonBFS;
        private Button buttonSearchMethod;
        private Button buttonStartSearch;

        private string chosenOption;
        private bool options = true;
        private Texture2D button;

        Grid[,] grid;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            buttonlist = new List<Button>();
            buttonlistDel = new List<Button>();
            buttonlistAdd = new List<Button>();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            grid = new Grid[10, 10];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = new Grid(Content.Load<Texture2D>("dirt2"), new Rectangle());
                }
            }


            font = Content.Load<SpriteFont>("font");
            button = Content.Load<Texture2D>("Btn");



            buttonSearchMethod = new Button(40, 400, "How do ye wish to search?", button);
            buttonDFS = new Button(300, 400, "DFS", button);
            buttonBFS = new Button(350, 400, "BFS", button);
            buttonStartSearch = new Button(500, 50, "Start search", button);

            buttonDFS.Click += DFS_Click;
            buttonBFS.Click += BFS_Click;
            buttonSearchMethod.Click += ButtonSearchMethod_Click;

            

            buttonlist.Add(buttonSearchMethod);
            //buttonlist.Add(buttonDFS);
            //buttonlist.Add(buttonBFS);

        }

        private void ButtonStartSearch_Click(object sender, EventArgs e)
        {
        
            if (chosenOption == "DFS")
            {
                //TODO DFS stuff
            }

            else if (chosenOption == "BFS")
            {
                //TODO BFS stuff
            }
        }

        private void ButtonSearchMethod_Click(object sender, EventArgs e)
        {
            if (options == true)
            {
                buttonlistAdd.Add(buttonDFS);
                buttonlistAdd.Add(buttonBFS);
                Debug.WriteLine("Options unlocked");

                options = false;
            }

            else if(options == false)
            {
                buttonlistDel.Add(buttonDFS);
                buttonlistDel.Add(buttonBFS);

                buttonlistDel.Add(buttonStartSearch);

                buttonlistAdd.Clear();
                options = true;
            }
            

        }
        private void DFS_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Using DFS");
            chosenOption = "DFS";
            buttonlistAdd.Add(buttonStartSearch);
        }
        private void BFS_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Using BFS");
            chosenOption = "BFS";
            buttonlistAdd.Add(buttonStartSearch);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            foreach (var item in buttonlistDel)
            {
                buttonlist.Remove(item);
            }

            foreach (var item in buttonlistAdd)
            {
                buttonlist.Add(item);

            }

            foreach (var item in buttonlist)
            {
                item.Update();

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

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
            }
            foreach (var item in buttonlist)
            {
                item.Draw(_spriteBatch);
            }

            _spriteBatch.DrawString(font, $"Selected search method:  {chosenOption}", new Vector2(500, 0), Color.Black);


            _spriteBatch.End();

           

            base.Draw(gameTime);
        }
    }
}
