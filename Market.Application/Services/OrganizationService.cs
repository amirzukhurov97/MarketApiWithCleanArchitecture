using AutoMapper;
using Market.Application.DTOs.Organization;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Application.Services
{
    public class OrganizationService(IOrganizationRepository repository, IMapper mapper) : IGenericService<OrganizationRequest, OrganizationUpdateRequest, OrganizationResponse>
    {
        public string Create(OrganizationRequest item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return "The name cannot be empty";
            }
            else
            {
                var mapOrganization = mapper.Map<Organization>(item);
                repository.Add(mapOrganization);
                return $"Created new item with this ID: {mapOrganization.Name}";
            }
        }

        public IEnumerable<OrganizationResponse> GetAll()
        {
            try
            {
                var responses = new List<OrganizationResponse>();
                var products = repository.GetAll().Include(pc => pc.Address).Include(pm => pm.OrganizationType).ToList();
                if (products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        var response = mapper.Map<OrganizationResponse>(product);
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

        public OrganizationResponse GetById(Guid id)
        {
            try
            {
                OrganizationResponse responses = null;
                var organizationResponse = repository.GetById(id).Include(pc => pc.Address).Include(pm => pm.OrganizationType).ToList();
                if (organizationResponse.Count > 0)
                {
                    responses = mapper.Map<OrganizationResponse>(organizationResponse[0]);
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
                return "Organization is not found";
            }
            repository.Remove(id);
            return "Organization is deleted";
        }

        public string Update(OrganizationUpdateRequest item)
        {
            try
            {
                var _item = repository.GetById(item.Id).ToList();
                if (_item is null)
                {
                    return "Organization is not found";
                }
                var mapOrganization = mapper.Map<Organization>(item);
                repository.Update(mapOrganization);
                return "Organization is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
