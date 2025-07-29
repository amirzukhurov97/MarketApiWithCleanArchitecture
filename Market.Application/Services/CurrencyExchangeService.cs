using AutoMapper;
using Market.Application.DTOs.CurrencyExchange;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Application.Services
{
    public class CurrencyExchangeService(ICurrencyExchangeRepository repository, IMapper mapper) : IGenericService<CurrencyExchangeRequest, CurrencyExchangeUpdateRequest, CurrencyExchangeResponse>
    {
        public string Create(CurrencyExchangeRequest item)
        {
            if (item?.USDtoTJS == null)
            {
                return "USDtoTJS cannot be empty";
            }
            else
            {
                var mapUSDtoTJS = mapper.Map<CurrencyExchange>(item);
                repository.Add(mapUSDtoTJS);
                return $"Created new item with this ID: {mapUSDtoTJS.Id}";
            }
        }

        public IEnumerable<CurrencyExchangeResponse> GetAll()
        {
            try
            {
                List<CurrencyExchangeResponse>? responses = new List<CurrencyExchangeResponse>();
                var purchases = repository.GetAll().ToList();
                if (purchases.Count > 0)
                {
                    foreach (var purchase in purchases)
                    {
                        var response = mapper.Map<CurrencyExchangeResponse>(purchase);
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

        public CurrencyExchangeResponse GetById(Guid id)
        {
            try
            {
                CurrencyExchangeResponse responses = null;
                var exchangeList = repository.GetById(id).ToList();
                if (exchangeList.Count > 0)
                {
                    responses = mapper.Map<CurrencyExchangeResponse>(exchangeList[0]);
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
            var _item = repository.GetById(id).ToList();
            if (_item.Count() == 0)
            {
                return "CurrencyExchange is not found";
            }
            repository.Remove(id);

            return "CurrencyExchange is deleted";
        }

        public string Update(CurrencyExchangeUpdateRequest item)
        {
            try
            {
                var _item = repository.GetById(item.Id).ToList();
                if (_item is null)
                {
                    return "Purchase is not found";
                }
                var mapCurrencyExchange = mapper.Map<CurrencyExchange>(item);
                repository.Update(mapCurrencyExchange);
                return "CurrencyExchange is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
        public decimal CurrencyRate()
        {
            try
            {
                return repository.GetActual();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving the actual currency exchange rate.", ex);
            }
        }
    }
}
