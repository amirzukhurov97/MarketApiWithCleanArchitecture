using AutoMapper;
using Market.Application.DTOs.Measurement;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Application.Services
{
    public class MeasurementService(IMeasurementRepository repository, IMapper mapper) : IGenericService<MeasurementRequest, MeasurementUpdateRequest, MeasurementResponse>
    {
        public string Create(MeasurementRequest item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return "The name cannot be empty";
            }
            else
            {
                var mapToEntity = mapper.Map<Measurement>(item);
                repository.Add(mapToEntity);
                return $"Created new item with this ID: {mapToEntity.Id}";
            }
        }

        public IEnumerable<MeasurementResponse> GetAll()
        {
            try
            {
                List<MeasurementResponse>? responses = new List<MeasurementResponse>();
                var measurements = repository.GetAll().ToList();
                if (measurements.Count > 0)
                {
                    foreach (var measurement in measurements)
                    {
                        var response = mapper.Map<MeasurementResponse>(measurement);
                        responses.Add(response);
                    }
                }
                else
                {
                    throw new Exception("No measurements found.");
                }
                return responses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MeasurementResponse GetById(Guid id)
        {
            try
            {
                MeasurementResponse responses = null;
                var productResponse = repository.GetById(id).ToList();
                if (productResponse.Count > 0)
                {
                    responses = mapper.Map<MeasurementResponse>(productResponse[0]);
                }
                return responses;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Remove(Guid id)
        {
            var _item = repository.GetById(id);
            if (_item is null)
            {
                return "Measurement is not found";
            }
            repository.Remove(id);

            return "Measurement is deleted";
        }

        public string Update(MeasurementUpdateRequest item)
        {
            try
            {
                var _item = repository.GetById(item.Id).ToList();
                if (_item is null)
                {
                    return "Measurement is not found";
                }
                var mapMeasurement = mapper.Map<Measurement>(item);
                repository.Update(mapMeasurement);
                return "Measurement is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
