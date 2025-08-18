using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Purchase
{
    public record PurchaseRequest : EntityBaseRequest
    {
        public decimal Price { get; set; }
        public decimal PriceUSD { get; set; }
        public double Quantity { get; set; }
        public string? Comment { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
