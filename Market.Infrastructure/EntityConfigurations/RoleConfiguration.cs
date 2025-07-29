using Market.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(o => o.Name)
               .IsRequired()
               .HasMaxLength(50);

        }
    }
}
