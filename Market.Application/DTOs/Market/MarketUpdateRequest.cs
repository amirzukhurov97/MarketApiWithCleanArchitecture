using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Market
{
    public record MarketUpdateRequest : EntityBaseUpdateRequest
    {
        public double Quantity { get; set; }
        public Guid ProductId { get; set; }
    }
}
