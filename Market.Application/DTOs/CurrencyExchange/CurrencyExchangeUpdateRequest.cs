using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.CurrencyExchange
{
    public record CurrencyExchangeUpdateRequest : EntityBaseUpdateRequest
    {
        public DateTime DateTime { get; set; }
        public decimal USDtoTJS { get; set; }
    }
}
