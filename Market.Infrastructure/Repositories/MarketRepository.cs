using Market.Infrastructure.DataBase;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Infrastructure.Repositories
{
    public class MarketRepository(ApplicationDbContext context) :  IMarketRopository
    {
        public string Expense(Stock item)
        {
            try
            {
                var market = context.Markets.FirstOrDefault(m => m.ProductId == item.ProductId);

                if (market != null)
                {
                    market.Quantity -= item.Quantity;
                    context.Update(market);
                    //context.SaveChanges();
                    return $"Updated market for product ID: {item.ProductId} with new quantity: {market.Quantity}";
                }
                else
                {
                    var newMarket = new Stock
                    {
                        Id = Guid.NewGuid(),
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    };
                    context.Add(newMarket);
                    //context.SaveChanges();
                    return $"Created new market entry for product ID: {item.ProductId} with quantity: {newMarket.Quantity}";
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public IQueryable<Stock> GetAll()
        {
            try
            {
                return context.Markets.AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Income(Stock item)
        {
            try
            {
                var market = context.Markets.FirstOrDefault(m => m.ProductId == item.ProductId);

                if (market != null)
                {
                    market.Quantity += item.Quantity;
                    context.Update(market);
                    //context.SaveChanges();
                    return $"Updated market for product ID: {item.ProductId} with new quantity: {market.Quantity}";
                }
                else
                {
                    var newMarket = new Stock
                    {
                        Id = Guid.NewGuid(),
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    };
                    context.Add(newMarket);
                    //context.SaveChanges();
                    return $"Created new market entry for product ID: {item.ProductId} with quantity: {newMarket.Quantity}";
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
