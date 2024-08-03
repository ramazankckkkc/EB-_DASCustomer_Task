using EB__DASCustomer_TaskWebAPI.Data;
using EB__DASCustomer_TaskWebAPI.Dtos;
using EB__DASCustomer_TaskWebAPI.Entites;
using EB__DASCustomer_TaskWebAPI.Exceptions;
using EB__DASCustomer_TaskWebAPI.Services.Abstarct;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EB__DASCustomer_TaskWebAPI.Services.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly AppDbContext _appDbContext;

        public CustomerManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Customer> AddAsync(CreateCustomerDto createCustomerDto)
        {
            Customer customer = new()
            {
                BirthDay = createCustomerDto.BirthDay,
                CreatedDate = DateTime.Now.AddHours(3),
                Email = createCustomerDto.Email,
                FirstName = createCustomerDto.FirstName,
                LastName = createCustomerDto.LastName,
                PhoneNumber = createCustomerDto.PhoneNumber,
                Id = Guid.NewGuid().ToString(),
                ImageUrl = GenerateImageName(createCustomerDto.Image.FileName)
            };
            await _appDbContext.Customers.AddAsync(customer);
            await _appDbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> DeleteteAsync(string id)
        {
            Customer? getById = await GetAsync(x => x.Id == id);
            if (getById is null) throw new NotFoundException(name: "Customer", key: id);

            getById.IsDeleted = true;
            getById.DeletedDate = DateTime.Now.AddHours(3);
            _appDbContext.Customers.Update(getById);
            await _appDbContext.SaveChangesAsync();
            return getById;
        }

        public async Task<List<GetAllCustomerDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Customer> getList = _appDbContext.Customers.AsNoTracking().OrderByDescending(x => x.CreatedDate);
            List<Customer> result = await getList.ToListAsync();
            if (!result.Any()) return new List<GetAllCustomerDto>();

            List<GetAllCustomerDto> getAllCustomerDtos = result.Select(x => new GetAllCustomerDto
            {
                BirthDay = x.BirthDay,
                CreatedDate = x.CreatedDate,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DeletedDate = x.DeletedDate,
                PhoneNumber = x.PhoneNumber,
                Id = x.Id,
                UpdateDate = x.UpdateDate,
                ImageUrl = x.ImageUrl
            }).ToList();
            return getAllCustomerDtos;
        }

        public async Task<Customer> GetAsync(Expression<Func<Customer, bool>> expression)
        {
            Customer? getById = await _appDbContext.Customers.FirstOrDefaultAsync(expression);
            return getById;
        }
        public async Task<Customer> UpdateAsync(UpdateCustomerDto updateCustomerDto, string id)
        {
            Customer? getById = await GetAsync(x => x.Id == id);
            if (getById is null) throw new NotFoundException(name: "Customer", key: id);

            getById.UpdateDate = DateTime.Now.AddHours(3);
            getById.PhoneNumber = updateCustomerDto.PhoneNumber;
            getById.Email = updateCustomerDto.Email;
            getById.FirstName = updateCustomerDto.FirstName;
            getById.LastName = updateCustomerDto.LastName;
            getById.BirthDay = updateCustomerDto.BirthDay;
            if (updateCustomerDto.Image is not null)
                getById.ImageUrl = GenerateImageName(updateCustomerDto.Image.FileName);
            _appDbContext.Customers.Update(getById);
            await _appDbContext.SaveChangesAsync();
            return getById;
        }
        private string GenerateImageName(string? fileName)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(fileName).Take(10).ToArray()).Replace(" ", "-");
            Random random = new Random();
            int randomNumber = random.Next(1, 100000);
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + randomNumber + Path.GetExtension(fileName);
            return imageName;
        }
    }
}