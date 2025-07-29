using Market.Application.Interfacies;
using Market.Infrastructure.DataBase;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : Repository<User>(context),IUserRepository
    {
        public override IQueryable<User> GetAll()
        {
            return _context.Users.Include(pc => pc.Role);
        }
    }
}
