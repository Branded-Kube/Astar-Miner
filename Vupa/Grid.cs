using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vupa
{
    class Grid
    {
        Texture2D texture;
        Rectangle rectangle;

        public Grid(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
    }
}
