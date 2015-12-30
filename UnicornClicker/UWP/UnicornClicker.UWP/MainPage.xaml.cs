using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UnicornClicker.UWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Game));
        }

        private void Scores_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Scores));
        }
    }
}
