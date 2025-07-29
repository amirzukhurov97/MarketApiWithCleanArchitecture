using AutoMapper;
using Market.Application.DTOs.Sale;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Application.Services
{
    public class SaleService(ISaleRepository repository, IMarketRopository marketRopository, IMapper mapper) : IGenericService<SaleRequest, SaleUpdateRequest, SaleResponse>
    {
        public string Create(SaleRequest item)
        {
            try
            {
                item.SumPrice = item.Price * Convert.ToDecimal(item.Quantity);
                item.SumPriceUSD = item.PriceUSD * Convert.ToDecimal(item.Quantity);
                var mapQuantity = mapper.Map<Sale>(item);
                var marketItem = new Stock
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                var marketResponse = marketRopository.Expense(marketItem);
                repository.Add(mapQuantity);
                return $"Created new newItem with this ID: {mapQuantity.Id}";

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
