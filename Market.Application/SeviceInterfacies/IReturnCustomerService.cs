using Market.Application.DTOs.Customer;
using Market.Application.DTOs.Report;
using Market.Application.DTOs.ReturnCustomer;
using Market.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.SeviceInterfacies
{
    public interface IReturnCustomerService<TRequest, TUpdateRequest, TResponse> : IGenericService<ReturnCustomerRequest, ReturnCustomerUpdateRequest, ReturnCustomerResponse>
    {
        IEnumerable<TResponse> GetReport(ReportModel reportModel);
    }
}
