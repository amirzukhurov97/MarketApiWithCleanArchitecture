using Market.Domain.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MarketApi.Models
{
    public class Purchase : ProductBase
    {        
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; } = null!;
    }
}
