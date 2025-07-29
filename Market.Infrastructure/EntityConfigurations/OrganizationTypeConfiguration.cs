using MarketApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.EntityConfigurations
{
    public class OrganizationTypeConfiguration : IEntityTypeConfiguration<OrganizationType>
    {
        public void Configure(EntityTypeBuilder<OrganizationType> builder)
        {
            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasKey(o => o.Id);

        }
    }
}
