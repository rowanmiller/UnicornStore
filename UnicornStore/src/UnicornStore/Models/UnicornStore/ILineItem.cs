using System;

namespace UnicornStore.Models.UnicornStore
{
    public interface ILineItem
    {
        Product Product { get; }
        int Quantity { get; }
        decimal PricePerUnit { get; }
    }
}