using Market.Infrastructure.DataBase;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Infrastructure.Repositories
{
    public class CustomerRepository(ApplicationDbContext context) : Repository<Customer>(context), ICustomerRepository
    {
       
    }
}
