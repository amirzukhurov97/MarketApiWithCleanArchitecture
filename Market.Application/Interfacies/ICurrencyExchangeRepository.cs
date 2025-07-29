using MarketApi.Models;

namespace MarketApi.Infrastructure.Interfacies
{
    public interface ICurrencyExchangeRepository : IRepository<CurrencyExchange>
    {
        decimal GetActual();
    }
}
