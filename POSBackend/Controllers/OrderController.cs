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
        [Route("{customerId:guid}")]
        public IActionResult AddOrder(Guid customerId, List<OrderItemDto> items)
        {
            Customer? customer = dbContext.Customers.Find(customerId);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            var order = new PlaceOrder
            {
                OrderId = Guid.NewGuid().ToString(),
                CustomerId = customer.Id.ToString("D"),
                Customer = customer,
                OrderDate = DateTime.Now,
                Paid = 0,
                Discount = 0,
                Balance = 0,
                OrderItems = new List<OrderItem>()
            };

            double totalAmount = 0;

            foreach (var itemDto in items)
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
    }
}
