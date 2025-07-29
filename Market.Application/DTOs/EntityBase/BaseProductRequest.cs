namespace Market.Application.DTOs.EntityBase
{
    public record BaseProductRequest
    {
        public decimal Price { get; set; }
        public decimal PriceUSD { get; set; }
        public double Quantity { get; set; }
        public decimal SumPrice { get; set; }
        public decimal SumPriceUSD { get; set; }
        public DateTime Date { get; set; }
        public string? Comment { get; set; }
        public Guid ProductId { get; set; }
    }
}
