 using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Vupa
{
    public class Player
    {
        #region Fields & Properties

        // Textures
        private Texture2D sprite;
        private Texture2D fogSprite1;
        private Texture2D fogSprite2;
        private Texture2D fogSprite3;
        private Texture2D fogSprite4;
        private Color color = Color.White;

        // Point
        public Point tmpposition;
        private Point position;
        private Point size;
        private Point fogPosition;
        private Point fogSize;

        // Rectangle
        private Rectangle playerRectangle;
        private Rectangle fogRectangle;

        // Int
        public int score;
        public int Health {get {return health;} set { health = value; } }
        private int health;

        // String
        public string Name { get { return name; } set { name = value; } }
        private string name;

        // Bool
        public bool isAlive;
        private bool dontMove;
        private bool dontMoveCorner;
        private bool correct;
        private bool correctPathCheck;

        // Keybord / Keys
        private KeyboardState oldState;
        private KeyboardState newState;
        private Keys currentKey;

        #endregion

        #region Constructor
        /// <summary>
        /// Player constructor, start is the players start position
        /// </summary>
        /// <param name="start"></param>
        public Player(Point start)
        {
            this.size = new Point(100, 100);
            this.playerRectangle = new Rectangle(position, size);
            this.fogSize = new Point(3098, 3098);
            this.fogRectangle = new Rectangle(fogPosition, fogSize);

        }
        #endregion

        #region Methods
        public void Update()
        {
            CheckState();

            // Calls move if any key is pressed
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                Move(currentKey);
            }

            currentKey = Keys.None;
            

        }

        /// <summary>
        /// Checks which key is pressed
        /// Then checks if player is inside game borders
        /// If moving to a corner (7,9,1,3) checks if there is a wall diagonal/horizontal to the player position in the direction player is trying to move to
        /// </summary>
        public void CheckState()
        {
            newState = Keyboard.GetState();
            dontMoveCorner = false;

            if (isAlive == true)
            {
                if (newState.IsKeyDown(Keys.NumPad1) && oldState.IsKeyUp(Keys.NumPad1))
                {
                   
                    if (playerRectangle.Left >= Game1.border.Left + 1 && playerRectangle.Bottom <= Game1.border.Bottom - 1)
                    {

                        foreach (Cell cell in Game1.level.Notwalkables)
                        {
                            if (playerRectangle.Y == cell.BoundingRectangle.Y - 100 && playerRectangle.X == cell.BoundingRectangle.X || playerRectangle.Y == cell.BoundingRectangle.Y && playerRectangle.X == cell.BoundingRectangle.X + 100)
                            {
                                dontMoveCorner = true;
                            }
                        }
                        if (dontMoveCorner == false)
                        {
                            currentKey = Keys.NumPad1;

                        }
                    }

                }

                else if (newState.IsKeyDown(Keys.NumPad2) && oldState.IsKeyUp(Keys.NumPad2))
                {

                    if (playerRectangle.Bottom <= Game1.border.Bottom - 1)
                    {
                        currentKey = Keys.NumPad2;
                    }

                }

                else if (newState.IsKeyDown(Keys.NumPad3) && oldState.IsKeyUp(Keys.NumPad3))
                {
                    if (playerRectangle.Right <= Game1.border.Right - 1 && playerRectangle.Bottom <= Game1.border.Bottom - 1)
                    {
                        foreach (Cell cell in Game1.level.Notwalkables)
                        {
                            if (playerRectangle.Y == cell.BoundingRectangle.Y - 100 && playerRectangle.X == cell.BoundingRectangle.X || playerRectangle.Y == cell.BoundingRectangle.Y && playerRectangle.X == cell.BoundingRectangle.X - 100)
                            {
                                dontMoveCorner = true;
                            }
                        }
                        if (dontMoveCorner == false)
                        {
                            currentKey = Keys.NumPad3;
                        }

                    }

                }

                else if (newState.IsKeyDown(Keys.NumPad4) && oldState.IsKeyUp(Keys.NumPad4))
                {
                    if ((playerRectangle.Left >= Game1.border.Left + 1))
                    {
                        currentKey = Keys.NumPad4;
                    }

                }

                else if (newState.IsKeyDown(Keys.NumPad6) && oldState.IsKeyUp(Keys.NumPad6))
                {
                    if ((playerRectangle.Right <= Game1.border.Right - 1))
                    {
                        currentKey = Keys.NumPad6;
                    }
                }

                else if (newState.IsKeyDown(Keys.NumPad7) && oldState.IsKeyUp(Keys.NumPad7))
                {
                    if (playerRectangle.Left >= Game1.border.Left + 1 && playerRectangle.Top >= Game1.border.Top + 1)
                    {
                        foreach (Cell cell in Game1.level.Notwalkables)
                        {
                            if (playerRectangle.Y == cell.BoundingRectangle.Y + 100 && playerRectangle.X == cell.BoundingRectangle.X || playerRectangle.Y == cell.BoundingRectangle.Y && playerRectangle.X == cell.BoundingRectangle.X + 100)
                            {
                                dontMoveCorner = true;
                            }
                        }
                        if (dontMoveCorner == false)
                        {
                            currentKey = Keys.NumPad7;

                        }


                    }

                }

                else if (newState.IsKeyDown(Keys.NumPad8) && oldState.IsKeyUp(Keys.NumPad8))
                {
                    if ((playerRectangle.Top >= Game1.border.Top + 1))
                    {
                        currentKey = Keys.NumPad8;
                    }
                }

                else if (newState.IsKeyDown(Keys.NumPad9) && oldState.IsKeyUp(Keys.NumPad9))
                {
                    if (playerRectangle.Right <= Game1.border.Right - 1 && playerRectangle.Top >= Game1.border.Top + 1)
                    {
                        foreach (Cell cell in Game1.level.Notwalkables)
                        {
                            if (playerRectangle.Y == cell.BoundingRectangle.Y + 100 && playerRectangle.X == cell.BoundingRectangle.X || playerRectangle.Y == cell.BoundingRectangle.Y && playerRectangle.X == cell.BoundingRectangle.X - 100)
                            {
                                dontMoveCorner = true;

                            }
                        }
                        if (dontMoveCorner == false)
                        {
                            currentKey = Keys.NumPad9;
                        }
                    }
                }

                oldState = newState;
            }      
        }


        //player movement (keybinds)
        //checks is the cell is NotWalkable
        public Point Move(Keys pressedKey)
        {
            KeyboardState newState = Keyboard.GetState();

            if (pressedKey == Keys.NumPad1)
            {
                tmpposition.X -= 100;
                tmpposition.Y += 100;
                fogPosition.X -= 100;
                fogPosition.Y += 100;
                correctPathCheck = true;

                return tmpposition;
            }

            else if (pressedKey == Keys.NumPad2)
            {
                tmpposition.Y += 100;
                fogPosition.Y += 100;
                correctPathCheck = true;

                return tmpposition;
        }

            else if (pressedKey == Keys.NumPad3)
            {
                tmpposition.X += 100;
                tmpposition.Y += 100;
                    fogPosition.X += 100;
                    fogPosition.Y += 100;
                    correctPathCheck = true;

                    return tmpposition;
            }

            else if (pressedKey == Keys.NumPad4)
            {
                tmpposition.X -= 100;
                fogPosition.X -= 100;
                correctPathCheck = true;
   
                return tmpposition;
            }

            else if (pressedKey == Keys.NumPad6)
            {

                tmpposition.X += 100;
                fogPosition.X += 100;
                correctPathCheck = true;

                return tmpposition;
            }

            else if (pressedKey == Keys.NumPad7)
            {
                tmpposition.X -= 100;
                tmpposition.Y -= 100;
                fogPosition.X -= 100;
                fogPosition.Y -= 100;
                correctPathCheck = true;

                return tmpposition;
            }


            else if (pressedKey == Keys.NumPad8)
            {
                tmpposition.Y -= 100;
                fogPosition.Y -= 100;
                correctPathCheck = true;

                return tmpposition;
            }

            else if (pressedKey == Keys.NumPad9)
            {

                tmpposition.X += 100;
                tmpposition.Y -= 100;
                fogPosition.X += 100;
                fogPosition.Y -= 100;
                correctPathCheck = true;

                return tmpposition;
            }

            foreach (Cell cell in Game1.level.Notwalkables)
            {
                if (tmpposition.X == cell.MyPos.X * 100 && tmpposition.Y == cell.MyPos.Y * 100) 
                {
                        dontMove = true;
                        tmpposition = position;
                }
            }

            if (dontMove == false)
            {
                position = tmpposition;
                if (correctPathCheck)
                {
                    CorrectPath();
                }
                VisualManager.start.X = position.X / 100;
                VisualManager.start.Y = position.Y / 100;
            }

            // Moves player and fog
            playerRectangle.X = position.X;
            playerRectangle.Y = position.Y;
            fogRectangle.X = position.X - 1500;
            fogRectangle.Y = position.Y - 1500;

            // Creates new astar path after player have moved
            Game1.visualManager.FindPath();

            dontMove = false;
            correctPathCheck = false;
            oldState = newState;
            return position;

        }
        //checks is the player has moved to a square on the optimal path of the A*
        //if they have, they get 1 point added to their score
        //if not, they get 1 health subtracted from their total
        public void CorrectPath()
        {
                foreach (Node node in VisualManager.finalPath)
                {
                    if (node.Position.X == position.X / 100 && node.Position.Y == position.Y / 100)
                    {
                        score += 1;
                        correct = true;
                    }
                }
                if (correct == false)
                {
                    health -= 1;
                score -= 2;
                    DeathCheck();
                }

            correct = false;

        }
        //checks is the players health is below 0
        private void DeathCheck()
        {
            if (health <= 0)
            {
                isAlive = false;
                Game1.state = State.GAMEOVER;
            }
        }
        /// <summary>
        /// Loads / sets player and fog of war textures
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("GameTextures/player");
            fogSprite1 = content.Load<Texture2D>("GameTextures/fow1");
            fogSprite2 = content.Load<Texture2D>("GameTextures/fow2");
            fogSprite3 = content.Load<Texture2D>("GameTextures/fow3");
            fogSprite4 = content.Load<Texture2D>("GameTextures/fow4");
        }

        /// <summary>
        /// Draws player sprite and fow depending on lvlnumber
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,playerRectangle, color);

            if (Game1.level.LvlNumber > 1 && Game1.level.LvlNumber < 4)
            {
                spriteBatch.Draw(fogSprite4, fogRectangle, Color.White);
            }
            if (Game1.level.LvlNumber > 3 && Game1.level.LvlNumber < 7)
            {
                spriteBatch.Draw(fogSprite3, fogRectangle, Color.White);
            }
            if (Game1.level.LvlNumber == 7)
            {
                spriteBatch.Draw(fogSprite2, fogRectangle, Color.White);
            }


        }
        #endregion
    }
}
