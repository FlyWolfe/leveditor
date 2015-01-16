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
    class LayerWindowButton : Button
    {
        private Texture2D texture;
        private float transparency;
        private int sliderValue;

        public int SliderValue
        {
            get { return sliderValue; }
            set
            {
                sliderValue = value;

                /* We start off with 100.0f as 100.0f / 100.0f = 1, which is a fully opaque layer mask.
                 * We subtract 12 from sliderValue as 12 is the hardcoded position that the slider can
                 * go furthest left to (creating an artificial "zero" value). We divide by 2 as the
                 * slider button can traverse 200 pixels. Divide by 100 to get 0 <= value <= 1.
                 * 
                 * e.g. If the slider is at 140px: (100 - ((140 - 12) / 2)) / 100 = .36; closer to an invisible mask.
                 */
                transparency = (100.0f - ((sliderValue - 12) / 2)) / 100.0f;
            }
        }

        public float Transparency
        {
            get { return transparency; }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public LayerWindowButton(Rectangle rectangle, Texture2D texture, int id)
            : base(rectangle, id)
        {
            this.texture = texture;
            transparency = 1;
        }
    }
}