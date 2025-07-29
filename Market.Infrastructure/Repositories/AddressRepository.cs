using Market.Infrastructure.DataBase;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Infrastructure.Repositories
{
    public class AddressRepository(ApplicationDbContext context) : Repository<Address>(context), IAddressRepository
    {
    }
}
