using Market.Domain.Abstract.Entity;

namespace MarketApi.Models
{
    public class Cart : EntityBase
    {
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalPriceUSD { get; set; }
        
    }
}
