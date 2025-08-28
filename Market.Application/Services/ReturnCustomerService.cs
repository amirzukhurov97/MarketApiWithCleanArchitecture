using AutoMapper;
using Market.Application.DTOs.Purchase;
using Market.Application.DTOs.Report;
using Market.Application.DTOs.ReturnCustomer;
using Market.Application.DTOs.Sell;
using Market.Application.SeviceInterfacies;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Application.Services
{
    public class ReturnCustomerService(IReturnCustomerRepository repository, IMarketRopository marketRopository, ICurrencyExchangeRepository currency, IMapper mapper) : IReturnCustomerService<ReturnCustomerRequest, ReturnCustomerUpdateRequest, ReturnCustomerResponse>
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
            try
            {
                var result = repository.GetAll(pageSize, pageNumber).ToList();
                return mapper.Map<List<ReturnCustomerResponse>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ReturnCustomerResponse> GetReport(ReportModel reportModel)
        {
            try
            {
                var query = repository.GetAll()
                    .Include(pc => pc.Product)
                    .Include(pm => pm.Customer)
                    .AsQueryable();

                if (reportModel.ProductId != null)
                {
                    query = query.Where(p => p.ProductId == reportModel.ProductId);
                }

                if (reportModel.OrganizationOrCustomerId != null)
                {
                    query = query.Where(p => p.CustomerId == reportModel.OrganizationOrCustomerId);
                }

                if (reportModel.StartDate != null)
                {
                    query = query.Where(p => p.Date >= reportModel.StartDate);
                }

                if (reportModel.EndDate != null)
                {
                    query = query.Where(p => p.Date <= reportModel.EndDate);
                }

                var reportReturnCustomers = query.ToList();
                return mapper.Map<List<ReturnCustomerResponse>>(reportReturnCustomers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ReturnCustomerResponse GetById(Guid id)
        {
            try
            {
                ReturnCustomerResponse? responses = null;
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
