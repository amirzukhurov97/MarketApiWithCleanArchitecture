using Azure;
using MarketApi.Models;

namespace MarketApi.Infrastructure.Interfacies
{
    public interface IMarketRopository
    {
        IQueryable<Stock> GetAll();
        IEnumerable<Stock> GetAll(int pageSize, int pageNumber);
        string Income(Stock item);
        string Expense(Stock item);

    }
}
