using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Input;

namespace LevEditor
{
    public static class Input
    {
        private static GestureSample gesture = default(GestureSample);
        private static List<GestureSample> gestures = new List<GestureSample>();
        private static EdgeGesture edgeGesture;

        private static Vector2 prevPosition;

        // Every time we complete an edge gesture, we toggle the HUD's display.
        private static bool displayHUD = false;

        public static bool DisplayHUD
        {
            get
            {
                return displayHUD;
            }
        }
        
        public static Vector2 Tap
        {
            get
            {
                if (gesture.GestureType == GestureType.Tap && prevPosition != gesture.Position)
                {
                    return new Vector2(gesture.Position.X / Main.ScreenScale.X,
                                       gesture.Position.Y / Main.ScreenScale.Y);
                }

                return new Vector2(-1, -1);
            }
        }

        public static Vector2 Drag
        {
            get
            {
                return new Vector2(gesture.Delta.X / Main.ScreenScale.X,
                                   gesture.Delta.Y / Main.ScreenScale.Y);
            }
        }

        /// <summary>
        /// Returns the position of any gesture.
        /// </summary>
        public static Vector2 GesturePosition
        {
            get
            {
                if (gesture.GestureType == GestureType.FreeDrag && prevPosition != gesture.Position)
                {
                    return new Vector2(gesture.Position.X / Main.ScreenScale.X,
                                       gesture.Position.Y / Main.ScreenScale.Y);
                }

                return new Vector2(-1, -1);
            }
        }

        public static void Initialize()
        {
            edgeGesture = EdgeGesture.GetForCurrentView();
            edgeGesture.Canceled += new TypedEventHandler<EdgeGesture, EdgeGestureEventArgs>(EdgeGesture_Canceled);
            edgeGesture.Completed += new TypedEventHandler<EdgeGesture, EdgeGestureEventArgs>(EdgeGesture_Completed);
            edgeGesture.Starting += new TypedEventHandler<EdgeGesture, EdgeGestureEventArgs>(EdgeGesture_Starting);
        }

        private static void EdgeGesture_Starting(object sender, EdgeGestureEventArgs e)
        {

        }

        private static void EdgeGesture_Canceled(object sender, EdgeGestureEventArgs e)
        {

        }

        private static void EdgeGesture_Completed(object sender, EdgeGestureEventArgs e)
        {
            if (displayHUD)
                displayHUD = false;

            else
                displayHUD = true;
        }

        public static void Update(GameTime gameTime)
        {
            prevPosition = gesture.Position;
            gestures.Clear();
            while (TouchPanel.IsGestureAvailable)
            {
                gesture = TouchPanel.ReadGesture();
            }
        }
    }
}
