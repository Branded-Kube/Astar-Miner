//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using System.Collections.Generic;
//using System.Text;

namespace Vupa
{
    class Grid
    {
        private VisualManager visualManager;

        Texture2D texture;
        Rectangle rectangle;

        public Grid(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
            //visualManager = new VisualManager(, this.DisplayRectangle);

        }

//        public void Draw(SpriteBatch spriteBatch, Vector2 position)
//        {
//            if (texture != null)
//            {
//                spriteBatch.Draw(texture, position, Color.White);
//            }
//        }
//    }
//}
