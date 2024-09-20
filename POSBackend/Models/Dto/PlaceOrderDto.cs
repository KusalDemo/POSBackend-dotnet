using POSBackend.Models.Entities;

namespace POSBackend.Models.Dto
{
    public class PlaceOrderDto
    {
        public required string OrderId { get; set; }
        public required string CustomerId { get; set; }
        public required DateTime OrderDate { get; set; }
        public double Paid { get; set; }
        public int Discount { get; set; }
        public double Balance { get; set; }
    }
}
