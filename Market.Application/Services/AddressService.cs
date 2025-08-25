using AutoMapper;
using Market.Application.DTOs.Address;
using Market.Application.SeviceInterfacies;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Application.Services
{
    public class AddressService(IAddressRepository repository, IMapper mapper) : IAddressService
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
                return $"Created new item with this ID: {mapToEntity.Name}";
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
        public IEnumerable<AddressResponse> GetAll(int pageSize, int pageNumber)
        {
            try
            {
                var adresses = repository.GetAll(pageSize, pageNumber).ToList();
                return mapper.Map<List<AddressResponse>>(adresses);
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
            try
            {
                var _item = repository.GetById(id).FirstOrDefault();
                if (_item is not null)
                {
                    var isDelete = repository.Remove(id);
                    return $"Address {_item.Name} is deleted";
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public string Update(AddressUpdateRequest item)
        {
            try
            {
                var _item = repository.GetById(item.Id).ToList();
                if (_item.Count() == 0)
                {
                    return "";
                }
                var mapAddress = mapper.Map<Address>(item);
                var res = repository.Update(mapAddress);
                return $"Address is updated {mapAddress.Name}";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
