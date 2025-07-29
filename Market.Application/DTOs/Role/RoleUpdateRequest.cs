using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Role
{
    public record RoleUpdateRequest : EntityBaseUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
