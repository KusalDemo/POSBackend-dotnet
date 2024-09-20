using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSBackend.Data;
using POSBackend.Models.Dto;
using POSBackend.Models.Entities;

namespace POSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        //Constructor through Injection (Database Context)
        public ItemController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllItems()
        {
            List<Item> items = dbContext.Items.ToList();
            List<Item> eligibleItems = new List<Item>();

            foreach (var item in items) {
                if (item.Price != 0 && item.Qty != 0) { 
                    eligibleItems.Add(item);
                }
            }
            return Ok(eligibleItems);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetItemById(Guid id) {
            Item? fetchedItem = dbContext.Items.Find(id);
            if (fetchedItem == null) {
                return NotFound("Oops can't find Item you looking for..");
            }
           return Ok(fetchedItem);
        }

        [HttpPost]
        public IActionResult AddItem(ItemDto dto)
        {
            Item item = new Item()
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Qty = dto.Qty,
            };
            dbContext.Items.Add(item);
            dbContext.SaveChanges();

            return Ok(item);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateItem(Guid id, ItemDto dto) {
            Item? item = dbContext.Items.Find(id);
            if (item == null) {
                return NotFound("Item you looking to update not find..");
            }

            item.Name = dto.Name;
            item.Description = dto.Description;
            item.Price = dto.Price;
            item.Qty = dto.Qty;

            dbContext.SaveChanges();
            return Ok(item);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteItem(Guid id) {
            Item? item = dbContext.Items.Find(id);
            if (item == null)
            {
                return NotFound("Item you looking to Delete not find..");
            }
            item.Price = 0;
            item.Qty =0;

            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
