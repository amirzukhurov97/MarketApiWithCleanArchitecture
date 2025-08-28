using AutoMapper;
using Market.Application.DTOs.Market;
using MarketApi.Infrastructure.Interfacies;
using Microsoft.EntityFrameworkCore;

namespace Market.Application.Services
{
    public class MarketService(IMarketRopository repository, IMapper mapper)
    {

        public IEnumerable<MarketResponse> GetAll()
        {
            try
            {
                List<MarketResponse>? responses = new List<MarketResponse>();
                var products = repository.GetAll().Include(pc => pc.Product).ToList();
                if (products.Count > 0)
                {
                    foreach (var puroduct in products)
                    {
                        var response = mapper.Map<MarketResponse>(puroduct);
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

        public IEnumerable<MarketResponse> GetAll(int pageSize, int pageNumber)
        {
            try
            {
                var resultPage = repository.GetAll(pageSize, pageNumber).ToList();
                return mapper.Map<List<MarketResponse>>(resultPage);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
