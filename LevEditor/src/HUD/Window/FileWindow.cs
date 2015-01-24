using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevEditor
{
    /* TODO:
     * -Choose export location
     * -Export as .jpg or .png (for static backgrounds and/or just making pictures/designs)
     * -Export as a file to read in the data from (should it be editable outside of the program?)
     * -Save file
     * -Import/open existing file (will only work for non-.jpg or .png files)
     * -Import a spritesheet
     */
    class FileWindow : Window
    {
        public FileWindow(Vector2 position, Vector2 dimensions)
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
                System.Diagnostics.Debug.WriteLine("Exporting.");
                FileExporter.ExportCanvas();
            }

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
