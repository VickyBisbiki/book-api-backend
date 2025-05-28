using APSNET_Core_002.Interfaces;
using APSNET_Core_002.Models;

namespace APSNET_Core_002.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        { 
            return await _customerRepository.CreateCustomerAsync(customer);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            return await _customerRepository.DeleteCustomerAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
           return await _customerRepository.GetCustomerByIdAsync(id);

        }

        public async Task<Customer> UpdateCustomerAsync(int id, Customer customer)
        {
            return await _customerRepository.UpdateCustomerAsync(id, customer);
        }
    }
}
