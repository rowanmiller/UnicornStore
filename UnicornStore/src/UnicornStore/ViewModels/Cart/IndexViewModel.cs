using System;
using System.Collections.Generic;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.ViewModels.Cart
{
    public class IndexViewModel
    {
        public IEnumerable<CartItem> CartItems { get; set; }
        public IEnumerable<Category> TopLevelCategories { get; set; }
        public IndexMessage Message { get; set; }

        

    }

    public enum IndexMessage
    {
        None,
        ItemAdded,
        ItemRemoved
    }

    public static class IndexMessageExtensions
    {
        public static string GetMessage(this IndexMessage message)
        {
            switch (message)
            {
                case IndexMessage.ItemAdded:
                    return "Item added to your cart";
                case IndexMessage.ItemRemoved:
                    return "Item removed from your cart";
                default:
                    return string.Empty;
            }
        }
    }
}