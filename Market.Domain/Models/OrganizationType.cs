using Market.Domain.Abstract.Entity;

namespace MarketApi.Models
{
    public class OrganizationType : EntityBase
    {
        public string? Name { get; set; }
        public List<Organization> Organizations { get; set; } = new List<Organization>();
    }
}
