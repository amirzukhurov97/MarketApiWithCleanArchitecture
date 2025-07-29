using Market.Domain.Abstract.Entity;

namespace MarketApi.Models
{
    public class Address : EntityBase
    {
        public string? Name { get; set; }
        public List<Organization> Organizations { get; set; } = new List<Organization>();
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}
