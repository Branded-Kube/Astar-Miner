using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vupa
{
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

        private Player player;
        public static Rectangle border;
        private Texture2D backgroundSprite;
        private Texture2D lvl1box;
        private Texture2D lvl2box;
        private Texture2D lvl3box;
        private Texture2D lvl4box;
        private Texture2D controlsinfo;
        private Rectangle backgroundRectangle;
        public static Level level;
        StringBuilder PlayerNameInput = new StringBuilder("Player");
        Highscore highScore = new Highscore();
        KeyboardState oldstate;

        //    Grid grid;

        public int lvlnumber;
        public Point startLoc;
        public Point endLoc;
        int sizeX = 1000;
        int sizeY = 1000;

        // Set 1st state
        public static State state = State.MENU;
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

           
        }
        #endregion

        #region Methods
        protected override void Initialize()
        {
           
            visualManager = new VisualManager(_spriteBatch, new Rectangle(0, 0, sizeX, sizeY));
            _graphics.PreferredBackBufferWidth = 1400;
            _graphics.PreferredBackBufferHeight = 1080;
            backgroundRectangle = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            var bordersize = new Point(_graphics.PreferredBackBufferWidth - 400, _graphics.PreferredBackBufferHeight - 80);
            var borderPosition = new Point(100, 100);
            border = new Rectangle(borderPosition, bordersize);

         
          
            startLoc = new Point(1, 1);
            player = new Player(startLoc);
            level = new Level(lvlnumber);
            StartGame();

            GenerateLvl();
            Window.TextInput += NameInput;

            _graphics.ApplyChanges();


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

            base.Initialize();
        }

        private void StartGame()
        {
            lvlnumber = 1;
            player.Health = 5;
            player.isAlive = true;

        }

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
            else
            {
                var character = e.Character;
                PlayerNameInput.Append(character);

            }
            player.Name = PlayerNameInput.ToString();
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
            lvl1box = Content.Load<Texture2D>("GameTextures/lvl1info");
            lvl2box = Content.Load<Texture2D>("GameTextures/lvl2info");
            lvl3box = Content.Load<Texture2D>("GameTextures/lvl3info");
            lvl4box = Content.Load<Texture2D>("GameTextures/lvl4info");
            controlsinfo = Content.Load<Texture2D>("GameTextures/controls");


            player.LoadContent(Content);
            visualManager.LoadContent(Content);
            visualManager.FindPath();


        }

       

        private void SaveHighScore()
        {
            var score = new Highscore() { Score = player.score, Name = player.name };
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

            // Updating PLAYGAME state
            switch (state)
            {
                case State.PLAYGAME:
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            Exit();
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

                        }
                        player.Update();
                        break;
                    }

                // Updating MENU state
                case State.MENU:
                    {
                        KeyboardState keyState = Keyboard.GetState();
                        //If enter or space in down = start the game
                        if (keyState.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                        {
                            Window.TextInput -= NameInput;

                            state = State.PLAYGAME;

                            // Put media/music for the PLAYGAME here (if its a long soundtrack because it will only be played once, once you hit play game)
                        }

                        //If S is down, look at highscore
                        if (keyState.IsKeyDown(Keys.S))
                        {
                            Window.TextInput -= NameInput;

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
                        oldstate = keyState;
                        break;
                    }

                // Updating HIGHSCORE state
                case State.HIGHSCORE:
                    {
                        KeyboardState keyState = Keyboard.GetState();
                        if (keyState.IsKeyDown(Keys.B))
                        {
                            Window.TextInput += NameInput;
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
                            SaveHighScore();
                            state = State.HIGHSCORE;

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
            player.tmpposition = new Point(startLoc.X * 100, startLoc.Y * 100);

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
                        _spriteBatch.Draw(controlsinfo, new Vector2(1180, 850), Color.White);

                        visualManager.Draw(_spriteBatch);

                        player.Draw(_spriteBatch);

                        // draws goal ontop of fog of war
                        visualManager.grid.Find(cell => cell.MyPos == endLoc).Draw(_spriteBatch);

                    
                        if (lvlnumber == 1)
                        {
                            _spriteBatch.Draw(lvl1box, new Vector2(1125, 80), Color.White);
                        }
                        if (lvlnumber == 2)
                        {
                            _spriteBatch.Draw(lvl2box, new Vector2(1125, 80), Color.White);
                        }
                        if (lvlnumber == 3)
                        {
                            _spriteBatch.Draw(lvl3box, new Vector2(1125, 80), Color.White);
                        }
                        if (lvlnumber == 4)
                        {
                            _spriteBatch.Draw(lvl4box, new Vector2(1125, 80), Color.White);
                        }

                        if (player.isAlive == true)
                        {
                            _spriteBatch.Draw(textBox, new Vector2(522, 0), Color.White);
                            _spriteBatch.DrawString(font, $"Score: {player.score.ToString()} ", new Vector2(530, 10), Color.White);
                            _spriteBatch.DrawString(font, $"Health: {player.Health.ToString()} ", new Vector2(650, 10), Color.White);

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
                        _spriteBatch.Draw(highscoreTexture, new Vector2(0, 0), Color.White);


                        // Loading highscores
                        //if (File.Exists("highscore.xml"))
                        //{
                        //    var serializer = new XmlSerializer(highScore.highScores.GetType(), "HighScores.Scores");
                        //    object obj;
                        //    using (var reader = new StreamReader("highscores.xml"))
                        //    {
                        //        obj = serializer.Deserialize(reader.BaseStream);
                        //    }
                        //    highScore.highScores = (List<Highscore>)obj;

                        //}

                        int i = 0;
                        foreach (Highscore h in highScore.highScores)
                        {
                            _spriteBatch.DrawString(font, $"Name:  {h.Name}         Score: {h.Score}", new Vector2(500, 400 + 50 * i), Color.White);
                            i++;
                        }
                        break;
                    }

                // Drawing GAMEOVER state
                case State.GAMEOVER:
                    {
                        _spriteBatch.Draw(gameOverTexture, new Vector2(0, 0), Color.White);
                        
                        _spriteBatch.DrawString(font, $"Gameover your final score is: {player.score} ", new Vector2(500, 500), Color.Red);
                        break;
                    }
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}