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
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static ContentManager content;
        private VisualManager visualManager;

        private Texture2D agentTexture;
        public static SpriteFont font;

        private List<Button> buttonlist;
        private List<Button> buttonlistDel;
        private List<Button> buttonlistAdd;
        private Button buttonDFS;
        private Button buttonBFS;
        private Button buttonSearchMethod;
        private Button buttonStartSearch;
        private Button buttonStartAstar;

        private string chosenOption;
        private bool options = true;
        private Texture2D button;
        private Player player;
        //private Player fogwar;
        //private FogWar fogwar;
        //private FogWar fwCenter;
        public static Rectangle border;

    //    Grid grid;

        

        

        private MouseState mClick;


        public Point startLoc = new Point(0, 0);
        public Point endLoc = new Point(9, 9);


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            IsMouseVisible = true;

            buttonlist = new List<Button>();
            buttonlistDel = new List<Button>();
            buttonlistAdd = new List<Button>();
        }

        protected override void Initialize()
        {
            // Create the tile objects in the array

          //  grid = new Grid[10, 10]; 

            
            visualManager = new VisualManager(_spriteBatch  , new Rectangle(0, 0, 1000,1000));
            player = new Player(startLoc);
            //fogwar = new Player(startLoc);
            //fwCenter = new FogWar();
            _graphics.PreferredBackBufferWidth = 1300;
            _graphics.PreferredBackBufferHeight = 900;
            var bordersize = new Point(_graphics.PreferredBackBufferWidth  , _graphics.PreferredBackBufferHeight  );
            var borderPosition = new Point(0,0);
             border = new Rectangle(borderPosition, bordersize);
            _graphics.ApplyChanges();
            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //cellImage = Content.Load<Texture2D>("dirt");
            //agentTexture = Content.Load<Texture2D>("worker");
            font = Content.Load<SpriteFont>("font");

            button = Content.Load<Texture2D>("Btn");
            buttonSearchMethod = new Button(1050, 700, "How do ye wish to search?", button);
            buttonDFS = new Button(1100, 300, "DFS", button);
            buttonBFS = new Button(1100, 350, "BFS", button);
            buttonStartSearch = new Button(1050, 50, "Start search", button);
            buttonStartAstar = new Button(1050, 100, "Start A*", button);
            buttonDFS.Click += DFS_Click;
            buttonBFS.Click += BFS_Click;
            buttonSearchMethod.Click += ButtonSearchMethod_Click;
            buttonStartSearch.Click += ButtonStartSearch_Click;
            buttonStartAstar.Click += ButtonStartAStar_Click;
            buttonlist.Add(buttonSearchMethod);

            player.LoadContent(Content);
            //fogwar.LoadContent(Content);
            //fwCenter.LoadContent(Content);
        }

        private void ButtonStartAStar_Click(object sender, EventArgs e)
        {
            //TODO AStar starter
            Debug.WriteLine("A* starting");
            visualManager.FindPath();
        }
        private void ButtonStartSearch_Click(object sender, EventArgs e)
        {
            if (chosenOption == "DFS")
            {
                //TODO DFS stuff
                Debug.WriteLine("Starting DFS search");
                buttonlistAdd.Add(buttonStartAstar);
            }
            else if (chosenOption == "BFS")
            {
                //TODO BFS stuff
                Debug.WriteLine("Starting BFS search");
                buttonlistAdd.Add(buttonStartAstar);
            }
            else
            {
            }
            Debug.WriteLine("No searching method chosen");
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

            else if (options == false)
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

            player.Update();



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                visualManager.FindPath();
            }
             


            // TODO: Add your update logic here
            visualManager.Update();

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


            //fogwar.Update();

            //fwCenter.Update();
            
            base.Update(gameTime);
            //agent.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            visualManager.Render(_spriteBatch);
            //fogwar.Draw(_spriteBatch);
            player.Draw(_spriteBatch);

            foreach (var item in buttonlist)
            {
                item.Draw(_spriteBatch);
            }
            _spriteBatch.DrawString(font, $"Selected search method: {chosenOption}", new Vector2(530, 0), Color.Black);

            //fwCenter.Draw(_spriteBatch);

            _spriteBatch.End();

       
            //agent.Draw(_spriteBatch);

            base.Draw(gameTime);
        }


    }
}