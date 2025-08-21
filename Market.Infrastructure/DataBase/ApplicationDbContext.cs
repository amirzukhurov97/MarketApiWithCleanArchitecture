using Market.Domain.Models;
using Market.Infrastructure.EntityConfigurations;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Infrastructure.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Organization> Organizations { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;
        public DbSet<Purchase> Purchases { get; set; } = null!;
        public DbSet<ReturnCustomer> ReturnCustomers { get; set; } = null!;
        public DbSet<ReturnOrganization> ReturnOrganizations { get; set; } = null!;
        public DbSet<OrganizationType> OrganizationType { get; set; } = null!;
        public DbSet<Measurement> Measurements { get; set; } = null!;
        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<CurrencyExchange> CurrencyExchange { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Stock> Markets { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureDeleted();
            //    Database.EnsureCreated();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ProductConfiguration).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
