using AutoMapper;
using Market.Application.DTOs.Product;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Market.Application.Services
{
    public class ProductServise(IProductRepository repository, IMapper mapper) : IGenericService<ProductRequest, ProductUpdateRequest, ProductResponse>
    {        
        public string Create(ProductRequest item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return "The name cannot be empty";
            }
            else
            {
                var mapProduct = mapper.Map<Product>(item);
                repository.Add(mapProduct);
                return $"Created new item with this ID: {mapProduct.Id}";
            }
        }

        public IEnumerable<ProductResponse> GetAll()
        {
            try
            {
                List<ProductResponse>? responses = new List<ProductResponse>();
                var products = repository.GetAll().Include(pc => pc.ProductCategory).Include(pm => pm.Measurement).ToList();
                if (products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        var response = mapper.Map<ProductResponse>(product);
                        responses.Add(response);
                    }
                }
                return responses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductResponse GetById(Guid id)
        {
            try
            {
                ProductResponse responses = null;
                var productResponse = repository.GetById(id).Include(pc => pc.ProductCategory).Include(pm => pm.Measurement).FirstOrDefault();
                if (productResponse !=null)
                {
                    responses = mapper.Map<ProductResponse>(productResponse);
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
                return "Product is not found";
            }
            repository.Remove(id);

            return "Product is deleted";
        }

        public string Update(ProductUpdateRequest item)
        {
            try
            {
                var _item = repository.GetById(item.Id).FirstOrDefault();
                if (_item is null)
                {
                    return "Product is not found";
                }
                var mapProduct = mapper.Map<Product>(item);
                repository.Update(mapProduct);
                return "Product is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
