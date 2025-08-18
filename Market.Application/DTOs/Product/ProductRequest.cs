using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Product
{
    public record ProductRequest : EntityBaseRequest
    {
        public string Name { get; set; } = string.Empty;
        public double Capacity { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public Guid MeasurementId { get; set; }
        public Guid ProductCategoryId { get; set; }
    }
}
