using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.ProductCategory
{
    public record ProductCategoryRequest : EntityBaseRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
