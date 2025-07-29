using MarketApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.EntityConfigurations
{
    public class MarketConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasOne(m => m.Product);
        }
    }
}
