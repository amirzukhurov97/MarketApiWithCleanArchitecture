using Market.Application.DTOs.Customer;
using Market.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.SeviceInterfacies
{
    public interface ICustomerService<TRequest, TUpdateRequest, TResponse> : IGenericService<CustomerRequest, CustomerUpdateRequest, CustomerResponse>
    {
        IEnumerable<TResponse> GetByAlphabet();
    }
}
