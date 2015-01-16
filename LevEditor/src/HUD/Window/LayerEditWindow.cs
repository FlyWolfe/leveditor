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
        private Texture2D button1Texture;
        private Texture2D button2Texture;
        private Texture2D button3Texture;
        private List<Button> layerIndexButtons;

        private Texture2D sliderTexture;
        private Rectangle slider;
        private LayerWindowButton sliderButton;

        public static int SelectedLayer = 0;

        public LayerEditWindow(Vector2 position, Vector2 dimensions) : base(position, dimensions)
        {
            layerIndexButtons = new List<Button>();
        }

        new public void Load(ContentManager content, string filePath)
        {
            base.Load(content, filePath);

            Texture2D sliderButtonTexture = content.Load<Texture2D>("Test_Content/SliderButton.png");
            button1Texture = content.Load<Texture2D>("Test_Content/1.png");
            button2Texture = content.Load<Texture2D>("Test_Content/2.png");
            button3Texture = content.Load<Texture2D>("Test_Content/3.png");
            sliderTexture = content.Load<Texture2D>("Test_Content/Slider.png");

            slider = new Rectangle(28, 200, sliderTexture.Width, sliderTexture.Height);
            sliderButton = new LayerWindowButton(new Rectangle(12, 184, 32, 32), sliderButtonTexture, 0); // Test values for now.

            // For testing, just using three buttons.
            for (int i = 0; i < 3; ++i)
            {
                // 42 to add a buffer of 10 pixels (size of button = 32px).
                layerIndexButtons.Add(new Button(new Rectangle((42 * i) + 10, 15, 32, 32), i));
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
                        foreach (Button button in layerIndexButtons)
                        {
                            if (button.Rect.Contains(Input.Tap))
                                SelectedLayer = button.ID;
                        }
                    }

                    if (Input.Drag.X != 0)
                    {
                        ChangeSliderPosition((int)Input.Drag.X);
                    }
                }
            }

            base.Update(gameTime);
        }

        new public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(button1Texture, layerIndexButtons[0].Rect, Color.White);
            spriteBatch.Draw(button2Texture, layerIndexButtons[1].Rect, Color.White);
            spriteBatch.Draw(button3Texture, layerIndexButtons[2].Rect, Color.White);
            spriteBatch.Draw(sliderTexture, slider, Color.White);
            spriteBatch.Draw(sliderButton.Texture, sliderButton.Rect, Color.White);
        }

        private void ChangeSliderPosition(int delta)
        {
            if (delta < 0) // Moving left.
            {
                if (delta < (slider.X - sliderButton.Rect.Center.X)) // Comparing larger abs value.
                {
                    delta = slider.X - sliderButton.Rect.Center.X;
                    sliderButton.ChangeXPosition(delta);
                }

                else
                    sliderButton.ChangeXPosition(delta);
            }

            else // Moving right.
            {
                if (delta > ((slider.X + slider.Width) - sliderButton.Rect.Center.X))
                {
                    delta = (slider.X + slider.Width) - sliderButton.Rect.Center.X;
                    sliderButton.ChangeXPosition(delta);
                }

                else
                    sliderButton.ChangeXPosition(delta);
            }
        }
    }
}
