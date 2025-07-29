using AutoMapper;
using Market.Application.DTOs.Customer;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Application.Services
{
    public class CustomerService(ICustomerRepository repository, IMapper mapper) : IGenericService<CustomerRequest, CustomerUpdateRequest, CustomerResponse>
    {
        public string Create(CustomerRequest item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return "The name cannot be empty";
            }
            else
            {
                var mapToEntity = mapper.Map<Customer>(item);
                repository.Add(mapToEntity);
                return $"Created new item with this ID: {mapToEntity.Id}";
            }
        }

        public IEnumerable<CustomerResponse> GetAll()
        {
            try
            {
                List<CustomerResponse> responses = new List<CustomerResponse>();
                var customers = repository.GetAll().Include(p=>p.Address).ToList();
                if (customers.Count > 0)
                {
                    foreach (var customer in customers)
                    {
                        var response = mapper.Map<CustomerResponse>(customer);
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

        public CustomerResponse GetById(Guid id)
        {
            try
            {
                CustomerResponse responses = null;
                var response = repository.GetById(id).Include(c=>c.Address).ToList();
                if (response.Count > 0)
                {
                    responses = mapper.Map<CustomerResponse>(response[0]);
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
                return "Customer is not found";
            }
            repository.Remove(id);

            return "Customer is deleted";
        }

        public string Update(CustomerUpdateRequest item)
        {
            try
            {
                var _item = repository.GetById(item.Id).ToList();
                if (_item is null)
                {
                    return "Measurement is not found";
                }
                var mapMeasurement = mapper.Map<Customer>(item);
                repository.Update(mapMeasurement);
                return "Customer is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
