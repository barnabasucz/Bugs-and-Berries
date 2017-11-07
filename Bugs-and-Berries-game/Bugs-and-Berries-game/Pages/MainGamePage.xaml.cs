using Microsoft.Graphics.Canvas.Brushes;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Bugs_and_Berries_game.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainGamePage : Page
    {
        private Visual.Visualizer visualizer;
        private bool isLoaded;

        public MainGamePage()
        {
            isLoaded = false;
            visualizer = new Visual.Visualizer(
                new World.Locations.LocationContainer(), new Visual.Arrangements.TileArrangements(),
                new Visual.TileGraphicArrangement());
            this.InitializeComponent();
        }

        private void NorthButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WestButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SouthButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EastButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            this.LcdScreen.RemoveFromVisualTree();
            this.LcdScreen = null;
        }

        private void LcdScreen_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, 
            Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            if (isLoaded)
            {
                args.DrawingSession.Clear(Color.FromArgb(255, 220, 220, 220));
                visualizer.Draw(sender, args);
            }
        }

        public void LoadComplete()
        {
            isLoaded = true;
            LcdScreen.Invalidate();
        }
        private void LcdScreen_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            isLoaded = false;
            visualizer.CreateResources(sender, args, LoadComplete);
        }
    }
}
