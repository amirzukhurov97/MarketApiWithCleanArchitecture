using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Address
{
    public record AddressResponse : EntityBaseResponse
    {
        public string Name { get; set; } = string.Empty;
    }
}
