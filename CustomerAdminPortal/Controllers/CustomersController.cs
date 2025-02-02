using CustomerAdminPortal.Data;
using CustomerAdminPortal.Models;
using CustomerAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace CustomerAdminPortal.Controllers
{
    //https://localhost:7280/api/Customers

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CustomersController(ApplicationDbContext dbContext)
        {
                this._context = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
          var allCoustomer= _context.customers.ToList();
            return Ok(allCoustomer);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetCustomerID(Guid id)
        {
          var customer= _context.customers.Find(id);
            if(customer is null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult AddCustomer(AddCustomerDetails addCustomerdetails)
        {
            var customerEntitt = new Customers()
            {
                Name = addCustomerdetails.Name,
                Email = addCustomerdetails.Email,
                Phone = addCustomerdetails.Phone,
                Salary = addCustomerdetails.Salary
            };
            _context.customers.Add(customerEntitt);
            _context.SaveChanges();

            return Ok(customerEntitt);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateCustomer(Guid id,UpdateCustomerDetails updateCustomer)
        {
            var customer = _context.customers.Find(id);
            if(customer is null)
            {
                return NotFound();

            }
            customer.Name = updateCustomer.Name;
            customer.Email = updateCustomer.Email;
            customer.Phone = updateCustomer.Phone;
            customer.Salary = updateCustomer.Salary;
            _context.SaveChanges();
            return Ok(customer);
            
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteCustomer(Guid id)
        {
            var Customer = _context.customers.Find(id);
            if(Customer is null)
            {
                return NotFound();
            }
            _context.customers.Remove(Customer);
            _context.SaveChanges();
            return Ok();
        }
    }
}
