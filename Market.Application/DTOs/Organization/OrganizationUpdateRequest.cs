using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Organization
{
    public record OrganizationUpdateRequest : EntityBaseUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public Guid OrganizationTypeId { get; set; }
        public Guid AddressId { get; set; }
    }
}
