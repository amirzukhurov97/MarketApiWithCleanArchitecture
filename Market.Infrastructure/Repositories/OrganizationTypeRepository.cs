using Market.Infrastructure.DataBase;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Infrastructure.Repositories
{
    public class OrganizationTypeRepository(ApplicationDbContext context) : Repository<OrganizationType>(context), IOrganizationTypeRepository
    {
    }
}
