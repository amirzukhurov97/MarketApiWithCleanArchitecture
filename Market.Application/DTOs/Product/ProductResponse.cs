using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Product
{
    public record ProductResponse : EntityBaseResponse
    {
        public string Name { get; set; }
        public double Capacity { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? MeasurementName { get; set; }
        public string? ProductCategoryName { get; set; }
    }
}
