namespace POSBackend.Models.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Address { get; set; }
        public required string Availability {  get; set; }
        public ICollection<PlaceOrder> Orders { get; set; }
    }
}
