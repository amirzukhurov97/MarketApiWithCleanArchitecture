using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Market
{
    public record MarketResponse : EntityBaseResponse
    {
        public double Quantity { get; set; }
        public string? ProductName { get; set; }
    }
}
