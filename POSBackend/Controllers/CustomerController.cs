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
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        //Constructor through Injection (Database Context)
        public CustomerController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var  allCustomers=dbContext.Customers.ToList();
            List<Customer> customers = new List<Customer>();
            foreach (var customer in allCustomers)
            {
                if (customer.Availability == "available")
                {
                    customers.Add(customer);
                }
            }
            return Ok(customers);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetCustomerById(Guid id)
        {
            Customer? customer = dbContext.Customers.Find(id);
            if (customer == null) { 
                return NotFound("Oops..! No any Customer found.");
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult AddCustomer(CustomerDto dto)
        {
            Customer customer = new Customer()
            {
                Name = dto.Name,
                Email = dto.Email,
                Address = dto.Address,
                Availability=dto.Availability
            };

            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();

            return Ok(customer);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateCustomer(Guid id, CustomerDto dto) {
            Customer? fetchedCustomer = dbContext.Customers.Find(id);
            if (fetchedCustomer == null) {
                return NotFound("Can't find any Customer related to given ID.");
            }

            fetchedCustomer.Name = dto.Name;
            fetchedCustomer.Email = dto.Email;
            fetchedCustomer.Address = dto.Address;
            fetchedCustomer.Availability = dto.Availability;

            dbContext.SaveChanges();
            return Ok(fetchedCustomer);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteCustomer(Guid id) {
            Customer? fetchedCustomer = dbContext.Customers.Find(id);
            if (fetchedCustomer == null)
            {
                return NotFound("Can't find any Customer related to given ID.");
            }
            fetchedCustomer.Availability = "unavailable";

            dbContext.SaveChanges();
            return NoContent();
        }

        //Custom Queries..
        /*[HttpDelete("delete-customer/{id:guid}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                var result = await dbContext.Customers.FromSqlRaw($"UPDATE Customers SET Availability = 'Unavailable' WHERE Id = {id}")
            .ToListAsync();

                if (result.Count == 0) {
                    return BadRequest("Not Deleted..");
                }
                return Ok("Customer deleted succesfully..");

            } catch (Exception e) { 
                return BadRequest(e.Message);
            }
        }*/
    }
}
