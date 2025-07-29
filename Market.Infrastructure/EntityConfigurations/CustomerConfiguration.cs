using MarketApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.EntityConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.PhoneNumber)
                .HasMaxLength(50);

            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Address)
                .WithMany(a => a.Customers)
                .HasForeignKey(a => a.AddressId);            
        }
    }
}
