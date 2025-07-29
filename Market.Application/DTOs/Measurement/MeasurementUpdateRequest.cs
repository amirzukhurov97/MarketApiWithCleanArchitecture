using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Measurement
{
    public record MeasurementUpdateRequest : EntityBaseUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
