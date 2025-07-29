using MarketApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.EntityConfigurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasKey(o => o.Id);

        }
    }
}
