using APSNET_Core_002.Data;
using APSNET_Core_002.Interfaces;
using APSNET_Core_002.Models;
using Microsoft.EntityFrameworkCore;

namespace APSNET_Core_002.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync(); ;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (id <= 0)
            {
                throw new ArgumentException("Invalid customer ID.");
            }          

            return customer;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {    
            if(customer == null || string.IsNullOrEmpty(customer.Name) || string.IsNullOrEmpty(customer.Email))
            {
                throw new ArgumentException("Customer data are required.");
            }
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return  customer;

        }

        public async Task<Customer> UpdateCustomerAsync(int id, Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(id);

            if (existingCustomer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");

            }

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            _context.Customers.Update(existingCustomer);
            await _context.SaveChangesAsync();
            return  existingCustomer;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = _context.Customers.FindAsync(id);
            if (customer == null)
            {
                new KeyNotFoundException($"Customer with ID {id} not found.");
                return false;
            }
            _context.Customers.Remove(customer.Result);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
