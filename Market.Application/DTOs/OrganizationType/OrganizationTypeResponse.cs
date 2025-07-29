using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.OrganizationType
{
    public record OrganizationTypeResponse : EntityBaseResponse
    {
        public string Name { get; set; } = string.Empty;
        
    }
}
