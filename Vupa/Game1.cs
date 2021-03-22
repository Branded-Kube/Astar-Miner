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
    //MenuState, GameState, GameOverState
    public enum State { MENU, PLAYGAME, HIGHSCORE, GAMEOVER }

    public class Game1 : Game
    {
        #region Fields & Properties
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static ContentManager content;
        public static VisualManager visualManager;

        //Menu
        public Texture2D menuTexture;
        public Texture2D gameOverTexture;
        public Texture2D highscoreTexture;

        //Game
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
        private Button buttonSaveHighScore;
        private Button buttonRestart;

        private string chosenOption;
        private bool options = true;
        private Texture2D button;
        private Player player;
        public static Rectangle border;
        private Texture2D backgroundSprite;
        private Rectangle backgroundRectangle;
        public static Level level;

        //    Grid grid;

        public int lvlnumber = 1;
        //private Keyboard keys = Keyboard.GetState().GetPressedKeys();

        private MouseState mClick;


        public Point startLoc;
        //public Point StartLoc { get; set; }
        public Point endLoc;
        int sizeX = 1000;
        int sizeY = 1000;

        // Set 1st state
        State state = State.MENU;

        #endregion

        #region Constructor
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            IsMouseVisible = true;

            //Menu
            menuTexture = null;

            //Game
            buttonlist = new List<Button>();
            buttonlistDel = new List<Button>();
            buttonlistAdd = new List<Button>();
        }
        #endregion

        #region Methods
        protected override void Initialize()
        {

            visualManager = new VisualManager(_spriteBatch, new Rectangle(0, 0, sizeX, sizeY));
            _graphics.PreferredBackBufferWidth = 1300;
            _graphics.PreferredBackBufferHeight = 1200;
            backgroundRectangle = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            var bordersize = new Point(_graphics.PreferredBackBufferWidth - 300, _graphics.PreferredBackBufferHeight - 200);
            var borderPosition = new Point(100, 100);
            border = new Rectangle(borderPosition, bordersize);
            //_graphics.IsFullScreen = true;

            button = Content.Load<Texture2D>("GameTextures/Btn");
            buttonSearchMethod = new Button(1050, 700, "How do ye wish to search?", button);
            buttonDFS = new Button(1100, 300, "DFS", button);
            buttonBFS = new Button(1100, 350, "BFS", button);
            buttonStartSearch = new Button(1050, 50, "Start search", button);
            buttonStartAstar = new Button(1050, 100, "Start A*", button);
            buttonSaveHighScore = new Button(500, 600, "Save highscore", button);
            buttonRestart = new Button(500, 650, "Restart game", button);

            buttonDFS.Click += DFS_Click;
            buttonBFS.Click += BFS_Click;
            buttonSearchMethod.Click += ButtonSearchMethod_Click;
            buttonStartSearch.Click += ButtonStartSearch_Click;
            buttonStartAstar.Click += ButtonStartAStar_Click;
            buttonSaveHighScore.Click += ButtonSaveHighScore_Click;
            buttonRestart.Click += ButtonRestart_Click;
            buttonlist.Add(buttonSearchMethod);
            startLoc = new Point(1, 1);
            player = new Player(startLoc);
            level = new Level(lvlnumber);

            GenerateLvl();

            _graphics.ApplyChanges();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Menues
            menuTexture = Content.Load<Texture2D>("MenuTextures/menu");
            highscoreTexture = Content.Load<Texture2D>("MenuTextures/scorebox");
            gameOverTexture = Content.Load<Texture2D>("MenuTextures/GameOver");

            //Game
            font = Content.Load<SpriteFont>("Fonts/font");
            textBox = Content.Load<Texture2D>("GameTextures/textbox");
            backgroundSprite = Content.Load<Texture2D>("GameTextures/background");

            player.LoadContent(Content);
            visualManager.LoadContent(Content);
            visualManager.FindPath();


        }

        #region Button Methods
        private void ButtonSaveHighScore_Click(object sender, EventArgs e)
        {
            // Save Highscore stuff her
        }

        private void ButtonRestart_Click(object sender, EventArgs e)
        {
            // Restart spil her
        }

        private void ButtonStartAStar_Click(object sender, EventArgs e)
        {
            //TODO AStar starter
            Debug.WriteLine("A* starting");
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

        #endregion

        public void GameOver()
        {
            // Debug.WriteLine("GameOver");

            buttonlistDel.Add(buttonSearchMethod);
            buttonlistDel.Add(buttonDFS);
            buttonlistDel.Add(buttonBFS);
            buttonlistDel.Add(buttonStartSearch);
            buttonlistDel.Add(buttonStartAstar);

            buttonlistAdd.Clear();

            buttonlistAdd.Add(buttonSaveHighScore);
            buttonlistAdd.Add(buttonRestart);
        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Updating PLAYGAME state
            switch (state)
            {
                case State.PLAYGAME:
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            Exit();
                        }

                        if (player.isAlive == false)
                        {
                            GameOver();
                        }

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
                        if (player.position.X / 100 == endLoc.X && player.position.Y / 100 == endLoc.Y)
                        {
                            if (lvlnumber < 4)
                            {
                                lvlnumber++;
                                player.Health += 3;

                            }
                            else
                            {
                                Debug.WriteLine("WIN SCREEN");
                            }
                            GenerateLvl();
                            //visualManager.FindPath();

                        }
                        player.Update();
                        break;
                    }

                // Updating MENU state
                case State.MENU:
                    {
                        KeyboardState keyState = Keyboard.GetState();

                        //If enter or space in down = start the game
                        if (keyState.IsKeyDown(Keys.Enter) || keyState.IsKeyDown(Keys.Space))
                        {
                            state = State.PLAYGAME;

                            // Put media/music for the PLAYGAME here (if its a long soundtrack because it will only be played once, once you hit play game)
                        }
                        
                        //If S is down, look at highscore
                        if (keyState.IsKeyDown(Keys.S))
                        {
                            state = State.HIGHSCORE;
                        }

                        //If Escape is down close game
                        if (keyState.IsKeyDown(Keys.Escape))
                        {
                            Exit();
                        }

                        //FOR TESTING - game over screen
                        if (keyState.IsKeyDown(Keys.Q))
                        {
                            state = State.GAMEOVER;
                        }

                        break;
                    }

                // Updating HIGHSCORE state
                case State.HIGHSCORE:
                    {
                        KeyboardState keyState = Keyboard.GetState();

                        if (keyState.IsKeyDown(Keys.B))
                        {
                            state = State.MENU;
                        }
                        break;
                    }

                // Updating GAMEOVER State
                case State.GAMEOVER:
                    {
                        KeyboardState keyState = Keyboard.GetState();

                        if (keyState.IsKeyDown(Keys.Enter) || keyState.IsKeyDown(Keys.Space))
                        {
                            state = State.MENU;
                        }
                        if (keyState.IsKeyDown(Keys.Escape))
                        {
                            Exit();
                        }
                        break;
                    }
            }

            base.Update(gameTime);
        }

        public void GenerateLvl()
        {
            level.LvlNumber = lvlnumber;
            startLoc = level.SetStart();
            endLoc = level.SetGoal();
            VisualManager.start = startLoc;
            VisualManager.goal = endLoc;

            Debug.WriteLine(startLoc);
            Debug.WriteLine(endLoc);
            player.position = new Point(startLoc.X * 100, startLoc.Y * 100);

            level.SetWalls();
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            switch (state)
            {
                //Drawing PLAYGAME state
                case State.PLAYGAME:
                    {
                        _spriteBatch.Draw(backgroundSprite, backgroundRectangle, Color.White);

                        visualManager.Draw(_spriteBatch);

                        player.Draw(_spriteBatch);



                        foreach (var item in buttonlist)
                        {
                            item.Draw(_spriteBatch);
                        }

                        if (player.isAlive == true)
                        {
                            _spriteBatch.Draw(textBox, new Vector2(522, 0), Color.White);
                            _spriteBatch.DrawString(font, $"Selected search method: {chosenOption}", new Vector2(530, 7), Color.White);
                        }


                        if (player.isAlive == false)
                        {
                            _spriteBatch.DrawString(font, $"Gameover your final score is: {player.score} ", new Vector2(500, 500), Color.Red);
                        }
                        break;
                    }

                // Drawing MENU state
                case State.MENU:
                    {
                        _spriteBatch.Draw(menuTexture, new Vector2(0, 0), Color.White);
                        break;
                    }

                //Drawing HIGHSCORE state
                case State.HIGHSCORE:
                    {
                        _spriteBatch.Draw(highscoreTexture, new Vector2(0, 0), Color.White);
                        break;
                    }

                // Drawing GAMEOVER state
                case State.GAMEOVER:
                    {
                        _spriteBatch.Draw(gameOverTexture, new Vector2(0, 0), Color.White);
                        break;
                    }
            }
            _spriteBatch.End();

            //agent.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
        #endregion
    }
}