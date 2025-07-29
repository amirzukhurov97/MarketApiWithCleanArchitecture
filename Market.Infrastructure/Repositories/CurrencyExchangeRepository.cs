using Market.Infrastructure.DataBase;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Market.Infrastructure.Repositories
{
    public class CurrencyExchangeRepository(ApplicationDbContext context) : Repository<CurrencyExchange>(context), ICurrencyExchangeRepository
    {
        public decimal GetActual()
        {
            CurrencyExchange? exchangeRate = _context.CurrencyExchange.LastOrDefault();
            if(exchangeRate == null)
            {
                return 0;
            }
            return exchangeRate.USDtoTJS;
        }
    }
}
