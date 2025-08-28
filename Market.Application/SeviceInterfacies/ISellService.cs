using Market.Application.DTOs.Report;
using Market.Application.DTOs.Sell;
using Market.Application.Services;

namespace Market.Application.SeviceInterfacies
{
    public interface ISellService<TRequest, TUpdateRequest, TResponse> : IGenericService<SellRequest, SellUpdateRequest, SellResponse>
    {
        IEnumerable<TResponse> GetReport(ReportModel reportModel);
    }
}
