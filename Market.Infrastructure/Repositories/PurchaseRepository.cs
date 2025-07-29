using Market.Infrastructure.DataBase;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Infrastructure.Repositories
{
    public class PurchaseRepository(ApplicationDbContext context) : Repository<Purchase>(context), IPurchaseRepository
    {
    }
}
