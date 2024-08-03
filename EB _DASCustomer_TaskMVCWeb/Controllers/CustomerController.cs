using EB__DASCustomer_TaskMVCWeb.Services.Customers;
using Microsoft.AspNetCore.Mvc;

namespace EB__DASCustomer_TaskMVCWeb.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetCustomersAsync();
            return View(customers);
        }        
        [HttpGet]
        public IActionResult CustomerAdd()
        {
            return View();
        }
    }
}
