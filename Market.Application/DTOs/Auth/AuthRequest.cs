using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Auth
{
    public record AuthRequest : EntityBaseRequest
    {
        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool IsBlocked { get; set; }

        public Guid UserId { get; set; }
    }
}
