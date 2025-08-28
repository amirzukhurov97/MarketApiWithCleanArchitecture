using AutoMapper;
using Market.Application.DTOs.Address;
using Market.Application.DTOs.CurrencyExchange;
using Market.Application.DTOs.Customer;
using Market.Application.SeviceInterfacies;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Market.Application.Services
{
    public class CustomerService(ICustomerRepository repository, IMapper mapper) : ICustomerService<CustomerRequest, CustomerUpdateRequest, CustomerResponse>
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
                return $"Created new item with this ID: {mapToEntity.Name}";
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

        public IEnumerable<CustomerResponse> GetAll(int pageSize, int pageNumber)
        {
            try
            {
                var resultPage = repository.GetAll(pageSize, pageNumber).ToList();
                return mapper.Map<List<CustomerResponse>>(resultPage);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CustomerResponse> GetByAlphabet()
        {
            try
            {
                var customers = repository.GetAll().OrderBy(a => a.Name).ToList();
                return mapper.Map<List<CustomerResponse>>(customers);
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
                CustomerResponse? responses = null;
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
            if (_item.Count() == 0)
            {
                return "";
            }
            repository.Remove(id);

            return "Customer is deleted";
        }

        public string Update(CustomerUpdateRequest item)
        {
            try
            {
                var _item = repository.GetById(item.Id).ToList();
                if (_item.Count() == 0)
                {
                    return "";
                }
                var mapCustomer = mapper.Map<Customer>(item);
                repository.Update(mapCustomer);
                return "Customer is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
