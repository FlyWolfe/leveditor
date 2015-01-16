using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevEditor
{
    public static class Zone
    {
        public static int ZoneWidth;
        public static int ZoneHeight;
        public static List<int>[,] Canvas;

        public static void Initialize()
        {
            Canvas = new List<int>[ZoneHeight, ZoneWidth];

            for (int i = 0; i < ZoneHeight; ++i)
            {
                for (int j = 0; j < ZoneWidth; ++j)
                {
                    Canvas[i, j] = new List<int>();
                }
            }
        }
    }
}
