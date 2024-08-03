using EB__DASCustomer_TaskWebAPI.Dtos;
using EB__DASCustomer_TaskWebAPI.Entites;
using EB__DASCustomer_TaskWebAPI.Exceptions;
using EB__DASCustomer_TaskWebAPI.Services;
using EB__DASCustomer_TaskWebAPI.Services.Abstarct;
using EB__DASCustomer_TaskWebAPI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EB__DASCustomer_TaskWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ImageService _imageService;
        public CustomersController(
            ILogger<CustomersController> logger,
            ICustomerService customerService,
            IWebHostEnvironment webHostEnvironment,
            ImageService imageService)
        {
            _imageService = imageService;
            _customerService = customerService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        /// <summary>
        /// Yeni bir müşteri oluşturur.
        /// </summary>
        /// <param name="createCustomerDto">Müşteri ekleme modeli.</param>
        /// <returns>Oluşturulan müşteri bilgileri.</returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm] CreateCustomerDto createCustomerDto)
        {
            //verilerin geliş şekli console loglar incelleniyor
            _logger.LogInformation(JsonSerializer.Serialize(createCustomerDto));
            // Dogrulama işlemleri veritabanına eklencek verilerin.
            var validator = new CreateCustomerDtoValidator();
            var validateResult = await validator.ValidateAsync(createCustomerDto);
            if (!validateResult.IsValid) throw new ValidationException(validateResult.Errors);

            //Business Rule(iş kuralı)
            var getById = await _customerService.GetAsync(x => x.Email == createCustomerDto.Email);
            if (getById is not null) throw new BadRequestException($"{createCustomerDto.Email} email adresi zaten kayıtlı başka email adresiyle kayıt olunuz");

            //veritabanına veri ekleme
            Customer addedCustomer = await _customerService.AddAsync(createCustomerDto);
            //resim ekleme
            await _imageService.ImageAdd(createCustomerDto.Image!, addedCustomer.ImageUrl!);

            return Ok(addedCustomer);
        }
        /// <summary>
        /// Tüm müşterileri getirir.
        /// </summary>
        /// <returns>Tüm müşterilerin listesi.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Verileri listeleme
            return Ok(await _customerService.GetAllAsync());
        }
        /// <summary>
        /// Belirtilen ID'ye sahip müşteriyi getirir.
        /// </summary>
        /// <param name="id">Müşteri ID'si.</param>
        /// <returns>Belirtilen ID'ye sahip müşteri.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            // id ye göre veri çekme
            Customer getById = await _customerService.GetAsync(x => x.Id == id);
            if (getById is null) throw new NotFoundException(name: "Customer", key: id);

            return Ok(getById);
        }
        /// <summary>
        /// Mevcut bir müşteriyi günceller.
        /// </summary>
        /// <param name="updateCustomerDto">Müşteri güncelleme modeli.</param>
        /// <returns>Güncellenen müşteri bilgileri.</returns>
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromForm] UpdateCustomerDto updateCustomerDto)
        {
            //verilerin geliş şekli console loglar incelleniyor
            _logger.LogInformation(JsonSerializer.Serialize(updateCustomerDto));
            // Dogrulama işlemleri veritabanına eklencek verilerin.
            var validator = new UpdateCustomerDtoValidator();
            var validateResult = await validator.ValidateAsync(updateCustomerDto);
            if (!validateResult.IsValid) throw new ValidationException(validateResult.Errors);
            //güncelleme işlemi burada email alanı unique oldugu için başka kullanıclarda bu email varsa güncellenemez.
            var getById = await _customerService.GetAsync(x => x.Email == updateCustomerDto.Email && x.Id != id);
            if (getById is not null) throw new BadRequestException($"{updateCustomerDto.Email} email adresi zaten kayıtlı lütfen başka email adresi girin.");

            var updatedCustomer = await _customerService.UpdateAsync(updateCustomerDto: updateCustomerDto, id: id);
            //resim ekleme
            if (updateCustomerDto.Image is not null)
                await _imageService.ImageAdd(updateCustomerDto.Image!, updatedCustomer.ImageUrl);
            return Ok(updatedCustomer);
        }
        /// <summary>
        /// Belirtilen ID'ye sahip müşteriyi siler.
        /// </summary>
        /// <param name="id">Müşteri ID'si.</param>
        /// <returns>Silme işleminin sonucu.</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            return Ok(await _customerService.DeleteteAsync(id));
        }
    }
}