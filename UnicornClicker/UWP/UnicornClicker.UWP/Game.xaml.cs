using UnicornClicker.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UnicornClicker.UWP
{
    public sealed partial class Game : Page
    {
        private GameViewModel _viewModel;

        public Game()
        {
            this.InitializeComponent();

            _viewModel = new GameViewModel();
            this.DataContext = _viewModel;
            _viewModel.StartNewGame();
        }

        private void Unicorn_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.HandleClick();
        }

        private void Retry_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.StartNewGame();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    } 
}
