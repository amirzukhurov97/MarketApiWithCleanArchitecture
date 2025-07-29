using MarketApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.EntityConfigurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.Property(p => p.Quantity)
                            .IsRequired();
            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.PriceUSD)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.SumPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.SumPriceUSD)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    } 
    
}
