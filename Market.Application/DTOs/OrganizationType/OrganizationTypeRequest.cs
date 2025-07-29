using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.OrganizationType
{
    public record OrganizationTypeRequest : EntityBaseRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
