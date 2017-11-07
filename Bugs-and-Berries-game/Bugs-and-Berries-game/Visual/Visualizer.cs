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

        public void Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender,
            Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            double screenWidth = sender.ActualWidth;
            double screenHeight = sender.ActualHeight;
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
                Windows.Foundation.Rect r = Windows.UI.Xaml.RectHelper.FromCoordinatesAndDimensions(
                    x, y, (float)hStride, (float)vStride);
                //// test only:
                //var brush = new Microsoft.Graphics.Canvas.Brushes.CanvasSolidColorBrush(sender, Windows.UI.Colors.Blue);
                //args.DrawingSession.DrawRoundedRectangle(r, 5f, 5f, brush);
                CanvasBitmap bitmap = bitmapHolder.BitmapForLocation(i);
                args.DrawingSession.DrawImage(bitmap, r);
            }
        }

        public delegate void LoadCompleteCallback();

        public void CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, 
            Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args, LoadCompleteCallback callback)
        {
            bitmapHolder.CreateResources(sender, args);
            callback();
        }
    }
}
