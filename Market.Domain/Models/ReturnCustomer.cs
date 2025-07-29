using Market.Domain.Abstract;

namespace MarketApi.Models
{
    public class ReturnCustomer : ProductBase
    {       
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
