using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Vupa
{
    class Player
    {
        Texture2D sprite;
        Color color = Color.Black;
        Rectangle playerRectangle;
        Point size;
        KeyboardState oldState;
        Point position;
        int score = 0;
        bool correct;

        public Player(Point start)
        {
            this.position = start;
            this.size = new Point(100,100);
            this.playerRectangle = new Rectangle(position, size);


        }
        public void Update()
        {
            move();
            this.playerRectangle.X = position.X;
            this.playerRectangle.Y = position.Y;


        }

        public void move()
        {

            KeyboardState newState = Keyboard.GetState();
            if (playerRectangle.Top >= Game1.border.Top +1)
            {
                if (newState.IsKeyDown(Keys.NumPad8) && oldState.IsKeyUp(Keys.NumPad8))
                {
                    position.Y -= 100;
                    CorrectPath();

                }

            }
            if (playerRectangle.Bottom <= Game1.border.Bottom -1)
            {
                if (newState.IsKeyDown(Keys.NumPad2) && oldState.IsKeyUp(Keys.NumPad2))
                {
                    position.Y += 100;
                    CorrectPath();

                }
            }
            if (playerRectangle.Left >= Game1.border.Left + 1)
            {
                if (newState.IsKeyDown(Keys.NumPad4) && oldState.IsKeyUp(Keys.NumPad4))
                {
                    position.X -= 100;
                    CorrectPath();

                }
            }
            if (playerRectangle.Right <= Game1.border.Right - 1)
            {
                if (newState.IsKeyDown(Keys.NumPad6) && oldState.IsKeyUp(Keys.NumPad6))
                {
                    position.X += 100;
                    CorrectPath();

                }
            }
            if (playerRectangle.Left >= Game1.border.Left + 1 && playerRectangle.Top >= Game1.border.Top + 1)
            {
                if (newState.IsKeyDown(Keys.NumPad7) && oldState.IsKeyUp(Keys.NumPad7))
                {
                    position.X -= 100;
                    position.Y -= 100;
                    CorrectPath();

                }
            }
            if (playerRectangle.Right <= Game1.border.Right - 1 && playerRectangle.Bottom <= Game1.border.Bottom - 1)
            {
                if (newState.IsKeyDown(Keys.NumPad3) && oldState.IsKeyUp(Keys.NumPad3))
                {
                    position.X += 100;
                    position.Y += 100;
                    CorrectPath();

                }
            }
            if (playerRectangle.Right <= Game1.border.Right - 1 && playerRectangle.Top >= Game1.border.Top + 1)
            {
                if (newState.IsKeyDown(Keys.NumPad9) && oldState.IsKeyUp(Keys.NumPad9))
                {
                    position.X += 100;
                    position.Y -= 100;
                    CorrectPath();

                }
            }
            if (playerRectangle.Left >= Game1.border.Left + 1 && playerRectangle.Bottom <= Game1.border.Bottom - 1)
            {
                if (newState.IsKeyDown(Keys.NumPad1) && oldState.IsKeyUp(Keys.NumPad1))
                {
                    position.X -= 100;
                    position.Y += 100;
                    CorrectPath();


                }
            }
            oldState = newState;
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

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,playerRectangle, color);
            spriteBatch.DrawString(Game1.font,$"Score: {score}", new Vector2(1050, 500), Color.Black);

        }

    }
}
