using Market.Application.DTOs.Address;
using Market.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.SeviceInterfacies
{
    public interface IAddressService : IGenericService<AddressRequest, AddressUpdateRequest, AddressResponse>
    {
    }
}
