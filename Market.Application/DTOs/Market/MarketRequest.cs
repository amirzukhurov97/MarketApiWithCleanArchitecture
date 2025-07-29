using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Market
{
    public record MarketRequest : EntityBaseRequest
    {
        public double Quantity { get; set; }
        public Guid ProductId { get; set; }
    }
}
