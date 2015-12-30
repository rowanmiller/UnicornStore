using UnicornClicker.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UnicornClicker.UWP
{
    public sealed partial class Scores : Page
    {
        private ScoresViewModel _viewModel;

        public Scores()
        {
            this.InitializeComponent();

            _viewModel = new ScoresViewModel();
            this.DataContext = _viewModel;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
