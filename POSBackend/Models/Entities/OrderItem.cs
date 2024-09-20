namespace POSBackend.Models.Entities
{
    public class OrderItem
    {
        public string OrderId { get; set; }
        public string ItemId { get; set; }

        public PlaceOrder PlaceOrder { get; set; }
        public Item Item { get; set; }

        public int ItemCount { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }
    }
}
