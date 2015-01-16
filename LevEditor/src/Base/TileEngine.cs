using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevEditor
{
    /* TODO: Change the layer loop to be a for instead of foreach to account for layer transparency;
     * e.g. checking if layer i has a particular transparency to be multiplied by.
     * 
     * TODO: Add a final texture at the top to serve as a grid for the tiles;
     * difficult to see exactly where you're placing a tile with touch.
     * 
     * TODO: Consider switching 2D array to a dictionary?
     */
    class TileEngine
    {
        // So we only render MxM tiles for a NxN zone where M <= N.
        private int visibleSurfaceRenderWidth = 50;
        private int visibleSurfaceRenderHeight = 50;

        public void Draw(SpriteBatch spriteBatch, Vector2 startPosition)
        {
            // Position of the first tile we render.
            int firstX = (int)(startPosition.X / Tile.TileWidth);
            int firstY = (int)(startPosition.Y / Tile.TileHeight);

            // Offset as the camera won't always be at a position evenly divisble by TileWidth or TileHeight.
            int offsetX = (int)(startPosition.X % Tile.TileWidth);
            int offsetY = (int)(startPosition.Y % Tile.TileHeight);

            for (int i = 0; i < visibleSurfaceRenderHeight; ++i)
            {
                // Bounds checking to make sure we aren't rendering outside the 2D array.
                if ((i + firstY >= 0) && (i + firstY < Zone.ZoneHeight))
                {
                    for (int j = 0; j < visibleSurfaceRenderWidth; ++j)
                    {
                        // More bounds checking.
                        if ((j + firstX >= 0) && (j + firstX < Zone.ZoneWidth))
                        {
                            int row = i + firstY;
                            int col = j + firstX;
                            // We render each tile in each layer at each position in ascending order.
                            for (int layer = 0; layer < Zone.Canvas[row, col].Count; ++layer)
                            {
                                spriteBatch.Draw(Tile.TilesetTexture,
                                                 new Rectangle((j * Tile.TileWidth) - offsetX,
                                                               (i * Tile.TileHeight) - offsetY,
                                                               Tile.TileWidth,
                                                               Tile.TileHeight),
                                                 Tile.GetSourceRectangle(Zone.Canvas[row, col][layer]),
                                                 Color.White * LayerEditWindow.LayerIndexButtons[layer].Transparency);
                            }
                        }
                    }
                }
            }
        }
    }
}
