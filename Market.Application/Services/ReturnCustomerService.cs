using AutoMapper;
using Market.Application.DTOs.ReturnCustomer;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Application.Services
{
    public class ReturnCustomerService(IReturnCustomerRepository repository, IMarketRopository marketRopository, ICurrencyExchangeRepository currency, IMapper mapper) : IGenericService<ReturnCustomerRequest, ReturnCustomerUpdateRequest, ReturnCustomerResponse>
    {
        public string Create(ReturnCustomerRequest item)
        {
            try
            {
                var mapReturnCustomer = mapper.Map<ReturnCustomer>(item);
                if (item.PriceUSD == 0)
                {
                    mapReturnCustomer.PriceUSD = item.Price / currency.GetActual();
                }
                if (item.Price == 0)
                {
                    mapReturnCustomer.Price = item.PriceUSD * currency.GetActual();
                }
                mapReturnCustomer.SumPrice = mapReturnCustomer.Price * Convert.ToDecimal(mapReturnCustomer.Quantity);
                mapReturnCustomer.SumPriceUSD = mapReturnCustomer.PriceUSD * Convert.ToDecimal(mapReturnCustomer.Quantity);
                mapReturnCustomer.Date = DateTime.Now;

                var marketItem = new Stock
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                var marketResponse = marketRopository.Income(marketItem);
                repository.Add(mapReturnCustomer);
                return $"Created new Item with this ID: {mapReturnCustomer.Id}";

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ReturnCustomerResponse> GetAll()
        {
            try
            {
                List<ReturnCustomerResponse>? responses = new List<ReturnCustomerResponse>();
                var purchases = repository.GetAll().Include(pc => pc.Product).Include(pm => pm.Customer).ToList();
                if (purchases.Count > 0)
                {
                    foreach (var purchase in purchases)
                    {
                        var response = mapper.Map<ReturnCustomerResponse>(purchase);
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

        public IEnumerable<ReturnCustomerResponse> GetAll(int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public ReturnCustomerResponse GetById(Guid id)
        {
            try
            {
                ReturnCustomerResponse responses = null;
                var purchaseList = repository.GetById(id).Include(pc => pc.Product).Include(pm => pm.Customer).FirstOrDefault();
                if (purchaseList != null)
                {
                    responses = mapper.Map<ReturnCustomerResponse>(purchaseList);
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
                    return "ReturnCustomer is not found";
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
                return "ReturnCustomer is deleted";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Update(ReturnCustomerUpdateRequest newItem)
        {
            try
            {
                var _item = repository.GetById(newItem.Id).FirstOrDefault();
                if (_item is null)
                {
                    return "ReturnCustomer is not found";
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
                var mapPurchase = mapper.Map<ReturnCustomer>(newItem);
                repository.Update(mapPurchase);
                return "ReturnCustomer is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
