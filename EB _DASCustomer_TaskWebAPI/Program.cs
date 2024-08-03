using EB__DASCustomer_TaskWebAPI.Data;
using EB__DASCustomer_TaskWebAPI.Exceptions;
using EB__DASCustomer_TaskWebAPI.Services;
using EB__DASCustomer_TaskWebAPI.Services.Abstarct;
using EB__DASCustomer_TaskWebAPI.Services.Concrete;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerService, CustomerManager>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDatabase"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
              builder =>
              {
                  builder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader();
              });
});
//Cross-Cutting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler(options => { });

app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
