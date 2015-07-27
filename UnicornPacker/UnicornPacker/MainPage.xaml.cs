using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using UnicornPacker.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnicornPacker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Splitter.IsPaneOpen = (Splitter.IsPaneOpen == true) ? false : true;
        }

        private void ChooseOrders_Click(object sender, RoutedEventArgs e)
        {
            ScenarioFrame.Navigate(typeof(ChooseOrders));
            if (Window.Current.Bounds.Width < 640)
            {
                Splitter.IsPaneOpen = false;
            }
        }

        private void FillOrder_Click(object sender, RoutedEventArgs e)
        {
            ScenarioFrame.Navigate(typeof(FillAnOrder));
            if (Window.Current.Bounds.Width < 640)
            {
                Splitter.IsPaneOpen = false;
            }
        }

        private async void Sync_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new OrdersContext())
            {
                var orders = db.Orders.Where(o => o.IsShipped && !o.IsShippingSynced).ToList();

                if (orders.Any())
                {
                    try
                    {
                        foreach (var order in orders)
                        {
                            await UnicornStoreService.ShippedOrder(order.OrderId);
                            order.IsShippingSynced = true;
                            db.SaveChanges();
                        }

                        var dialog = new MessageDialog("All shipped orders now recorded in central database.");
                        await dialog.ShowAsync();
                    }
                    catch (HttpRequestException)
                    {
                        var dialog = new MessageDialog("Could not connect. Try again when you have a network connection.");
                        await dialog.ShowAsync();
                    }
                }
                else
                {
                    var dialog = new MessageDialog("There weren't any shipped orers not already recorded in central database.");
                    await dialog.ShowAsync();
                }
            }
        }
    }
}
