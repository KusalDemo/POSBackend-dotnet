namespace POSBackend.Models.Dto
{
    public class ItemDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
    }
}
