using MarketApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.EntityConfigurations
{
    public class ReturnCustomerConfiguration : IEntityTypeConfiguration<ReturnCustomer>
    {
        public void Configure(EntityTypeBuilder<ReturnCustomer> builder)
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
