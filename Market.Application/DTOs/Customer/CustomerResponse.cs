using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Customer
{
    public record CustomerResponse :EntityBaseResponse
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string AddressName { get; set; } = string.Empty;
    }
}
