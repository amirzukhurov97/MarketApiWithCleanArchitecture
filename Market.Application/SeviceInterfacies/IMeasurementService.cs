using Market.Application.DTOs.Measurement;
using Market.Application.Services;

namespace Market.Application.SeviceInterfacies
{
    public interface IMeasurementService<TRequest, TUpdateRequest, TResponse> : IGenericService<MeasurementRequest, MeasurementUpdateRequest, MeasurementResponse>
    {
        IEnumerable<TResponse> GetByAlphabet();
    }
}
