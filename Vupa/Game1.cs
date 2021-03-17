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
        private Texture2D textBox;

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
        public static Rectangle border;
        private Texture2D backgroundSprite;
        private Rectangle backgroundRectangle;
        public static Level level;

        //    Grid grid;

        private int lvlnumber = 1;


        private MouseState mClick;


        public Point startLoc;
        public Point endLoc;
        int sizeX = 1000;
        int sizeY = 1000;

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

           

            visualManager = new VisualManager(_spriteBatch  , new Rectangle(0, 0, sizeX, sizeY));
            _graphics.PreferredBackBufferWidth = 1300;
            _graphics.PreferredBackBufferHeight = 1200;
            backgroundRectangle = new Rectangle(0,0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            var bordersize = new Point(_graphics.PreferredBackBufferWidth -300 , _graphics.PreferredBackBufferHeight -200   );
            var borderPosition = new Point(100, 100);
             border = new Rectangle(borderPosition, bordersize);
            //_graphics.IsFullScreen = true;
            player = new Player(startLoc);

            GenerateLvl();

            _graphics.ApplyChanges();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //cellImage = Content.Load<Texture2D>("dirt");
            //agentTexture = Content.Load<Texture2D>("worker");
            font = Content.Load<SpriteFont>("font");
            textBox = Content.Load<Texture2D>("textbox");
            backgroundSprite = Content.Load<Texture2D>("background");

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
            //if (mClick.LeftButton == ButtonState.Pressed)
            //{
            //    startLoc.X = (int)mClick.X / 50;
            //    startLoc.Y = (int)mClick.Y / 50;
            //}
            //if (mClick.RightButton == ButtonState.Pressed)
            //{
            //    endLoc.X = (int)mClick.X / 50;
            //    endLoc.Y = (int)mClick.Y / 50;
            //}




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
            if (player.position.X /100 == endLoc.X && player.position.Y / 100 == endLoc.Y)
            {
                lvlnumber = 2;
                GenerateLvl();
            }
            player.Update();

            base.Update(gameTime);
            //agent.Update(gameTime);

        }
        public void GenerateLvl()
        {
            level = new Level(lvlnumber);
            startLoc = level.SetStart();
            endLoc = level.SetGoal();
            VisualManager.start = startLoc;
            VisualManager.goal = endLoc;
            

            Debug.WriteLine(startLoc);
            Debug.WriteLine(endLoc);
            visualManager.FindPath();
            player.position = new Point(startLoc.X *100, startLoc.Y *100);
            visualManager.LoadContent(Content);

        }




        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundSprite, backgroundRectangle, Color.White);

            visualManager.Render(_spriteBatch);

            player.Draw(_spriteBatch);

            _spriteBatch.Draw(textBox, new Vector2(522, 0), Color.White);

            foreach (var item in buttonlist)
            {
                item.Draw(_spriteBatch);
            }
            _spriteBatch.DrawString(font, $"Selected search method: {chosenOption}", new Vector2(530, 7), Color.White);

            _spriteBatch.End();

       
            //agent.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}