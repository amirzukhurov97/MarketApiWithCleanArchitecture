using Market.Domain.Abstract;

namespace MarketApi.Models
{
    public class ReturnOrganization : ProductBase
    {
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; } = null!;
    }
}
