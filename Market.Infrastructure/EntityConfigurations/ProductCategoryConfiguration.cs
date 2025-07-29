using MarketApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.EntityConfigurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(50);

            builder.HasKey(o => o.Id);

        }
    }
}
