using Market.Domain.Abstract;

namespace MarketApi.Models
{
    public class Sale : ProductBase
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
