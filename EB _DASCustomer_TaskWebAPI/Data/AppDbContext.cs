using EB__DASCustomer_TaskWebAPI.Entites;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EB__DASCustomer_TaskWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}