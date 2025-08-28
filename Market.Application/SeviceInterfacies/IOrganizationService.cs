using Market.Application.DTOs.Organization;
using Market.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.SeviceInterfacies
{
    public interface IOrganizationService<TRequest, TUpdateRequest, TResponse> : IGenericService<OrganizationRequest, OrganizationUpdateRequest, OrganizationResponse>
    {
        IEnumerable<TResponse> GetByAlphabet();
    }
}
