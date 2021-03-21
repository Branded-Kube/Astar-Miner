using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Vupa
{
    class Button
    {
        #region Fields
        private Color hoverColor;
        private Color currentColor;
        private MouseState mouseCurrent;
        private MouseState mouseLast;
        private Rectangle mouseRectangle;
        private Rectangle buttonRectangle;
        private readonly string buttonDescription;
        private Texture2D sprite;
        #endregion

        #region Events
        public event EventHandler Click;
        #endregion

        #region Constructor
        public Button(int positionX, int positionY, string buttonDescription, Texture2D texture2D)
        {
            hoverColor = Color.Gray;
            this.buttonRectangle = new Rectangle(positionX, positionY, buttonDescription.Length*8 + 20, 50);
            this.buttonDescription = buttonDescription;
            this.sprite = texture2D;
        }
        #endregion

        #region Methods
        public void Update()
        {
            mouseLast = mouseCurrent;
            mouseCurrent = Mouse.GetState();
            mouseRectangle = new Rectangle(mouseCurrent.X, mouseCurrent.Y, 1, 1);
            if (mouseRectangle.Intersects(buttonRectangle))
            {
                this.currentColor = hoverColor;
                if (mouseLast.LeftButton == ButtonState.Pressed && mouseCurrent.LeftButton == ButtonState.Released)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
            else
            {
                this.currentColor = Color.White;
            }

        }
        public void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(sprite, buttonRectangle, currentColor);
            var x = (buttonRectangle.X + (buttonRectangle.Width / 2)) - (Game1.font.MeasureString(buttonDescription).X / 2);
            var y = (buttonRectangle.Y + (buttonRectangle.Height / 2)) - (Game1.font.MeasureString(buttonDescription).Y / 2);
            _spriteBatch.DrawString(Game1.font, buttonDescription, new Vector2(x, y), Color.Black);
        }
        #endregion
    }
}
