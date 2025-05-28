using APSNET_Core_002.Data;
using APSNET_Core_002.Models;
using APSNET_Core_002.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APSNET_Core_002.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;
        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {

            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                if (customers == null || !customers.Any())
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
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null || string.IsNullOrEmpty(customer.Name) || string.IsNullOrEmpty(customer.Email))
            {
                return BadRequest("Customer data are required");
            }
            else
            {
                try
                {
                    customer = await _customerService.CreateCustomerAsync(customer);
                    return CreatedAtAction("CreateCustomer", new { Id = customer.Id }, customer);
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new customer record.");
                }
            }
        }

        [HttpPut]

        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            if (customer == null || customer.Id == 0)
            {
                return BadRequest("Customer data is required for update.");
            }
            var exoistingCustomer = await _customerService.GetCustomerByIdAsync(customer.Id);

            if (exoistingCustomer == null)
            {
                return NotFound($"Customer with ID {customer.Id} not found.");
            }

            try
            {
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
            var success = await _customerService.DeleteCustomerAsync(id);
            if (!success) return NotFound();
            return Ok($"Customer with ID {id} deleted.");

        }
    }
}
