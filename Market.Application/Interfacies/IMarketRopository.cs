using MarketApi.Models;

namespace MarketApi.Infrastructure.Interfacies
{
    public interface IMarketRopository
    {
        IQueryable<Stock> GetAll();
        string Income(Stock item);
        string Expense(Stock item);
    }
}
