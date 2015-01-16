using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevEditor
{
    public class Camera
    {
        protected Vector2 position;
        protected Matrix transform;
        protected Matrix inverseTransform;
        protected Viewport viewport;

        public static int ScreenResolutionWidth;
        public static int ScreenResolutionHeight;

        // The bounds of the width/height of the zone (px) minus
        // the width/height of the application's resolution, respectively.
        public static int ViewBoundsWidth;
        public static int ViewBoundsHeight;

        /// <summary>
        /// The position of the upper-left corner of the camera in world coordinates.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Matrix Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        public Matrix InverseTransform
        {
            get { return inverseTransform; }
            set { inverseTransform = value; }
        }

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            this.position = Vector2.Zero;
            ScreenResolutionWidth = viewport.Width;
            ScreenResolutionHeight = viewport.Height;
            ViewBoundsWidth = (Zone.ZoneWidth * Tile.TileWidth) - ScreenResolutionWidth;
            ViewBoundsHeight = (Zone.ZoneHeight * Tile.TileHeight) - ScreenResolutionHeight;
        }

        /// <summary>
        /// Converts where the user touches to world coordinates.
        /// </summary>
        public Vector2 ScreenToWorld(Vector2 touchPos)
        {
            return Vector2.Transform(touchPos, inverseTransform);
        }

        /// <summary>
        /// Converts world coordinates to screen coordinates.
        /// </summary>
        public Vector2 WorldToScreen(Vector2 worldPos)
        {
            return Vector2.Transform(worldPos, transform);
        }

        // Why not allow unrestricted panning, they just can't edit anything outside of the map they wanted to build?
        public void Update(GameTime gameTime)
        {
            transform = Matrix.CreateTranslation(-position.X, -position.Y, 0);
            inverseTransform = Matrix.Invert(transform);
        }

        /// <summary>
        /// Determines if a position is out of bounds.
        /// </summary>
        public static bool IsInWorld(Vector2 position)
        {
            if ((position.X < 0) || (position.Y < 0))
                return false;

            else if ((position.X > (Zone.ZoneWidth * Tile.TileWidth)) || (position.Y > (Zone.ZoneHeight * Tile.TileHeight)))
                return false;

            return true;
        }
    }
}
