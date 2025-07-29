using AutoMapper;
using Market.Application.DTOs.Address;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Application.Services
{
    public class AddressService(IAddressRepository repository, IMapper mapper) : IGenericService<AddressRequest, AddressUpdateRequest, AddressResponse>
    {
        public string Create(AddressRequest item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return "The name cannot be empty";
            }
            else
            {
                var mapToEntity = mapper.Map<Address>(item);
                repository.Add(mapToEntity);
                return $"Created new item with this ID: {mapToEntity.Id}";
            }
        }

        public IEnumerable<AddressResponse> GetAll()
        {
            try
            {
                List<AddressResponse>? responses = new List<AddressResponse>();
                var adresses = repository.GetAll().ToList();
                if (adresses.Count > 0)
                {
                    foreach (var measurement in adresses)
                    {
                        var response = mapper.Map<AddressResponse>(measurement);
                        responses.Add(response);
                    }
                }
                return responses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AddressResponse GetById(Guid id)
        {
            try
            {
                AddressResponse responses = null;
                var response = repository.GetById(id).ToList();
                if (response.Count > 0)
                {
                    responses = mapper.Map<AddressResponse>(response[0]);
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
                return "Address is not found";
            }
            repository.Remove(id);

            return "Address is deleted";
        }

        public string Update(AddressUpdateRequest item)
        {
            try
            {
                var _item = repository.GetById(item.Id).ToList();
                if (_item is null)
                {
                    return "Address is not found";
                }
                var mapAddress = mapper.Map<Address>(item);
                repository.Update(mapAddress);
                return "Address is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
