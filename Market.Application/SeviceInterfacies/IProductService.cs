using Market.Application.DTOs.Product;
using Market.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.SeviceInterfacies
{
    public interface IProductService<TRequest, TUpdateRequest, TResponse> : IGenericService<ProductRequest, ProductUpdateRequest, ProductResponse>
    {
        IEnumerable<TResponse> GetByAlphabet();
    }
}
