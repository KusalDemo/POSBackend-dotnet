using POSBackend.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace POSBackend.Models.Dto
{
    public class OrderItemDto
    {
        public string OrderId { get; set; }
        public string ItemId { get; set; }
        public int ItemCount { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }
    }
}
