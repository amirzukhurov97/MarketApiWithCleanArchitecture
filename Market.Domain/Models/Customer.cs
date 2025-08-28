using Market.Domain.Abstract.Entity;

namespace MarketApi.Models
{
    public class Customer : EntityBase
    {
        public string? Name { get; set; } 
        public string? PhoneNumber { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; } = null!;

        public List<Sell> Sales { get; set; } = new List<Sell>();
        public List<ReturnCustomer> ReturnCustomers { get; set; } = new List<ReturnCustomer>();
    }
}
