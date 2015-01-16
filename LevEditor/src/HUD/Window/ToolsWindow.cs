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
    /* TODO:
     * -Undo
     * -Redo
     * -Paint-grab/smart-grab
     * -Show/hide grid
     */
    class ToolsWindow : Window
    {
        public ToolsWindow(Vector2 position, Vector2 dimensions) : base(position, dimensions)
        {
        }

        new public void Load(ContentManager content, string backgroundFilePath)
        {
            base.Load(content, backgroundFilePath);
        }

        new public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        new public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
