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
        private ToolsWindow ToolsWindow;
        private FileWindow fileWindow;
        private LayerEditWindow layerEditWindow;
        private TileEditWindow tileEditWindow;

        public HUD()
        {
            fileWindow = new FileWindow(new Vector2(896, 0), new Vector2(256, 256));
            assetsWindow = new AssetsWindow(new Vector2(0, 256), new Vector2(256, 512));
            tileEditWindow = new TileEditWindow(new Vector2(896, 256), new Vector2(256, 512));
            layerEditWindow = new LayerEditWindow(Vector2.Zero, new Vector2(256, 256));
            ToolsWindow = new ToolsWindow(new Vector2(256, 0), new Vector2(640, 192));
        }

        public bool Contains(Vector2 position)
        {
            if (fileWindow.BackgroundRectangle.Contains(position) ||
                assetsWindow.BackgroundRectangle.Contains(position) ||
                tileEditWindow.BackgroundRectangle.Contains(position) ||
                layerEditWindow.BackgroundRectangle.Contains(position) ||
                ToolsWindow.BackgroundRectangle.Contains(position))
                return true;

            return false;
        }

        public void Load(ContentManager content)
        {
            fileWindow.Load(content, "Test_Content/BackgroundFileIOW.png");
            assetsWindow.Load(content, "Test_Content/BackgroundAssetsW.png");
            tileEditWindow.Load(content, "Test_Content/BackgroundTileEditW.png");
            layerEditWindow.Load(content, "Test_Content/BackgroundLayerEditW.png");
            ToolsWindow.Load(content, "Test_Content/BackgroundEntityEditW.png");
        }

        public void Update(GameTime gameTime)
        {
            fileWindow.Update(gameTime);
            assetsWindow.Update(gameTime);
            tileEditWindow.Update(gameTime);
            layerEditWindow.Update(gameTime);
            ToolsWindow.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Input.DisplayHUD)
            {
                fileWindow.Draw(spriteBatch);
                assetsWindow.Draw(spriteBatch);
                tileEditWindow.Draw(spriteBatch);
                layerEditWindow.Draw(spriteBatch);
                ToolsWindow.Draw(spriteBatch);
            }
        }
    }
}
