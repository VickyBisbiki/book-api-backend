using APSNET_Core_002.Data;
using APSNET_Core_002.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APSNET_Core_002.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext conext)
        {

            _context = conext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {

            try
            {
                var customers = await _context.Customers.ToListAsync();
                if (customers == null || customers.Count == 0)
                {
                    return NotFound("No customers found.");
                }
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from the database: {ex.Message}");
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customers customer)
        {
            if (customer == null || string.IsNullOrEmpty(customer.Name) || string.IsNullOrEmpty(customer.Email))
            {
                return BadRequest("Customer data are required");
            }
            else
            {
                try
                {
                    _context.Customers.Add(customer);
                    _context.SaveChanges();
                    return CreatedAtAction("CreateCustomer", new { Id = customer.Id }, customer);
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new customer record.");
                }
            }
        }

        [HttpPut]

        public async Task<IActionResult> UpdateCustomer([FromBody] Customers customer)
        {
            if (customer == null || customer.Id == 0)
            {
                return BadRequest("Customer data is required for update.");
            }
            var exoistingCustomer = await _context.Customers.FindAsync(customer.Id);
            if (exoistingCustomer == null)
            {
                return NotFound($"Customer with ID {customer.Id} not found.");
            }
            try
            {
                exoistingCustomer.Name = customer.Name;
                exoistingCustomer.Email = customer.Email;
                _context.Customers.Update(exoistingCustomer);
                await _context.SaveChangesAsync();
                return Ok(exoistingCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating customer data: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
            try
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return Ok($"Customer with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting customer: {ex.Message}");
            }

        }
    }
}
