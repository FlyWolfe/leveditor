using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevEditor
{
    // Change to interface; windows ARE A window, they do not HAVE A window.
    class Window
    {
        protected Texture2D BackgroundTexture;

        public Rectangle BackgroundRectangle;

        public Window(Vector2 position, Vector2 dimensions)
        {
            BackgroundRectangle = new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
        }

        public void Load(ContentManager content, string fileName)
        {
            BackgroundTexture = content.Load<Texture2D>(fileName);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, Color.White);
        }
    }
}
