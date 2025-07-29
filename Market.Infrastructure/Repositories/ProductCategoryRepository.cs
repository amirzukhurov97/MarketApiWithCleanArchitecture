using Market.Infrastructure.DataBase;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Infrastructure.Repositories
{
    public class ProductCategoryRepository(ApplicationDbContext context) : Repository<ProductCategory>(context), IProductCategoryRepository
    {
    }
}
