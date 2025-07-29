using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.ReturnCustomer
{
    public record ReturnCustomerResponse : EntityBaseResponse
    {
        public decimal Price { get; set; }
        public decimal PriceUSD { get; set; }
        public double Quantity { get; set; }
        public decimal SumPrice { get; set; }
        public decimal SumPriceUSD { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
    }
}
