using EB__DASCustomer_TaskMVCWeb.Models;
using System.Text.Json;

namespace EB__DASCustomer_TaskMVCWeb.Services.Customers
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Customer>> GetCustomersAsync()
        {
            var response = await _httpClient.GetAsync("Customers");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            List<Customer>? customers = JsonSerializer.Deserialize<List<Customer>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return customers;
        }      
    }
}