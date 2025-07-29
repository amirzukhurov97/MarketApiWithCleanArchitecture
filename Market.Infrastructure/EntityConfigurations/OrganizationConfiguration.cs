using MarketApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.EntityConfigurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Address)
                .WithMany(a => a.Organizations)
                .HasForeignKey(a => a.AddressId);

            builder.HasOne(o => o.OrganizationType)
                .WithMany(a => a.Organizations)
                .HasForeignKey(a => a.OrganizationTypeId);
        }
    }
}
