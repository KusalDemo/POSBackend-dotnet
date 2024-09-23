using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSBackend.Data;
using POSBackend.Models.Dto;
using POSBackend.Models.Entities;

namespace POSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public OrderController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult AddOrder(PlaceOrderDto dto)
        {
            Customer? customer = dbContext.Customers.Find(dto.CustomerId);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            var order = new PlaceOrder
            {
                OrderId = Guid.NewGuid().ToString(),
                CustomerId = dto.CustomerId,
                Customer = customer,
                OrderDate = DateTime.Now,
                Paid = dto.Paid,
                Discount = dto.Discount,
                Balance = dto.Balance,
                OrderItems = new List<OrderItem>()
            };

            double totalAmount = 0;

            foreach (var itemDto in dto.items)
            {
                if (string.IsNullOrWhiteSpace(itemDto.ItemId) || itemDto.ItemId == Guid.Empty.ToString())
                {
                    return BadRequest("Invalid itemId provided.");
                }

                var item = dbContext.Items.Find(Guid.Parse(itemDto.ItemId));
                if (item == null)
                {
                    return NotFound($"Item with ID {itemDto.ItemId} not found");
                }

                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ItemId = item.Id.ToString("D"),
                    PlaceOrder = order,
                    Item = item,
                    ItemCount = itemDto.ItemCount,
                    UnitPrice = item.Price,
                    Total = item.Price * itemDto.ItemCount
                };

                order.OrderItems.Add(orderItem);
                totalAmount += orderItem.Total;

                item.Qty -= itemDto.ItemCount;
            }

            order.Balance = totalAmount;
            customer.Orders.Add(order);

            dbContext.SaveChanges();
            return Ok(order);
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            List<PlaceOrder> placeOrders = dbContext.PlaceOrders.ToList();
            return Ok(placeOrders);
        }
    }
}
