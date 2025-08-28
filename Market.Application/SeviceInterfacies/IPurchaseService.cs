using Market.Application.DTOs.Purchase;
using Market.Application.DTOs.Report;
using Market.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.SeviceInterfacies
{
    public interface IPurchaseService<TRequest, TUpdateRequest, TResponse> : IGenericService<PurchaseRequest, PurchaseUpdateRequest, PurchaseResponse>
    {
        IEnumerable<TResponse> GetReport(ReportModel reportModel);
    }
}
