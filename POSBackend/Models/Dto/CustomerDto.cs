namespace POSBackend.Models.Dto
{
    public class CustomerDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Address { get; set; }
        public required string Availability { get; set; }
    }
}
