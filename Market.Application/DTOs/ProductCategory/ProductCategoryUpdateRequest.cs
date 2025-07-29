using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.ProductCategory
{
    public record ProductCategoryUpdateRequest : EntityBaseUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
