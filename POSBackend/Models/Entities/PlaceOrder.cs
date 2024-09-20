namespace POSBackend.Models.Entities
{
    public class PlaceOrder
    {
        public required string OrderId { get; set; }
        public required string CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public required DateTime OrderDate { get; set; }
        public double Paid { get; set; }
        public int Discount { get; set; }
        public double Balance { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

}
