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
        private Point tmpposition;

        int health = 5;
        public int score;
        bool correct;
        Point fogPosition;
        bool dontMove;

        public Keys currentKey;
        public Keys oldKey;

        bool correctPathCheck = false;

        public bool isAlive = true;

        KeyboardState newState;

        

        public Player(Point start)
        {
            this.tmpposition = new Point(start.X *100, start.Y*100);
            this.fogPosition = new Point(position.X -1500, position.Y -1500);
            this.size = new Point(100, 100);
            this.playerRectangle = new Rectangle(position, size);
            this.fogSize = new Point(3098, 3098);
            this.fogRectangle = new Rectangle(fogPosition, fogSize);


            //VisualManager.start.X = position.X / 100;
            //VisualManager.start.Y = position.Y / 100;
        }
        public void Update()
        {
            DeathCheck();
            CheckState();


            Move(currentKey);
            currentKey = Keys.None;
            this.playerRectangle.X = position.X;
            this.playerRectangle.Y = position.Y;
            this.fogRectangle.X = position.X -1500;
            this.fogRectangle.Y = position.Y - 1500;

        }

        public void CheckState()
        {
            newState = Keyboard.GetState();

            if (isAlive == true)
            {
                if (newState.IsKeyDown(Keys.NumPad1) && oldState.IsKeyUp(Keys.NumPad1))
                {
                    if (playerRectangle.Left >= Game1.border.Left + 1 && playerRectangle.Bottom <= Game1.border.Bottom - 1)
                    {
                        currentKey = Keys.NumPad1;
                    }
                }

                if (newState.IsKeyDown(Keys.NumPad2) && oldState.IsKeyUp(Keys.NumPad2))
                {
                    //foreach (Cell cell in Game1.level.Notwalkables)
                    //{
                    //    if (playerRectangle.Bottom == cell.BoundingRectangle.Top && playerRectangle.X == cell.BoundingRectangle.X && playerRectangle.Y +100== cell.BoundingRectangle.Y)
                    //    {

                    //    }
                    //    else if (playerRectangle.Bottom <= Game1.border.Bottom - 1)
                    //    {
                    //        currentKey = Keys.NumPad2;

                    //    }

                    //}

                    if (playerRectangle.Bottom <= Game1.border.Bottom - 1)
                    {
                        currentKey = Keys.NumPad2;
                    }

                }

                if (newState.IsKeyDown(Keys.NumPad3) && oldState.IsKeyUp(Keys.NumPad3))
                {
                    if (playerRectangle.Right <= Game1.border.Right - 1 && playerRectangle.Bottom <= Game1.border.Bottom - 1)
                    {
                        currentKey = Keys.NumPad3;
                    }

                }

                if (newState.IsKeyDown(Keys.NumPad4) && oldState.IsKeyUp(Keys.NumPad4))
                {
                    if ((playerRectangle.Left >= Game1.border.Left + 1))
                    {
                        currentKey = Keys.NumPad4;
                    }

                }

                if (newState.IsKeyDown(Keys.NumPad6) && oldState.IsKeyUp(Keys.NumPad6))
                {
                    if ((playerRectangle.Right <= Game1.border.Right - 1))
                    {
                        currentKey = Keys.NumPad6;
                    }

                }

                if (newState.IsKeyDown(Keys.NumPad7) && oldState.IsKeyUp(Keys.NumPad7))
                {
                    if (playerRectangle.Left >= Game1.border.Left + 1 && playerRectangle.Top >= Game1.border.Top + 1)
                    {
                        currentKey = Keys.NumPad7;
                    }


                }

                if (newState.IsKeyDown(Keys.NumPad8) && oldState.IsKeyUp(Keys.NumPad8))
                {
                    if ((playerRectangle.Top >= Game1.border.Top + 1))
                    {
                        currentKey = Keys.NumPad8;
                    }
                }

                if (newState.IsKeyDown(Keys.NumPad9) && oldState.IsKeyUp(Keys.NumPad9))
                {
                    if (playerRectangle.Right <= Game1.border.Right - 1 && playerRectangle.Top >= Game1.border.Top + 1)
                    {
                        currentKey = Keys.NumPad9;
                    }
                }

                oldState = newState;
            }


            
        }


        
        public Point Move(Keys pressedKey)
        {
            KeyboardState newState = Keyboard.GetState();

            //dontMove = false;

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
            }
            dontMove = false;
            VisualManager.start.X = position.X / 100;
            VisualManager.start.Y = position.Y / 100;




            Game1.visualManager.FindPath();
            Game1.visualManager.LoadContent(Game1.content);

            correctPathCheck = false;
            oldState = newState;
            return position;

        }

        public void CorrectPath()
        {
           
                foreach (Node node in VisualManager.finalPath)
                {
                    if (node.Position.X == position.X / 100 && node.Position.Y == position.Y / 100)
                    {
                        Debug.WriteLine("plus");
                        health += 1;
                        score += 1;
                        correct = true;
                    }
                }
                if (correct == false)
                {
                    health -= 2;
                    Debug.WriteLine("minus");
                }

            correct = false;

        }

        private void DeathCheck()
        {
            if (health <= 0)
            {
                isAlive = false;
            }
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("worker");
            fogSprite = content.Load<Texture2D>("fogwar2");
            healthBox = content.Load<Texture2D>("textbox2");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,playerRectangle, color);

            if (Game1.level.LvlNumber == 2)
            {
               spriteBatch.Draw(fogSprite, fogRectangle, Color.White);
            }
            if (isAlive == true)
            {
                spriteBatch.Draw(healthBox, new Vector2(1098, 532), Color.White);
                spriteBatch.DrawString(Game1.font, $"health: {health}", new Vector2(1100, 540), Color.LightGreen);
            }
          
            

        }

    }
}
