using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;

namespace LevEditor
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Camera camera;
        private TileEngine tileEngine;
        private HUD hud;

        // 3:2 aspect ratio.
        private Vector3 screenScale;
        private Matrix scaleMatrix;
        public const int VirtualScreenWidth = 1152;
        public const int VirtualScreenHeight = 768;
        public static Vector3 ScreenScale;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;

            Content.RootDirectory = "Content";

            // Setting a temporary value for now.
            Zone.ZoneWidth = 100;
            Zone.ZoneHeight = 100;

            TouchPanel.EnabledGestures = GestureType.FreeDrag | GestureType.Tap;
            this.IsMouseVisible = true;
            this.IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            Zone.Initialize();
            camera = new Camera(GraphicsDevice.Viewport);

            // Scale our screen appropriately.
            float scaleX = (float)GraphicsDevice.Viewport.Width / (float)VirtualScreenWidth;
            float scaleY = (float)GraphicsDevice.Viewport.Height / (float)VirtualScreenHeight;

            // Can move to Update(GameTime) to support windowed mode.
            screenScale = new Vector3(scaleX, scaleY, 1.0f);
            ScreenScale = screenScale;
            scaleMatrix = Matrix.CreateScale(screenScale);

            tileEngine = new TileEngine();
            hud = new HUD();
            Input.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Tile.TilesetTexture = Content.Load<Texture2D>("Tilesheet.png");
            Tile.GridTexture = Content.Load<Texture2D>("Grid.png");
            hud.Load(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here.
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update(gameTime);

            if (Input.Tap != new Vector2(-1, -1))
            {
                if (!hud.Contains(Input.Tap) || !Input.DisplayHUD)
                {
                    Vector2 worldPosition = camera.ScreenToWorld(Input.Tap);

                    if (Camera.IsInWorld(worldPosition))
                    {
                        int x = (int)(worldPosition.X / Tile.TileWidth);
                        int y = (int)(worldPosition.Y / Tile.TileHeight);

                        // Check to make sure that we have the selected layer before editing it.
                        if (Zone.Canvas[y, x].Count - 1 >= LayerEditWindow.SelectedLayer)
                        {
                            Zone.Canvas[y, x][LayerEditWindow.SelectedLayer] = AssetsWindow.SelectedTile;
                        }

                        else // If we don't have the layer, we need to add blank layers up to the selected one.
                        {
                            for (int i = Zone.Canvas[y, x].Count; i <= LayerEditWindow.SelectedLayer; ++i)
                            {
                                Zone.Canvas[y, x].Add(i);
                            }

                            Zone.Canvas[y, x][LayerEditWindow.SelectedLayer] = AssetsWindow.SelectedTile;
                        }
                    }
                }
            }

            if ((!hud.Contains(Input.GesturePosition) || !Input.DisplayHUD) && Input.GesturePosition != new Vector2(-1, -1))
            {
                camera.Position = Vector2.Clamp(camera.Position - Input.Drag,
                                                new Vector2(-500, -500),
                                                new Vector2((Zone.ZoneWidth * Tile.TileWidth) - 100,
                                                            (Zone.ZoneHeight * Tile.TileHeight) - 100));
            }

            camera.Update(gameTime);
            hud.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Scale to fit the screen resolution.
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, scaleMatrix);
            tileEngine.Draw(spriteBatch, camera.Position);
            hud.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
