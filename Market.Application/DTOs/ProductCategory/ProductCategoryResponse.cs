using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.ProductCategory
{
    public record ProductCategoryResponse : EntityBaseResponse
    {
        public string Name { get; set; } = string.Empty;
    }
}
