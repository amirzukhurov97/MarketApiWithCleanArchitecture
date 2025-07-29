using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Customer
{
    public record CustomerRequest: EntityBaseRequest
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Guid AddressId { get; set; }
    }
}
