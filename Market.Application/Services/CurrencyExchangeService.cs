using AutoMapper;
using Market.Application.DTOs.CurrencyExchange;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

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

        public IEnumerable<CurrencyExchangeResponse> GetAll(int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
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

        //public async Task<CurrencyExchangeResponse?> GetExchangeRateFromNbtAsync()
        //{
        //    try
        //    {
        //        var today = DateTime.Today.ToString("yyyy-MM-d");
        //        var url = $"https://nbt.tj/ru/kurs/export_xml_dynamic.php?d1={today}&d2={today}&cn=840&cs=USD&export=xml";

        //        using var httpClient = new HttpClient();
        //        using var response = await httpClient.GetAsync(url);
        //        if (!response.IsSuccessStatusCode)
        //            return null;

        //        // Прочитать контент с кодировкой Windows-1251
        //        var stream = await response.Content.ReadAsStreamAsync();
        //        using var reader = new StreamReader(stream, Encoding.GetEncoding("windows-1251"));
        //        var xmlContent = await reader.ReadToEndAsync();

        //        var xdoc = XDocument.Parse(xmlContent);
        //        var record = xdoc.Descendants("Record").FirstOrDefault();
        //        if (record != null)
        //        {
        //            var charCode = record.Element("CharCode")?.Value;
        //            var valueStr = record.Element("Value")?.Value;
        //            var dateStr = record.Attribute("Date")?.Value;

        //            if (!string.IsNullOrWhiteSpace(charCode) && !string.IsNullOrWhiteSpace(valueStr))
        //            {
        //                return new CurrencyExchangeResponse
        //                {
        //                    USDtoTJS = decimal.Parse(valueStr, CultureInfo.InvariantCulture),
        //                    DateTime = DateTime.ParseExact(dateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture)
        //                };
        //            }
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        // В проде логируй
        //        return null;
        //    }
        //}


    }
}
