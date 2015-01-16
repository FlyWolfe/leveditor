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
    class LayerEditWindow : Window
    {
        private static List<LayerWindowButton> layerIndexButtons;

        private Texture2D sliderTexture;
        private Rectangle sliderRect;

        private Texture2D sliderButtonTexture;
        private Button sliderButton;

        public static int SelectedLayer = 0;

        public static List<LayerWindowButton> LayerIndexButtons
        {
            get { return layerIndexButtons; }
        }

        public LayerEditWindow(Vector2 position, Vector2 dimensions)
            : base(position, dimensions)
        {
            layerIndexButtons = new List<LayerWindowButton>();
        }

        new public void Load(ContentManager content, string filePath)
        {
            base.Load(content, filePath);

            sliderButtonTexture = content.Load<Texture2D>("Test_Content/SliderButton.png");
            sliderButton = new Button(new Rectangle(12, 184, 32, 32), 0); // Test values for now.

            sliderTexture = content.Load<Texture2D>("Test_Content/Slider.png");
            sliderRect = new Rectangle(28, 200, sliderTexture.Width, sliderTexture.Height);

            List<Texture2D> buttonTextures = new List<Texture2D>();
            buttonTextures.Add(content.Load<Texture2D>("Test_Content/1.png"));
            buttonTextures.Add(content.Load<Texture2D>("Test_Content/2.png"));
            buttonTextures.Add(content.Load<Texture2D>("Test_Content/3.png"));

            for (int i = 0; i < 3; ++i) // For testing, just using three buttons.
            {
                // 42 to add a buffer of 10 pixels (size of button = 32px).
                layerIndexButtons.Add(new LayerWindowButton(new Rectangle((42 * i) + 10, 15, 32, 32), buttonTextures[i], i));
                layerIndexButtons[i].SliderValue = sliderRect.Location.X;
            }
        }

        new public void Update(GameTime gameTime)
        {
            if (Input.DisplayHUD)
            {
                if (BackgroundRectangle.Contains(Input.GesturePosition))
                {
                    if (BackgroundRectangle.Contains(Input.Tap))
                    {
                        foreach (LayerWindowButton button in layerIndexButtons)
                        {
                            if (button.Rect.Contains(Input.Tap))
                            {
                                // We have to set the slider's position to the saved position for this layer.
                                SelectedLayer = button.ID;
                                sliderButton.ChangeXPosition(button.SliderValue - sliderButton.Rect.Location.X);
                            }
                        }
                    }

                    if (Input.Drag.X != 0)
                    {
                        // Change the position of the slider, and then save its position for this layer.
                        ChangeSliderPosition((int)Input.Drag.X);
                        layerIndexButtons[SelectedLayer].SliderValue = sliderButton.Rect.Location.X;
                    }
                }
            }

            base.Update(gameTime);
        }

        new public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(sliderTexture, sliderRect, Color.White);
            spriteBatch.Draw(sliderButtonTexture, sliderButton.Rect, Color.White);
            foreach(LayerWindowButton button in layerIndexButtons)
            {
                spriteBatch.Draw(button.Texture, button.Rect, Color.White);
            }
        }

        private void ChangeSliderPosition(int delta)
        {
            if (delta < 0) // Moving left.
            {
                if (delta < (sliderRect.X - sliderButton.Rect.Center.X)) // Comparing larger abs value.
                {
                    delta = sliderRect.X - sliderButton.Rect.Center.X;
                    sliderButton.ChangeXPosition(delta);
                }

                else
                    sliderButton.ChangeXPosition(delta);
            }

            else // Moving right.
            {
                if (delta > ((sliderRect.X + sliderRect.Width) - sliderButton.Rect.Center.X))
                {
                    delta = (sliderRect.X + sliderRect.Width) - sliderButton.Rect.Center.X;
                    sliderButton.ChangeXPosition(delta);
                }

                else
                    sliderButton.ChangeXPosition(delta);
            }
        }
    }
}
