using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Vupa
{
    public enum State { MENU, PLAYGAME, HIGHSCORE, GAMEOVER }

    public class Game1 : Game
    {
        #region Fields & Properties
        public static ContentManager content;
        public static SpriteFont font;
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;

        //Textures
        private Texture2D menuTexture;
        private Texture2D gameOverTexture;
        private Texture2D highscoreTexture;
        private Texture2D textBox;
        private Texture2D backgroundSprite;
        private Texture2D lvl1box;
        private Texture2D lvl2box;
        private Texture2D lvl3box;
        private Texture2D lvl4box;
        private Texture2D controlsinfo;

        // Custom classes/objects
        public static VisualManager visualManager;
        public static Level level;
        public static State state = State.MENU;
        private Player player;
        private Highscore highScore = new Highscore();
        private StringBuilder PlayerNameInput = new StringBuilder("Player");

        // Rectangles
        public static Rectangle border;
        private Rectangle backgroundRectangle;

        // Ints /points
        public int lvlnumber;
        private int sizeX = 1000;
        private int sizeY = 1000;
        private Point aStarStartPos;
        private Point aStarGoalPos;

        // Set 1st state
        private KeyboardState oldstate;
        private KeyboardState keyState;

        #endregion

        #region Constructor
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
           // IsMouseVisible = true;


            //Menu
            //menuTexture = null;

           
        }
        #endregion

        #region Methods
        protected override void Initialize()
        {
            // Sets window size
            _graphics.PreferredBackBufferWidth = 1400;
            _graphics.PreferredBackBufferHeight = 1080;

            // Instantiate objects
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundRectangle = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            border = new Rectangle(100,100, _graphics.PreferredBackBufferWidth - 400, _graphics.PreferredBackBufferHeight - 80);
            visualManager = new VisualManager(_spriteBatch, new Rectangle(0, 0, sizeX, sizeY));
            player = new Player(aStarStartPos);
            level = new Level(lvlnumber);

            Window.TextInput += NameInput;

            CheckXML();
            _graphics.ApplyChanges();
            base.Initialize();
        }
        /// <summary>
        /// Checks if an XML exits, otherwise creates one
        /// </summary>
        private void CheckXML()
        {
            if (File.Exists("highscores.xml"))
            {
                var serializer = new XmlSerializer(highScore.highScores.GetType(), "HighScores.Scores");
                object obj;
                using (var reader = new StreamReader("highscores.xml"))
                {
                    obj = serializer.Deserialize(reader.BaseStream);
                }
                highScore.highScores = (List<Highscore>)obj;
            }
        }

        /// <summary>
        /// Starts the game, sets default values for player / lvl at level 1
        /// </summary>
        private void StartGame()
        {
            lvlnumber = 1;
            player.Health = 5;
            player.isAlive = true;
            player.score = 0;
        }


        /// <summary>
        /// Deletes or adds a character to the stringbuilder and then sets the player name = PlayerNameInput.ToString()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameInput(object sender, TextInputEventArgs e)
        {
            var pressedKey = e.Key;
            int length = PlayerNameInput.Length;
            if (pressedKey == Keys.Back)
            {
                if (length > 0)
                {
                    PlayerNameInput.Remove(length -1, 1);
                }

            }
            else if (pressedKey != Keys.Tab)
            {
                var character = e.Character;
                PlayerNameInput.Append(character);
            }
            player.Name = PlayerNameInput.ToString();
        }


        protected override void LoadContent()
        {
            //Menues
            menuTexture = Content.Load<Texture2D>("MenuTextures/menu");
            highscoreTexture = Content.Load<Texture2D>("MenuTextures/highscore");
            gameOverTexture = Content.Load<Texture2D>("MenuTextures/GameOver");

            //Game
            font = Content.Load<SpriteFont>("Fonts/font");
            textBox = Content.Load<Texture2D>("GameTextures/textbox");
            backgroundSprite = Content.Load<Texture2D>("GameTextures/background");
            lvl1box = Content.Load<Texture2D>("GameTextures/info1");
            lvl2box = Content.Load<Texture2D>("GameTextures/info2");
            lvl3box = Content.Load<Texture2D>("GameTextures/info3");
            lvl4box = Content.Load<Texture2D>("GameTextures/info4");
            controlsinfo = Content.Load<Texture2D>("GameTextures/controls2");

            player.LoadContent(Content);
            visualManager.LoadContent(Content);
        }

       
        /// <summary>
        /// Sorts and saves highscore from xlm 
        /// </summary>
        private void SaveHighScore()
        {
            var score = new Highscore() { Score = player.score, Name = player.Name };
            highScore.highScores.Add(score);


            var serializer = new XmlSerializer(highScore.highScores.GetType(), "HighScores.Scores");
            Debug.WriteLine("Saving highscore");
            using (var writer = new StreamWriter("highscores.xml", false))
            {
                serializer.Serialize(writer.BaseStream, highScore.highScores);
                Debug.WriteLine("highscore saved");
            }
            highScore.highScores.Sort();

        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Listens for keyboard input
             keyState = Keyboard.GetState();

            // Updates current state
            switch (state)
            {
                // Updating PLAYGAME state
                case State.PLAYGAME:
                    {
                        // If player reaches goal, advance to next lvl number and increase health. if no more lvls, show win screen
                        if (player.tmpposition.X / 100 == aStarGoalPos.X && player.tmpposition.Y / 100 == aStarGoalPos.Y)
                        {
                            if (lvlnumber < 7)
                            {
                                lvlnumber++;
                                player.Health += 3;
                            }
                            else
                            {
                                Debug.WriteLine("WIN SCREEN"); 
                                state = State.GAMEOVER;
                            }
                            GenerateLvl();
                        }
                        player.Update();
                        break;
                    }

                // Updating MENU state
                case State.MENU:
                    {
                        //If enter or space in down = setup game and change state to PlayGame
                        if (keyState.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                        {
                            Window.TextInput -= NameInput;
                            StartGame();
                            GenerateLvl();
                            state = State.PLAYGAME;
                        }
                        //If S is down, look at highscore
                        if (keyState.IsKeyDown(Keys.Tab) && oldstate.IsKeyUp(Keys.Tab))
                        {
                            Window.TextInput -= NameInput;
                            state = State.HIGHSCORE;
                        }
                        oldstate = keyState;
                        break;
                    }

                // Updating HIGHSCORE state
                case State.HIGHSCORE:
                    {
                        // Deletes highscores stored (excluding the new player and score)
                        if (keyState.IsKeyDown(Keys.X) && oldstate.IsKeyUp(Keys.X))
                        {
                            highScore.highScores.Clear();
                            SaveHighScore();
                        }
                        if (keyState.IsKeyDown(Keys.Tab) && oldstate.IsKeyUp(Keys.Tab))
                        {
                            Window.TextInput += NameInput;
                            state = State.MENU;
                        }
                        oldstate = keyState;

                        break;
                    }

                // Updating GAMEOVER State
                case State.GAMEOVER:
                    {
                        // If Enter/ space is pressed, save highscore and change state to Highscore
                        if (keyState.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                        {
                            SaveHighScore();
                            state = State.HIGHSCORE;
                        }
                        oldstate = keyState;
                        break;
                    }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Resets cell colors, and sets astar start/goal positions, walls and player position depending on lvlnumber
        /// </summary>
        public void GenerateLvl()
        {
            foreach (Cell cell in visualManager.grid)
            {
                cell.MyColor = Color.White;
            }
            level.LvlNumber = lvlnumber;
            aStarStartPos = level.SetStart();
            aStarGoalPos = level.SetGoal();
            level.SetWalls();

            VisualManager.start = aStarStartPos;
            VisualManager.goal = aStarGoalPos;
            player.tmpposition = new Point(aStarStartPos.X * 100, aStarStartPos.Y * 100);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            // Draws state depending on state
            switch (state)
            {
                //Drawing PLAYGAME state
                case State.PLAYGAME:
                    {
                        if (player.isAlive == true)
                        {
                            _spriteBatch.Draw(backgroundSprite, backgroundRectangle, Color.White);
                            visualManager.Draw(_spriteBatch);
                            player.Draw(_spriteBatch);
                            _spriteBatch.Draw(controlsinfo, new Vector2(1180, 270), Color.White);

                            // draws goal ontop of fog of war
                            if (lvlnumber < 5)
                            {
                                visualManager.grid.Find(cell => cell.MyPos == aStarGoalPos).Draw(_spriteBatch);
                            }

                            // Draws infobox depending on lvlnumber
                            if (lvlnumber == 1)
                            {
                                _spriteBatch.Draw(lvl1box, new Vector2(1125, 545), Color.White);
                            }
                            if (lvlnumber == 2)
                            {
                                _spriteBatch.Draw(lvl2box, new Vector2(1125, 545), Color.White);
                            }
                            if (lvlnumber == 3)
                            {
                                _spriteBatch.Draw(lvl3box, new Vector2(1125, 545), Color.White);
                            }
                            if (lvlnumber == 4)
                            {
                                _spriteBatch.Draw(lvl4box, new Vector2(1125, 545), Color.White);
                            }

                            _spriteBatch.Draw(textBox, new Rectangle(1100, 95, 300, 80), Color.White);
                            _spriteBatch.DrawString(font, $" {player.score} ", new Vector2(1349, 113), Color.White);
                            _spriteBatch.DrawString(font, $" {player.Health} ", new Vector2(1238, 113), Color.White);
                            _spriteBatch.DrawString(font, $" {player.Name} ", new Vector2(1238, 143), Color.White);
                        }
                        break;
                    }

                // Drawing MENU state
                case State.MENU:
                    {
                        _spriteBatch.Draw(menuTexture, new Rectangle(0,0,_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

                      

                        _spriteBatch.DrawString(font, "Enter your name : ", new Vector2((_graphics.PreferredBackBufferWidth / 2)-100, 750), Color.Black, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
                        _spriteBatch.DrawString(font, PlayerNameInput, new Vector2((_graphics.PreferredBackBufferWidth / 2)-100, 800) , Color.Black, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);

                      
                        break;
                    }

                //Drawing HIGHSCORE state
                case State.HIGHSCORE:
                    {
                        _spriteBatch.Draw(highscoreTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

                      

                        // Draws each Highscore
                        int i = 0;
                        foreach (Highscore h in highScore.highScores)
                        {
                            _spriteBatch.DrawString(font, $"Name:  {h.Name}     Score: {h.Score}", new Vector2(500, 400 + 50 * i), Color.Black, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None,0.0f );
                            i++;
                        }
                        break;
                    }

                // Drawing GAMEOVER state
                case State.GAMEOVER:
                    {
                        _spriteBatch.Draw(gameOverTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                        
                        _spriteBatch.DrawString(font, $"Your final score is: {player.score} ", new Vector2(570, 540), Color.Black, 0.0f , Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
                        break;
                    }
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}