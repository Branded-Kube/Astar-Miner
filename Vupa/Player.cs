 using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Vupa
{
    public class Player
    {
        #region Fields & Properties
        Texture2D sprite;
        private Texture2D fogSprite;
        private Texture2D healthBox;
        Color color = Color.Black;
        Rectangle playerRectangle;
        Rectangle fogRectangle;
        Point size;
        Point fogSize;
        KeyboardState oldState;
        public Point position;
        public Point tmpposition;
        public int Health {get {return health;} set { health = value; } }
        int health;
        public int score;
        public string name;
        public string Name { get { return name; } set { name = value; } }
        bool correct;
        Point fogPosition;
        bool dontMove;
        bool dontMove1 = false;


        public Keys currentKey;
        public Keys oldKey;

        bool correctPathCheck = false;

        public bool isAlive;

        KeyboardState newState;
        #endregion

        #region Constructor
        public Player(Point start)
        {
            this.tmpposition = new Point(start.X *100, start.Y*100);
            this.fogPosition = new Point(position.X -1500, position.Y -1500);
            this.size = new Point(100, 100);
            this.playerRectangle = new Rectangle(position, size);
            this.fogSize = new Point(3098, 3098);
            this.fogRectangle = new Rectangle(fogPosition, fogSize);
            position = tmpposition;

        }
        #endregion

        #region Methods
        public void Update()
        {
            CheckState();

            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {

                Move(currentKey);
            }

            currentKey = Keys.None;
            this.playerRectangle.X = position.X;
            this.playerRectangle.Y = position.Y;
            this.fogRectangle.X = position.X -1500;
            this.fogRectangle.Y = position.Y - 1500;

        }

        // Brugt til at tjekke hvilken tast er trykket på,.

        public void CheckState()
        {
            newState = Keyboard.GetState();
            dontMove1 = false;

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
                                dontMove1 = true;
                            }
                        }
                        if (dontMove1 == false)
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
                                dontMove1 = true;
                            }
                        }
                        if (dontMove1 == false)
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
                                dontMove1 = true;
                            }
                        }
                        if (dontMove1 == false)
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
                                dontMove1 = true;

                            }
                        }
                        if (dontMove1 == false)
                        {
                            currentKey = Keys.NumPad9;
                        }
                    }
                }

                oldState = newState;
            }      
        }


        //player movement (keybinds)
        //checks is the tile is NotWalkable
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
            dontMove = false;

            Game1.visualManager.FindPath();

            correctPathCheck = false;
            oldState = newState;
            return tmpposition;

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
                        Debug.WriteLine("plus");
                        score += 1;
                        correct = true;
                    }
                }
                if (correct == false)
                {
                    health -= 1;
                    Debug.WriteLine("minus");
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

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("GameTextures/worker");
            fogSprite = content.Load<Texture2D>("GameTextures/fogwar2");
            healthBox = content.Load<Texture2D>("GameTextures/textbox2");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,playerRectangle, color);

            if (Game1.level.LvlNumber > 1)
            {
                spriteBatch.Draw(fogSprite, fogRectangle, Color.White);

            }
           
          
        }
        #endregion
    }
}
