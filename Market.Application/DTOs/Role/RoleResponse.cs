using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Role
{
    public record RoleResponse : EntityBaseResponse
    {
        public string Name { get; set; } = string.Empty;
    }
}
