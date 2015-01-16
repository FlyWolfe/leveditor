using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevEditor
{
    class LayerWindowButton : Button
    {
        private Texture2D texture;
        private float transparency;

        public float Transparency
        {
            get { return transparency; }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public LayerWindowButton(Rectangle rectangle, Texture2D texture, int id) : base(rectangle, id)
        {
            this.texture = texture;
            transparency = 1;
        }
    }
}
