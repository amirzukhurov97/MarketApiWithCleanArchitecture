using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Measurement
{
    public record MeasurementRequest : EntityBaseRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
