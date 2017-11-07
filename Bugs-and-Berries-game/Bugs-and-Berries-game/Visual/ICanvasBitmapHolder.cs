using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_and_Berries_game.Visual
{
    public interface ICanvasBitmapHolder
    {
        Microsoft.Graphics.Canvas.CanvasBitmap BitmapForLocation(int locationId);
        void CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args);
    }
}
