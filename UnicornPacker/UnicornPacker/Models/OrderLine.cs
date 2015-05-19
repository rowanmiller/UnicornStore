namespace UnicornPacker.Models
{
    public class OrderLine
    {
        public bool IsPacked { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public string ProductDisplayId
        {
            get { return ProductId.ToString().PadLeft(6, '0'); }
        }
    }

}
