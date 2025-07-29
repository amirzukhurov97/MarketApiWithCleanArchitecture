using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.OrganizationType
{
    public record OrganizationTypeUpdateRequest : EntityBaseUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
