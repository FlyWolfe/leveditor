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
    class AssetsWindow : Window
    {
        // Used for optimizing sprite button rendering.
        private Point startButtonRender;
        private Point endButtonRender;
        private int firstRowRenderID;
        private int lastRowRenderID;
        private const bool MoveIndexLower = true;
        private const bool MoveIndexHigher = false;

        private Dictionary<int, List<Button>> spriteDictionary;

        public static int SelectedTile;

        public AssetsWindow(Vector2 position, Vector2 dimensions)
            : base(position, dimensions)
        {
            startButtonRender = new Point(16, (int)position.Y - 16);
            endButtonRender = new Point(16, (int)position.Y + (int)dimensions.Y + 48);
            spriteDictionary = new Dictionary<int, List<Button>>();
            SelectedTile = 0;
        }

        /// <summary>
        /// Loads the background texture from filePath.
        /// </summary>
        new public void Load(ContentManager content, string backgroundFilePath)
        {
            base.Load(content, backgroundFilePath);

            int tileID = 0; // Keep track of our current tileID.
            int numButtonsAcross = (int)Math.Floor((double)(BackgroundTexture.Width / (Tile.TileWidth + 1))); // 1px for border
            int totalNumButtons = (Tile.TilesetTexture.Width / Tile.TileWidth) * (Tile.TilesetTexture.Height / Tile.TileHeight); // Rectangular spritesheet
            int numButtonsDown = (int)Math.Ceiling(((double)totalNumButtons / (double)numButtonsAcross));

            for (int i = 0; i < numButtonsDown; ++i)
            {
                List<Button> tmpValues = new List<Button>();

                /* Check tileID in the instance that the last row can fit more tiles
                 * than exist on the rest of the spritesheet.
                 */
                for (int j = 0; j < numButtonsAcross && tileID < totalNumButtons; ++j)
                {
                    // We add 1 to the x and y coordinate to act as a border between tiles.
                    tmpValues.Add(new Button(new Rectangle((j * (Tile.TileWidth + 1)) + BackgroundRectangle.Location.X,
                                                           (i * (Tile.TileHeight + 1)) + BackgroundRectangle.Location.Y,
                                                           Tile.TileWidth,
                                                           Tile.TileHeight),
                                             tileID, j, i));
                    ++tileID;
                }

                spriteDictionary.Add(i, tmpValues);
            }

            firstRowRenderID = 0;
            lastRowRenderID = spriteDictionary.Count - 1; // Temp test value for now.
        }

        new public void Update(GameTime gameTime)
        {
            if (Input.DisplayHUD)
            {
                if (BackgroundRectangle.Contains(Input.GesturePosition))
                {
                    if (Input.Tap != new Vector2(-1, -1) && BackgroundRectangle.Contains(Input.Tap))
                    {
                        SelectedTile = SelectNewTile(Input.Tap);
                    }

                    if (Input.Drag.Y != 0)
                        Slide((int)Input.Drag.Y);
                }
            }

            base.Update(gameTime);
        }

        new public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            for (int i = firstRowRenderID; i < lastRowRenderID; ++i)
            {
                foreach (Button button in spriteDictionary[i])
                {
                    spriteBatch.Draw(Tile.TilesetTexture, button.Rect, Tile.GetSourceRectangle(button.ID), Color.White);
                }
            }
        }

        private int SelectNewTile(Vector2 tap)
        {
            Vector2 tapY = new Vector2(Tile.TileWidth / 2, tap.Y); // Arbitrary x-value we know will be in the first column.

            for (int i = firstRowRenderID; i < lastRowRenderID; ++i)
            {
                if (spriteDictionary[i][0].Rect.Contains(tapY))
                {
                    foreach (Button button in spriteDictionary[i])
                    {
                        if (button.Rect.Contains(tap))
                        {
                            return button.ID;
                        }
                    }
                }
            }

            return 0;
        }

        private void Slide(int delta)
        {
            if (delta > 0)
            {
                int topButtonBoundary = BackgroundRectangle.Top - spriteDictionary[0][0].Rect.Top;
                if (topButtonBoundary > 0)
                {
                    /* If we try to move the buttons more than the amount of space that exists
                     * between the top button and the top of the window, we cap it so it only
                     * moves as far as the top of the window.
                     */
                    if (delta > topButtonBoundary)
                        delta = topButtonBoundary;

                    /* Can't modify the return value of b.Rect.Location, so we
                     * unfortunately have to iterate through a bunch of method calls.
                     */
                    for (int i = 0; i < spriteDictionary.Count; ++i)
                    {
                        foreach (Button b in spriteDictionary[i])
                            b.ChangeYPosition(delta);
                    }

                    // First and last ID will be at a lower value than they previously were.
                    firstRowRenderID = FirstRowToRender(MoveIndexLower);
                    lastRowRenderID = LastRowToRender(MoveIndexLower);
                }
            }

            if (delta < 0)
            {
                int lastIndex = spriteDictionary.Count - 1;
                int bottomButtonBoundary = BackgroundRectangle.Bottom - spriteDictionary[lastIndex][spriteDictionary[lastIndex].Count - 1].Rect.Bottom;

                if (bottomButtonBoundary < 0)
                {
                    if (delta < bottomButtonBoundary)
                        delta = bottomButtonBoundary;

                    for (int i = 0; i < spriteDictionary.Count; ++i)
                    {
                        foreach (Button b in spriteDictionary[i])
                            b.ChangeYPosition(delta);
                    }

                    // First and last ID will be at a higher value than they previously were.
                    firstRowRenderID = FirstRowToRender(MoveIndexHigher);
                    lastRowRenderID = LastRowToRender(MoveIndexHigher);
                }
            }
        }

        /// <summary>
        /// Returns the index of the top button we are to render.
        /// </summary>
        private int FirstRowToRender(bool moveIndex)
        {
            if (moveIndex == MoveIndexLower)
            {
                /* We iterate down from our previous button we started the render at, since
                 * if the index is going to be lower then so will our starting button.
                 */ 
                for (int i = firstRowRenderID; i > 0; --i)
                {
                    if (spriteDictionary[i][0].Rect.Contains(startButtonRender))
                        return i;
                }
            }

            else
            {
                /* We iterate up from the previous button we rendered at since the index
                 * must otherwise be higher if it is not lower.
                 */ 
                for (int i = firstRowRenderID; i < spriteDictionary.Count - 1; ++i)
                {
                    if (spriteDictionary[i][0].Rect.Contains(startButtonRender))
                        return i;
                }
            }

            return 0;
        }

        /// <summary>
        /// Returns the index of the bottom button we are to render.
        /// </summary>
        private int LastRowToRender(bool moveIndex)
        {
            if (moveIndex == MoveIndexLower)
            {
                for (int i = lastRowRenderID; i > 0; --i)
                {
                    if (spriteDictionary[i][0].Rect.Contains(endButtonRender))
                        return i;
                }
            }

            else
            {
                for (int i = lastRowRenderID; i < spriteDictionary.Count - 1; ++i)
                {
                    if (spriteDictionary[i][0].Rect.Contains(endButtonRender))
                        return i;
                }
            }

            return spriteDictionary.Count - 1;
        }
    }
}
