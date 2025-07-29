using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Role
{
    public record RoleRequest : EntityBaseRequest
    {
        public string Name { get; set; } = string.Empty;
    }    
}
