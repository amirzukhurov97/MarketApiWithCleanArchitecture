using Market.Application.DTOs.Report;
using Market.Application.DTOs.ReturnOrganization;
using Market.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.SeviceInterfacies
{
    public interface IReturnOrganizationService<TRequest, TUpdateRequest, TResponse>: IGenericService<ReturnOrganizationRequest, ReturnOrganizationUpdateRequest, ReturnOrganizationResponse>
    {
        IEnumerable<TResponse> GetReport(ReportModel reportModel);
    }
}
