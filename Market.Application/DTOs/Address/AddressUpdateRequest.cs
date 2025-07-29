using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Address
{
    public record AddressUpdateRequest: EntityBaseUpdateRequest
    {
        public string Name { get; set; } = string.Empty;

    }
}
