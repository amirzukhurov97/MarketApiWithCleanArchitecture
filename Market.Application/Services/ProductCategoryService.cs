using AutoMapper;
using Market.Application.DTOs.CurrencyExchange;
using Market.Application.DTOs.ProductCategory;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Application.Services
{
    public class ProductCategoryService(IProductCategoryRepository repository, IMapper mapper) : IGenericService<ProductCategoryRequest, ProductCategoryUpdateRequest, ProductCategoryResponse>
    {
        public string Create(ProductCategoryRequest item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return "The name cannot be empty";
            }
            else
            {
                var mapProductCategory = mapper.Map<ProductCategory>(item);
                repository.Add(mapProductCategory);
                return $"Created new item with this ID: {mapProductCategory.Name}";
            }
        }

        public IEnumerable<ProductCategoryResponse> GetAll()
        {
            try
            {
                List<ProductCategoryResponse>? responses = new List<ProductCategoryResponse>();
                var productCategories = repository.GetAll().ToList();
                if (productCategories.Count > 0)
                {
                    foreach (var product in productCategories)
                    {
                        var response = mapper.Map<ProductCategoryResponse>(product);
                        responses.Add(response);
                    }
                }
                else
                {
                    throw new Exception("No productCategories found.");
                }
                return responses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProductCategoryResponse> GetAll(int pageSize, int pageNumber)
        {
            try
            {
                var resultPage = repository.GetAll(pageSize, pageNumber).ToList();
                return mapper.Map<List<ProductCategoryResponse>>(resultPage);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductCategoryResponse GetById(Guid id)
        {
            try
            {
                ProductCategoryResponse? responses = null;
                var productResponse = repository.GetById(id).ToList();
                if (productResponse.Count > 0)
                {
                    responses = mapper.Map<ProductCategoryResponse>(productResponse[0]);
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
            var _item = repository.GetById(id);
            if (_item is null)
            {
                return "ProductCategory is not found";
            }
            repository.Remove(id);

            return "ProductCategory is deleted";
        }

        public string Update(ProductCategoryUpdateRequest item)
        {
            try
            {
                var _item = repository.GetById(item.Id).ToList();
                if (_item is null)
                {
                    return "ProductCategory is not found";
                }
                var mapProduct = mapper.Map<ProductCategory>(item);
                repository.Update(mapProduct);
                return "ProductCategory is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
