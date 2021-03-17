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
        private Texture2D scoreBox;
        Color color = Color.Black;
        Rectangle playerRectangle;
        Rectangle fogRectangle;
        Point size;
        Point fogSize;
        KeyboardState oldState;
        public Point position;
        int score = 0;
        int lvlNumber = 2;
        bool correct;
        Point fogPosition;

        public Keys currentKey;

        public Keys oldKey;

        bool correctPathCheck = false;

        KeyboardState newState;

        

        public Player(Point start)
        {
            this.position = new Point(start.X *100, start.Y*100);
            this.fogPosition = new Point(position.X -1500, position.Y -1500);

            this.size = new Point(100, 100);
            this.playerRectangle = new Rectangle(position, size);
            this.fogSize = new Point(3098, 3098);
            this.fogRectangle = new Rectangle(fogPosition, fogSize);


        }
        public void Update()
        {

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

            Debug.WriteLine(currentKey);

            //  currentKey = newState.

            if (newState.IsKeyDown(Keys.NumPad1) && oldState.IsKeyUp(Keys.NumPad1))
            {
                if (playerRectangle.Left >= Game1.border.Left + 1 && playerRectangle.Bottom <= Game1.border.Bottom - 1)
                {
                    currentKey = Keys.NumPad1;
                }
            }

            if (newState.IsKeyDown(Keys.NumPad2) && oldState.IsKeyUp(Keys.NumPad2))
            {
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
            //  oldKey = currentKey;


            
        }


        
        public Point Move(Keys pressedKey)
        {
            KeyboardState newState = Keyboard.GetState();

            

            if (pressedKey == Keys.NumPad1)
            {
                position.X -= 100;
                position.Y += 100;
                fogPosition.X -= 100;
                fogPosition.Y += 100;
                correctPathCheck = true;

                return position;
                    
                
            }

            else if (pressedKey == Keys.NumPad2)
            {
                position.Y += 100;
                fogPosition.Y += 100;
                correctPathCheck = true;

                return position;
        }

            else if (pressedKey == Keys.NumPad3)
            {
                    position.X += 100;
                    position.Y += 100;
                    fogPosition.X += 100;
                    fogPosition.Y += 100;
                    correctPathCheck = true;

                    return position;
            }

            else if (pressedKey == Keys.NumPad4)
            {
                position.X -= 100;
                fogPosition.X -= 100;
                correctPathCheck = true;
   
                return position;
            }

            else if (pressedKey == Keys.NumPad6)
            {

                position.X += 100;
                fogPosition.X += 100;
                correctPathCheck = true;

                return position;
            }

            else if (pressedKey == Keys.NumPad7)
            {
                position.X -= 100;
                position.Y -= 100;
                fogPosition.X -= 100;
                fogPosition.Y -= 100;
                correctPathCheck = true;

                return position;
            }



            else if (pressedKey == Keys.NumPad8)
            {
                position.Y -= 100;
                fogPosition.Y -= 100;
                correctPathCheck = true;

                return position;
            }

            else if (pressedKey == Keys.NumPad9)
            {

                position.X += 100;
                position.Y -= 100;
                fogPosition.X += 100;
                fogPosition.Y -= 100;
                correctPathCheck = true;

                return position;
            }

            if (correctPathCheck)
            {
                CorrectPath();
            }

            
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
                        score += 50;
                        correct = true;
                    }
                }
                if (correct == false)
                {
                    score -= 50;
                    Debug.WriteLine("minus");
                }

            correct = false;

        }
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("worker");
            fogSprite = content.Load<Texture2D>("fogwar2");
            scoreBox = content.Load<Texture2D>("textbox2");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,playerRectangle, color);
            if (lvlNumber > 1)
            {
                spriteBatch.Draw(fogSprite, fogRectangle, Color.White);

            }
            spriteBatch.Draw(scoreBox, new Vector2(1098, 532), Color.White);
            spriteBatch.DrawString(Game1.font,$"Score: {score}", new Vector2(1100, 540), Color.LightGreen);

        }

    }
}
