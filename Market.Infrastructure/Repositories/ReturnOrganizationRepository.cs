using Market.Infrastructure.DataBase;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Infrastructure.Repositories
{
    public class ReturnOrganizationRepository(ApplicationDbContext context) : Repository<ReturnOrganization>(context), IReturnOrganizationRepository
    {
    }
}
