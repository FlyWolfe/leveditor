﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevEditor
{
    class ToolsWindow : Window
    {
        public static bool DisplayGrid = false;

        public ToolsWindow(Vector2 position, Vector2 dimensions)
            : base(position, dimensions)
        {
        }

        public void Load(ContentManager content, string filePath)
        {
            base.Load(content, filePath);
        }

        public void Update(GameTime gameTime)
        {
            if (BackgroundRectangle.Contains(Input.Tap))
            {
                DisplayGrid = (DisplayGrid) ? false : true;
            }

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
