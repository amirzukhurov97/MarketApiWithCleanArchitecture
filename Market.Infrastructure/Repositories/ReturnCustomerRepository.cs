using Market.Infrastructure.DataBase;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Infrastructure.Repositories
{
    public class ReturnCustomerRepository(ApplicationDbContext context) : Repository<ReturnCustomer>(context), IReturnCustomerRepository
    {
    }
}
