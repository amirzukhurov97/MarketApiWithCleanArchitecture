using Market.Domain.Abstract.Entity;

namespace Market.Application.DTOs.Measurement
{
    public record MeasurementResponse : EntityBaseResponse
    {
        public string Name { get; set; } = string.Empty;
    }
}
