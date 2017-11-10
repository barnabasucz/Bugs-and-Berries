using Microsoft.Graphics.Canvas;

namespace Bugs_and_Berries_game.Visual
{
    public interface ICanvasBitmapHolder
    {
        CanvasBitmap BitmapForLocation(int locationId);
        CanvasBitmap BerryBitmap();
        CanvasBitmap BugBitmap();
        CanvasBitmap PlayerIdleBitmap(bool picking);
        CanvasBitmap PlayerPickingBitmap();
        void CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args);
    }
}
