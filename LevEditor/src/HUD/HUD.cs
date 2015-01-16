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
    class HUD
    {
        private AssetsWindow assetsWindow;
        private FileWindow fileWindow;
        private LayerEditWindow layerEditWindow;
        private ToolsWindow toolsWindow;

        public HUD()
        {
            fileWindow = new FileWindow(new Vector2(896, 0), new Vector2(256, 256));
            assetsWindow = new AssetsWindow(new Vector2(0, 256), new Vector2(256, 512));
            toolsWindow = new ToolsWindow(new Vector2(896, 256), new Vector2(256, 512));
            layerEditWindow = new LayerEditWindow(Vector2.Zero, new Vector2(256, 256));
        }

        public bool Contains(Vector2 position)
        {
            if (fileWindow.BackgroundRectangle.Contains(position) ||
                assetsWindow.BackgroundRectangle.Contains(position) ||
                layerEditWindow.BackgroundRectangle.Contains(position) ||
                toolsWindow.BackgroundRectangle.Contains(position))
                return true;

            return false;
        }

        public void Load(ContentManager content)
        {
            fileWindow.Load(content, "Test_Content/BackgroundFileIOW.png");
            assetsWindow.Load(content, "Test_Content/BackgroundAssetsW.png");
            toolsWindow.Load(content, "Test_Content/BackgroundToolsW.png");
            layerEditWindow.Load(content, "Test_Content/BackgroundLayerEditW.png");
        }

        public void Update(GameTime gameTime)
        {
            fileWindow.Update(gameTime);
            assetsWindow.Update(gameTime);
            toolsWindow.Update(gameTime);
            layerEditWindow.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Input.DisplayHUD)
            {
                fileWindow.Draw(spriteBatch);
                assetsWindow.Draw(spriteBatch);
                toolsWindow.Draw(spriteBatch);
                layerEditWindow.Draw(spriteBatch);
            }
        }
    }
}
