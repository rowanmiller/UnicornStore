using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnicornPacker.Models;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UnicornPacker
{
    public sealed partial class ChooseOrders : Page
    {
        public ChooseOrders()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await ReLoadPendingOrders();
        }

        private async void AssignOrders_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrders = Orders.SelectedItems.Cast<Order>().ToList();

            // Assign selected orders to current user
            var selectedIds = selectedOrders.Select(o => o.OrderId);

            try
            {
                // Take a local copy of the orders
                using (var db = new OrdersContext())
                {
                    foreach (var order in selectedOrders)
                    {
                        // TODO copy selected orders to local database
                    }


                }

                await UnicornStoreService.PackingOrders(selectedIds);

                // Refresh the page with pending orders
                await ReLoadPendingOrders();

                // Inform user of success
                var dialog = new MessageDialog("Orders successfully assigned.");
                await dialog.ShowAsync();
            }
            catch (HttpRequestException)
            {
                var dialog = new MessageDialog("Could not connect. Try again when you have a network connection.");
                await dialog.ShowAsync();
            }
            
        }

        private void Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.AssignOrders.IsEnabled = this.Orders.SelectedItems.Count > 0;
        }

        private async Task ReLoadPendingOrders()
        {
            this.Orders.ItemsSource = null;
            this.Message.Visibility = Visibility.Visible;
            this.OrdersPanel.Visibility = Visibility.Collapsed;

            Message.Text = "Fetching pending orders...";

            try
            {
                var orders = await UnicornStoreService.GetPendingOrders();
                if (orders.Any())
                {
                    this.Orders.ItemsSource = orders;
                    this.Message.Visibility = Visibility.Collapsed;
                    this.OrdersPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    Message.Text = "There are currently no pending orders.";
                }
            }
            catch (HttpRequestException)
            {
                    Message.Text = "Could not connect. Try again when you have a network connection.";
            }
        }
    }
}
