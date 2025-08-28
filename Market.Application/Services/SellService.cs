using AutoMapper;
using Market.Application.DTOs.Purchase;
using Market.Application.DTOs.Report;
using Market.Application.DTOs.Sell;
using Market.Application.SeviceInterfacies;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Application.Services
{
    public class SellService(ISaleRepository repository, IMarketRopository marketRopository, ICurrencyExchangeRepository currency, IMapper mapper) : ISellService<SellRequest, SellUpdateRequest, SellResponse>
    {
        public string Create(SellRequest item)
        {
            try
            {
                var mapSale = mapper.Map<Sell>(item);
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

        public IEnumerable<SellResponse> GetAll()
        {
            try
            {
                List<SellResponse>? responses = new List<SellResponse>();
                var purchases = repository.GetAll().Include(pc => pc.Product).Include(pm => pm.Customer).ToList();
                if (purchases.Count > 0)
                {
                    foreach (var purchase in purchases)
                    {
                        var response = mapper.Map<SellResponse>(purchase);
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

        public IEnumerable<SellResponse> GetAll(int pageSize, int pageNumber)
        {
            try
            {
                var result = repository.GetAll(pageSize, pageNumber).ToList();
                return mapper.Map<List<SellResponse>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SellResponse> GetReport(ReportModel reportModel)
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

                var reportSells = query.ToList();
                return mapper.Map<List<SellResponse>>(reportSells);
            }
            catch (Exception)
            {
                throw; // здесь можно логировать через Serilog вместо простого throw
            }
        }
        public SellResponse GetById(Guid id)
        {
            try
            {
                SellResponse? responses = null;
                var purchaseList = repository.GetById(id).Include(pc => pc.Product).Include(pm => pm.Customer).FirstOrDefault();
                if (purchaseList != null)
                {
                    responses = mapper.Map<SellResponse>(purchaseList);
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
                    return "Sell is not found";
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
                return "Sell is deleted";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Update(SellUpdateRequest newItem)
        {
            try
            {
                var _item = repository.GetById(newItem.Id).FirstOrDefault();
                if (_item is null)
                {
                    return "Sell is not found";
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
                var mapPurchase = mapper.Map<Sell>(newItem);
                repository.Update(mapPurchase);
                return "Sell is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
