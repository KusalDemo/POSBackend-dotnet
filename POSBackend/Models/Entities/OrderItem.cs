using System.ComponentModel.DataAnnotations;

namespace POSBackend.Models.Entities
{
    public class OrderItem
    {
        [Key]
        public string OrderId { get; set; }
        [Key]
        public string ItemId { get; set; }

        public PlaceOrder PlaceOrder { get; set; }
        public Item Item { get; set; }

        public int ItemCount { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }
    }
}
