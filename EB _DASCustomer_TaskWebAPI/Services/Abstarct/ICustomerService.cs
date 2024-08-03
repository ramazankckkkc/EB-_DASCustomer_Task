using EB__DASCustomer_TaskWebAPI.Dtos;
using EB__DASCustomer_TaskWebAPI.Entites;
using System.Linq.Expressions;

namespace EB__DASCustomer_TaskWebAPI.Services.Abstarct
{
    public interface ICustomerService
    {
        Task<Customer> AddAsync(CreateCustomerDto createCustomerDto);
        Task<Customer> UpdateAsync(UpdateCustomerDto updateCustomerDto, string id);
        Task<Customer> DeleteteAsync(string id);
        Task<Customer> GetAsync(Expression<Func<Customer,bool>> expression);
        Task<List<GetAllCustomerDto>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}