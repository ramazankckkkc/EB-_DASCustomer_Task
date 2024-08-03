namespace EB__DASCustomer_TaskMVCWeb.Models
{
    public class CreateCustomerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public IFormFile Image { get; set; }
    }
}
