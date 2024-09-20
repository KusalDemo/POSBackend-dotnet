using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var  allEmployees=dbContext.Customers.ToList();
            return Ok(allEmployees);
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
    }
}
