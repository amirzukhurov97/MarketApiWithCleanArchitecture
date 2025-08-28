using Market.Infrastructure.DataBase;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Infrastructure.Repositories
{
    public class SaleRepository(ApplicationDbContext context) : Repository<Sell>(context), ISaleRepository
    {
    }
}
