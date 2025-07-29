using AutoMapper;
using Market.Application.DTOs.ReturnOrganization;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Application.Services
{
    public class ReturnOrganizationService(IReturnOrganizationRepository repository, IMarketRopository marketRopository, IMapper mapper) : IGenericService<ReturnOrganizationRequest, ReturnOrganizationUpdateRequest, ReturnOrganizationResponse>
    {
        public string Create(ReturnOrganizationRequest item)
        {
            try
            {
                item.SumPrice = item.Price * Convert.ToDecimal(item.Quantity);
                item.SumPriceUSD = item.PriceUSD * Convert.ToDecimal(item.Quantity);
                var mapQuantity = mapper.Map<ReturnOrganization>(item);
                
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

        public IEnumerable<ReturnOrganizationResponse> GetAll()
        {
            try
            {
                List<ReturnOrganizationResponse>? responses = new List<ReturnOrganizationResponse>();
                var purchases = repository.GetAll().Include(pc => pc.Product).Include(pm => pm.Organization).ToList();
                if (purchases.Count > 0)
                {
                    foreach (var purchase in purchases)
                    {
                        var response = mapper.Map<ReturnOrganizationResponse>(purchase);
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

        public ReturnOrganizationResponse GetById(Guid id)
        {
            try
            {
                ReturnOrganizationResponse responses = null;
                var purchaseList = repository.GetById(id).Include(pc => pc.Product).Include(pm => pm.Organization).FirstOrDefault();
                if (purchaseList != null)
                {
                    responses = mapper.Map<ReturnOrganizationResponse>(purchaseList);
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
                    return "ReturnOrganization is not found";
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
                return "ReturnOrganization is deleted";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Update(ReturnOrganizationUpdateRequest newItem)
        {
            try
            {
                var _item = repository.GetById(newItem.Id).FirstOrDefault();
                if (_item is null)
                {
                    return "ReturnOrganization is not found";
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
                    marketRopository.Income(marketItem);
                }
                newItem.SumPrice = newItem.Price * Convert.ToDecimal(newItem.Quantity);
                newItem.SumPriceUSD = newItem.PriceUSD * Convert.ToDecimal(newItem.Quantity);
                var mapPurchase = mapper.Map<ReturnOrganization>(newItem);
                repository.Update(mapPurchase);
                return "ReturnOrganization is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
