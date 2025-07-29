using Market.Infrastructure.DataBase;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Infrastructure.Repositories
{
    public class MeasurementRepository(ApplicationDbContext context) : Repository<Measurement>(context), IMeasurementRepository
    {
    }
}
