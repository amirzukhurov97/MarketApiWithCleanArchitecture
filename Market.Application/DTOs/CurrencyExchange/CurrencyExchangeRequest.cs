using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.CurrencyExchange
{
    public record CurrencyExchangeRequest : EntityBaseRequest
    {
        public DateTime DateTime { get; set; }
        public decimal USDtoTJS { get; set; }
    }
}
