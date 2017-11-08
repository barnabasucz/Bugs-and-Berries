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
        private World.IGameItemHolder gameItemHolder;
        private ITileCoordinateHolder tileCoordinateHolder;
        private ICanvasBitmapHolder bitmapHolder;
        public Visualizer(World.IGameItemHolder gameItemHolder, 
            ITileCoordinateHolder tileCoordinateHolder, ICanvasBitmapHolder bitmapHolder)
        {
            this.gameItemHolder = gameItemHolder;
            this.tileCoordinateHolder = tileCoordinateHolder;
            this.bitmapHolder = bitmapHolder;
        }

        public World.IGameItemHolder GameItemHolder { set { gameItemHolder = value; } }
        public ITileCoordinateHolder TileCoordinateHolder { set { tileCoordinateHolder = value; } }

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
                    CanvasBitmap playerBitmap;
                    playerBitmap = bitmapHolder.PlayerIdleBitmap();
                    // to do: change bitmap depending on whether player's hand is out
                    args.DrawingSession.DrawImage(playerBitmap, r);
                }
            }
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
