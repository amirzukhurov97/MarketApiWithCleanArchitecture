using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;
using Market.Domain.Abstract.Entity;

namespace MarketApi.Models
{
    public class Product : EntityBase
    {
        public string? Name { get; set; }
        public double Capacity { get; set; } 
        public string? Description { get; set; }
        public Guid ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; } = null!;
        public Guid MeasurementId { get; set; }
        public Measurement Measurement { get; set; } = null!;
        public List<Purchase> Purchases { get; set; } = new List<Purchase>();
        public List<Sale> Sales { get; set; } = new List<Sale>();
        public List<ReturnCustomer> ReturnCustomers { get; set; } = new List<ReturnCustomer>();
        public List<ReturnOrganization> ReturnOrganizations { get; set; } = new List<ReturnOrganization>();
    }
}
