using AutoMapper;
using Market.Application.DTOs.OrganizationType;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;

namespace Market.Application.Services
{
    public class OrganizationTypeService(IOrganizationTypeRepository repository, IMapper mapper) : IGenericService<OrganizationTypeRequest, OrganizationTypeUpdateRequest, OrganizationTypeResponse>
    {
        public string Create(OrganizationTypeRequest item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return "The name cannot be empty";
            }
            else
            {
                var mappedOrganizationType = mapper.Map<OrganizationType>(item);
                repository.Add(mappedOrganizationType);
                return $"Created new item with this ID: {mappedOrganizationType.Name}";
            }
        }

        public IEnumerable<OrganizationTypeResponse> GetAll()
        {
            try
            {
                var responses = new List<OrganizationTypeResponse>();
                var organizationTypes = repository.GetAll().ToList();
                if (organizationTypes.Count > 0)
                {
                    foreach (var organizationType in organizationTypes)
                    {
                        var response = mapper.Map<OrganizationTypeResponse>(organizationType);
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

        public OrganizationTypeResponse GetById(Guid id)
        {
            try
            {
                OrganizationTypeResponse responses = null;
                var organizationTypes = repository.GetById(id).ToList();
                if (organizationTypes.Count > 0)
                {
                    responses = mapper.Map<OrganizationTypeResponse>(organizationTypes[0]);
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
                return "OrganizationType is not found";
            }
            repository.Remove(id);

            return "OrganizationType is deleted";
        }

        public string Update(OrganizationTypeUpdateRequest item)
        {
            try
            {
                var _item = repository.GetById(item.Id).ToList();
                if (_item is null)
                {
                    return "OrganizationType is not found";
                }
                var mapOrganizationType = mapper.Map<OrganizationType>(item);
                repository.Update(mapOrganizationType);
                return "OrganizationType is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
