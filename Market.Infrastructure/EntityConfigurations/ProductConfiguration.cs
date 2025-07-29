using MarketApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p=>p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.ProductCategory)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => p.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Measurement)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => p.MeasurementId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
