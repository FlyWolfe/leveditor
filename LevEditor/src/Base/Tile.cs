using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevEditor
{
    static class Tile
    {
        public static Texture2D TilesetTexture;

        public const int TileWidth = 32;
        public const int TileHeight = 32;

        /// <summary>
        /// Given an index, returns the corresponding rectangle on the spritesheet.
        /// </summary>
        public static Rectangle GetSourceRectangle(int tileIndex)
        {
            int tileX = tileIndex % (TilesetTexture.Width / TileWidth);
            int tileY = tileIndex / (TilesetTexture.Width / TileWidth);

            return new Rectangle(tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight);
        }
    }
}
