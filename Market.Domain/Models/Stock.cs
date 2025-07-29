using Market.Domain.Abstract.Entity;

namespace MarketApi.Models
{
    public class Stock : EntityBase
    {
        public double Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
