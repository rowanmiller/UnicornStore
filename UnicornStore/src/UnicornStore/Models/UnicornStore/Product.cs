using System;

namespace UnicornStore.AspNet.Models.UnicornStore
{
    public class Product
    {
        public int ProductId { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public decimal MSRP { get; set; }
        public decimal CurrentPrice { get; set; }
        public string ImageUrl { get; set; }

        public decimal Savings
        {
            get { return MSRP - CurrentPrice; }
        }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}