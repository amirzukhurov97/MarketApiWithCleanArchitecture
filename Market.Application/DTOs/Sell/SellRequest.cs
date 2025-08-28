using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Sell
{
    public record SellRequest : EntityBaseRequest
    {
        public decimal Price { get; set; }
        public decimal PriceUSD { get; set; }
        public double Quantity { get; set; }
        public string? Comment { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
