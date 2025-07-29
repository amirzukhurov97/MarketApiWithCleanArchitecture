using Market.Application.Interfacies;
using Market.Domain.Models;
using Market.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Infrastructure.Repositories
{
    public class AuthRepository(ApplicationDbContext context) : Repository<Auth>(context), IAuthRepository
    {
    }
}
