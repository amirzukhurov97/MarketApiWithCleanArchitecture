using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.CurrencyExchange
{
    public record CurrencyExchangeResponse : EntityBaseResponse
    {
        public DateTime DateTime { get; set; }

        public decimal USDtoTJS { get; set; }
    }
}
