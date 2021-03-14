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
        private Texture2D fogSprite;
        Color color = Color.Black;
        Rectangle playerRectangle;
        Rectangle fogRectangle;
        Point size;
        Point fogSize;
        KeyboardState oldState;
        Point position;
        Point fogPosition;
        int xborder = 900;
        int yborder = 800;

        public Player(Point start)
        {
            this.position = start;
            this.fogPosition = new Point(-1500, -1500);
            this.size = new Point(100,100);
            this.fogSize = new Point(3098, 3098);

         
        }
        public void Update()
        {
            move();
            this.playerRectangle = new Rectangle(position, size);
            this.fogRectangle = new Rectangle(fogPosition, fogSize);

        }

        public void move()
        {

            KeyboardState newState = Keyboard.GetState();
            if (playerRectangle.Top >= Game1.border.Top +1)
            {
                if (newState.IsKeyDown(Keys.NumPad8) && oldState.IsKeyUp(Keys.NumPad8))
                {
                    position.Y -= 100;
                    fogPosition.Y -= 100;
                }
              
            }
            if (playerRectangle.Bottom <= Game1.border.Bottom -1)
            {
                if (newState.IsKeyDown(Keys.NumPad2) && oldState.IsKeyUp(Keys.NumPad2))
                {
                    position.Y += 100;
                    fogPosition.Y += 100;
                }
            }
            if (playerRectangle.Left >= Game1.border.Left + 1)
            {
                if (newState.IsKeyDown(Keys.NumPad4) && oldState.IsKeyUp(Keys.NumPad4))
                {
                    position.X -= 100;
                    fogPosition.X -= 100;
                }
            }
            if (playerRectangle.Right <= Game1.border.Right - 1)
            {
                if (newState.IsKeyDown(Keys.NumPad6) && oldState.IsKeyUp(Keys.NumPad6))
                {
                    position.X += 100;
                    fogPosition.X += 100;
                }
            }
            if (playerRectangle.Left >= Game1.border.Left + 1 && playerRectangle.Top >= Game1.border.Top + 1)
            {
                if (newState.IsKeyDown(Keys.NumPad7) && oldState.IsKeyUp(Keys.NumPad7))
                {
                    position.X -= 100;
                    position.Y -= 100;
                    fogPosition.X -= 100;
                    fogPosition.Y -= 100;

                }
            }
            if (playerRectangle.Right <= Game1.border.Right - 1 && playerRectangle.Bottom <= Game1.border.Bottom - 1)
            {
                if (newState.IsKeyDown(Keys.NumPad3) && oldState.IsKeyUp(Keys.NumPad3))
                {
                    position.X += 100;
                    position.Y += 100;
                    fogPosition.X += 100;
                    fogPosition.Y += 100;

                }
            }
            if (playerRectangle.Right <= Game1.border.Right - 1 && playerRectangle.Top >= Game1.border.Top + 1)
            {
                if (newState.IsKeyDown(Keys.NumPad9) && oldState.IsKeyUp(Keys.NumPad9))
                {
                    position.X += 100;
                    position.Y -= 100;
                    fogPosition.X += 100;
                    fogPosition.Y -= 100;
                }
            }
            if (playerRectangle.Left >= Game1.border.Left + 1 && playerRectangle.Bottom <= Game1.border.Bottom - 1)
            {
                if (newState.IsKeyDown(Keys.NumPad1) && oldState.IsKeyUp(Keys.NumPad1))
                {
                    position.X -= 100;
                    position.Y += 100;
                    fogPosition.X -= 100;
                    fogPosition.Y += 100;
                }
            }
            oldState = newState;

        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("worker");
            fogSprite = content.Load<Texture2D>("fogwar");

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,playerRectangle, color);
            spriteBatch.Draw(fogSprite, fogRectangle, Color.White);
        }

    }
}
