using System.Collections.Generic;
using Windows.UI.Xaml;

namespace UnicornPacker.Models
{
    public class Order
    {
        public bool IsShipped { get; set; }
        public bool IsShippingSynced { get; set; }
        public int OrderId { get; set; }
        public string Addressee { get; set; }
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string CityOrTown { get; set; }
        public string StateOrProvince { get; set; }
        public string ZipOrPostalCode { get; set; }
        public string Country { get; set; }

        public List<OrderLine> Lines { get; set; }

        public string DisplayId
        {
            get { return OrderId.ToString().PadLeft(10, '0'); }
        }

        public Visibility LineTwoVisibility
        {
            get { return string.IsNullOrEmpty(this.LineTwo) ? Visibility.Collapsed : Visibility.Visible; }
        }
    }

}
