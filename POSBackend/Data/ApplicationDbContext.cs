using Microsoft.EntityFrameworkCore;
using POSBackend.Models.Entities;

namespace POSBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PlaceOrder> PlaceOrders  { get; set; }

    }
}
