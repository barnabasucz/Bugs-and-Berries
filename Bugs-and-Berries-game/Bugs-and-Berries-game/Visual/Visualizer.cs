using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_and_Berries_game.Visual
{
    public class Visualizer
    {
        private bool blinking; // false = don't animate blinkStateOn; true = animate blinkStateOn
        private bool blinkStateOn; // true = draw player, false = don't draw player
        private const int blinkInterval = 340;
        private int blinkLength;
        private int maxBlinkTime;
        private int totalBlinkTime;
        private bool picking;
        private const int maxPickTime = 500;
        private int pickTime;
        private World.IGameItemHolder gameItemHolder;
        private ITileCoordinateHolder tileCoordinateHolder;
        private ICanvasBitmapHolder bitmapHolder;
        public Visualizer(World.IGameItemHolder gameItemHolder, 
            ITileCoordinateHolder tileCoordinateHolder, ICanvasBitmapHolder bitmapHolder)
        {
            this.gameItemHolder = gameItemHolder;
            this.tileCoordinateHolder = tileCoordinateHolder;
            this.bitmapHolder = bitmapHolder;
            blinking = false;
            blinkStateOn = true;
            blinkLength = 0;
            picking = false;
            pickTime = 0;
        }

        public World.IGameItemHolder GameItemHolder { set { gameItemHolder = value; } }
        public ITileCoordinateHolder TileCoordinateHolder { set { tileCoordinateHolder = value; } }

        public void Blink(int maxMs)
        {
            blinking = true;
            totalBlinkTime = 0;
            maxBlinkTime = maxMs;
        }

        public void StopBlinking()
        {
            blinking = false;
            maxBlinkTime = 0;
            totalBlinkTime = 0;
        }

        public void Pick()
        {
            picking = true;
        }

        public void Update(StateMachine.GameStateCodes gameState, int msElapsed)
        {
            if (blinking)
            {
                blinkLength += msElapsed;
                if (totalBlinkTime >= maxBlinkTime)
                {
                    blinking = false;
                    totalBlinkTime = 0;
                    blinkLength = 0;
                }
                else
                {
                    if (blinkLength >= blinkInterval)
                    {
                        blinkStateOn = !blinkStateOn;
                        blinkLength = 0;
                    }
                }
            }
            else
            {
                blinkStateOn = true;
            }
            pickTime += msElapsed;
            if (pickTime >= maxPickTime)
            {
                picking = false;
                pickTime = 0;
            }
        }

        public void Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender,
            Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args, StateMachine.GameStateCodes gameState)
        {
            double screenWidth = sender.Size.Width;
            double screenHeight = sender.Size.Height;
            double hStride = System.Math.Floor(screenWidth / 6d);
            double vStride = System.Math.Floor(screenHeight / 7d);
            double hLeftover = screenWidth - (6d * hStride);
            double vLeftover = screenHeight - (7d * vStride);
            float hMargin = (float)System.Math.Floor(hLeftover / 2d);
            float vMargin = (float)System.Math.Floor(vLeftover / 2d);
            for (int i = 0; i < World.Globals.LocationCount; i++)
            {
                Arrangements.TileCoordinate coord = tileCoordinateHolder.TileCoordinateFor(i);
                float x = hMargin + (float)(coord.Column * hStride);
                float y = vMargin + (float)(coord.Row * vStride);
                var r = new Windows.Foundation.Rect(x, y, (float)hStride, (float)vStride);
                CanvasBitmap bitmap = bitmapHolder.BitmapForLocation(i);
                args.DrawingSession.DrawImage(bitmap, r);
                
                if (gameItemHolder.IsBerryAt(i) || gameState==StateMachine.GameStateCodes.StartingUp)
                {
                    CanvasBitmap berryBitmap = bitmapHolder.BerryBitmap();
                    args.DrawingSession.DrawImage(berryBitmap, r);
                }
                if (gameItemHolder.IsBugAt(i) || gameState == StateMachine.GameStateCodes.StartingUp)
                {
                    CanvasBitmap bugBitmap = bitmapHolder.BugBitmap();
                    args.DrawingSession.DrawImage(bugBitmap, r);
                }
                if (gameItemHolder.IsPlayerAt(i) || gameState == StateMachine.GameStateCodes.StartingUp)
                {
                    if (blinkStateOn)
                    {
                        CanvasBitmap playerBitmap;
                        playerBitmap = bitmapHolder.PlayerIdleBitmap(picking);
                        args.DrawingSession.DrawImage(playerBitmap, r);
                    }
                }
            }
            // draw message here
            // fade it during the update function
        }

        public delegate void LoadCompleteCallback();

        public void CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, 
            Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args, LoadCompleteCallback callback)
        {
            bitmapHolder.CreateResources(sender, args);
            callback();
        }
    }
}
