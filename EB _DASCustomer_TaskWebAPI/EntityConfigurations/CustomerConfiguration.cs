using EB__DASCustomer_TaskWebAPI.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EB__DASCustomer_TaskWebAPI.EntityConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            //Bu kod, customer tablomun yapılandırmasını tanımlar
            builder.ToTable("Customers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired().HasColumnName("FirstName");
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired().HasColumnName("LastName");
            builder.Property(x => x.PhoneNumber).HasMaxLength(10).IsRequired().HasColumnName("PhoneNumber");
            builder.Property(x => x.BirthDay).IsRequired().HasColumnName("BirthDay");
            builder.Property(x => x.Email).HasMaxLength(255).IsRequired().HasColumnName("Email");
            builder.HasQueryFilter(x => !x.IsDeleted);
            builder.Property(x => x.CreatedDate).HasColumnName("CreatedDate");
            builder.Property(x => x.UpdateDate).HasColumnName("UpdateDate");
            builder.Property(x => x.DeletedDate).HasColumnName("DeletedDate");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
