using Market.Domain.Models;
using Market.Infrastructure.DataBase;
using Market.Application.Interfacies;

namespace Market.Infrastructure.Repositories
{
    public class RoleRepository(ApplicationDbContext context) : Repository<Role>(context), IRoleRepository
    {
    }
}
