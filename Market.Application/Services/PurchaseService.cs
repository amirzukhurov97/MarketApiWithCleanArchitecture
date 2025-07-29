using AutoMapper;
using Market.Application.DTOs.Purchase;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Application.Services
{
    public class PurchaseService(IPurchaseRepository repository, IMarketRopository marketRopository, IMapper mapper) : IGenericService<PurchaseRequest, PurchaseUpdateRequest, PurchaseResponse>
    {
        public string Create(PurchaseRequest item)
        {
            try
            {                
                 item.SumPrice = item.Price * Convert.ToDecimal(item.Quantity);
                    item.SumPriceUSD = item.PriceUSD * Convert.ToDecimal(item.Quantity);
                    var mapQuantity = mapper.Map<Purchase>(item);
                    
                    var marketItem = new Stock
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    };
                    var marketResponse = marketRopository.Income(marketItem);
                    repository.Add(mapQuantity);
                return $"Created new newItem with this ID: {mapQuantity.Id}";   
                
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public IEnumerable<PurchaseResponse> GetAll()
        {
            try
            {
                List<PurchaseResponse>? responses = new List<PurchaseResponse>();
                var purchases = repository.GetAll().Include(pc => pc.Product).Include(pm => pm.Organization).ToList();
                if (purchases.Count > 0)
                {
                    foreach (var purchase in purchases)
                    {
                        var response = mapper.Map<PurchaseResponse>(purchase);
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

        public PurchaseResponse GetById(Guid id)
        {
            try
            {
                PurchaseResponse responses = null;
                var purchaseList = repository.GetById(id).Include(pc => pc.Product).Include(pm => pm.Organization).FirstOrDefault();
                if (purchaseList != null)
                {
                    responses = mapper.Map<PurchaseResponse>(purchaseList);
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
                    return "Purchase is not found";
                }
                if (_item.Quantity > 0)
                {
                    var marketItem = new Stock
                    {
                        ProductId = _item.ProductId,
                        Quantity = _item.Quantity
                    };
                    marketRopository.Expense(marketItem);
                }
                repository.Remove(id);
                return "Purchase is deleted";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Update(PurchaseUpdateRequest newItem)
        {
            try
            {
                var _item = repository.GetById(newItem.Id).FirstOrDefault();
                if (_item is null)
                {
                    return "Purchase is not found";
                }
                if (newItem.Quantity > _item.Quantity)
                {
                    var marketItem = new Stock
                    {
                        ProductId = _item.ProductId,
                        Quantity = newItem.Quantity - _item.Quantity
                    };
                    marketRopository.Income(marketItem);
                }
                else
                {
                    var marketItem = new Stock
                    {
                        ProductId = _item.ProductId,
                        Quantity = _item.Quantity - newItem.Quantity
                    };
                    marketRopository.Expense(marketItem);

                }
                newItem.SumPrice = newItem.Price * Convert.ToDecimal(newItem.Quantity);
                newItem.SumPriceUSD = newItem.PriceUSD * Convert.ToDecimal(newItem.Quantity);
                var mapPurchase = mapper.Map<Purchase>(newItem);
                repository.Update(mapPurchase);
                return "Purchase is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
