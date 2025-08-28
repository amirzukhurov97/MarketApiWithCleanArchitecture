using Market.Application.DTOs.Address;
using Market.Application.Services;

namespace Market.Application.SeviceInterfacies
{
    public interface IAddressService<TRequest, TUpdateRequest, TResponse> : IGenericService<AddressRequest, AddressUpdateRequest, AddressResponse>
    {
        IEnumerable<TResponse> GetByAlphabet();
    }
}
