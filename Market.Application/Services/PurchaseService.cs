using AutoMapper;
using Market.Application.DTOs.Purchase;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Application.Services
{
    public class PurchaseService(IPurchaseRepository repository, IMarketRopository marketRopository, IMapper mapper, ICurrencyExchangeRepository currency) : IGenericService<PurchaseRequest, PurchaseUpdateRequest, PurchaseResponse>
    {
        public string Create(PurchaseRequest item)
        {
            try
            {                
                var mapPurchase = mapper.Map<Purchase>(item);
                if (item.PriceUSD == 0)
                {
                    mapPurchase.PriceUSD = item.Price / currency.GetActual();
                }
                if (item.Price == 0)
                {
                    mapPurchase.Price = item.PriceUSD * currency.GetActual();
                }
                mapPurchase.SumPrice = mapPurchase.Price * Convert.ToDecimal(mapPurchase.Quantity);
                mapPurchase.SumPriceUSD = mapPurchase.PriceUSD * Convert.ToDecimal(mapPurchase.Quantity);
                mapPurchase.Date = DateTime.Now;

                var marketItem = new Stock
                 {
                     ProductId = item.ProductId,
                     Quantity = item.Quantity
                 };
                 var marketResponse = marketRopository.Income(marketItem);
                 var purchaseRespoce = repository.Add(mapPurchase);
                return $"Created new newItem with this ID: {purchaseRespoce.ProductId}";   
                
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
