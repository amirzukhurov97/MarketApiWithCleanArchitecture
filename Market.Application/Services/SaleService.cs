using AutoMapper;
using Market.Application.DTOs.Sale;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Application.Services
{
    public class SaleService(ISaleRepository repository, IMarketRopository marketRopository, ICurrencyExchangeRepository currency, IMapper mapper) : IGenericService<SaleRequest, SaleUpdateRequest, SaleResponse>
    {
        public string Create(SaleRequest item)
        {
            try
            {
                var mapSale = mapper.Map<Sale>(item);
                if (item.PriceUSD == 0)
                {
                    mapSale.PriceUSD = item.Price / currency.GetActual();
                }
                if (item.Price == 0)
                {
                    mapSale.Price = item.PriceUSD * currency.GetActual();
                }
                mapSale.SumPrice = mapSale.Price * Convert.ToDecimal(mapSale.Quantity);
                mapSale.SumPriceUSD = mapSale.PriceUSD * Convert.ToDecimal(mapSale.Quantity);
                mapSale.Date = DateTime.Now;
                var marketItem = new Stock
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                var marketResponse = marketRopository.Expense(marketItem);
                repository.Add(mapSale);
                return $"Created new newItem with this ID: {mapSale.Id}";

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SaleResponse> GetAll()
        {
            try
            {
                List<SaleResponse>? responses = new List<SaleResponse>();
                var purchases = repository.GetAll().Include(pc => pc.Product).Include(pm => pm.Customer).ToList();
                if (purchases.Count > 0)
                {
                    foreach (var purchase in purchases)
                    {
                        var response = mapper.Map<SaleResponse>(purchase);
                        responses.Add(response);
                    }
                }
                return responses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SaleResponse GetById(Guid id)
        {
            try
            {
                SaleResponse responses = null;
                var purchaseList = repository.GetById(id).Include(pc => pc.Product).Include(pm => pm.Customer).FirstOrDefault();
                if (purchaseList != null)
                {
                    responses = mapper.Map<SaleResponse>(purchaseList);
                }
                return responses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Remove(Guid id)
        {
            try
            {
                var _item = repository.GetById(id).FirstOrDefault();
                if (_item is null)
                {
                    return "Sale is not found";
                }
                if (_item.Quantity > 0)
                {
                    var marketItem = new Stock
                    {
                        ProductId = _item.ProductId,
                        Quantity = _item.Quantity
                    };
                    marketRopository.Income(marketItem);
                }
                repository.Remove(id);
                return "Sale is deleted";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Update(SaleUpdateRequest newItem)
        {
            try
            {
                var _item = repository.GetById(newItem.Id).FirstOrDefault();
                if (_item is null)
                {
                    return "Sale is not found";
                }
                if (newItem.Quantity > _item.Quantity)
                {
                    var marketItem = new Stock
                    {
                        ProductId = _item.ProductId,
                        Quantity = newItem.Quantity - _item.Quantity
                    };
                    marketRopository.Expense(marketItem);
                }
                else
                {
                    var marketItem = new Stock
                    {
                        ProductId = _item.ProductId,
                        Quantity = _item.Quantity - newItem.Quantity
                    };
                    marketRopository.Income(marketItem);

                }
                newItem.SumPrice = newItem.Price * Convert.ToDecimal(newItem.Quantity);
                newItem.SumPriceUSD = newItem.PriceUSD * Convert.ToDecimal(newItem.Quantity);
                var mapPurchase = mapper.Map<Sale>(newItem);
                repository.Update(mapPurchase);
                return "Sale is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
