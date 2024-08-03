using EB__DASCustomer_TaskWebAPI.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EB__DASCustomer_TaskWebAPI.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task ImageAdd(IFormFile file, string imageUrl)
        {
            string imageType = $"{file.ContentType}";
            if (imageType == "image/jpeg")
                imageType = ".jpeg";
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "Images");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var filePath = Path.Combine(uploadsFolder, imageUrl);
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }
}
