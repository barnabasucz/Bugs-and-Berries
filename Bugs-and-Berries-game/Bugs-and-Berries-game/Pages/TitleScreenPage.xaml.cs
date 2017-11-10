using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Bugs_and_Berries_game.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TitleScreenPage : Page
    {
        public TitleScreenPage()
        {
            this.InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Pages.MainGamePage));
            Window.Current.Activate();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Pages.EditorPage));
            Window.Current.Activate();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Pages.HelpPage));
            Window.Current.Activate();
        }
    }
}
