using MarketApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);            
            builder.HasIndex(u => u.Email)
                .IsUnique();

        }
    }
}
