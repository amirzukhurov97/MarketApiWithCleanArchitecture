using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Address
{
    public record AddressRequest : EntityBaseRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
