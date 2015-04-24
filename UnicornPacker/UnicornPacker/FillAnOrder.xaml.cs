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
    public sealed partial class FillAnOrder : Page
    {
        private readonly OrdersContext db = new OrdersContext();

        public FillAnOrder()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ReLoadAssignedOrders();
        }

        private void IsPacked_Changed(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();

            this.Shipped.IsEnabled = CurrentOrder.Lines.All(l => l.IsPacked);
        }

        private async void Shipped_Click(object sender, RoutedEventArgs e)
        {
            if (await ConfirmShipping())
            {
                CurrentOrder.IsShipped = true;
                db.SaveChanges();

                try
                {
                    await UnicornStoreService.RecordOrderShipped(CurrentOrder.OrderId);
                    CurrentOrder.IsShippingSynced = true;
                    db.SaveChanges();
                }
                catch (HttpRequestException)
                {
                    var dialog = new MessageDialog("Could not mark as shipped in central database. Use the 'Sync' button when you are back online.");
                    await dialog.ShowAsync();
                }

                ReLoadAssignedOrders();
            }
        }

        private async Task<bool> ConfirmShipping()
        {
            var dialog = new MessageDialog("Confirm that this package has been shipped?");
            dialog.Commands.Add(new UICommand("Yes"));
            dialog.Commands.Add(new UICommand("No"));
            var result = await dialog.ShowAsync();
            return result.Label == "Yes";
        }

        private void ReLoadAssignedOrders()
        {
            this.Orders.ItemsSource = db.Orders.Where(o => !o.IsShipped).ToList();

            // TODO this is a workaround for Include not working with SQLite
            var lines = db.OrderLines.ToList();

            this.OrdersPanel.Visibility = this.Orders.Items.Any() ? Visibility.Visible : Visibility.Collapsed;
            this.NoOrders.Visibility = this.Orders.Items.Any() ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentOrder = (Order)Orders.SelectedItem;
        }

        private Order _currentOrder;
        private Order CurrentOrder
        {
            get
            {
                return _currentOrder;
            }
            set
            {
                _currentOrder = value;
                this.Order.DataContext = value;
                if (value == null)
                {
                    this.Order.Visibility = Visibility.Collapsed;
                }
                else
                {
                    var order = (Order)this.Order.DataContext;
                    this.Shipped.IsEnabled = order.Lines.All(l => l.IsPacked);
                    this.Order.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
