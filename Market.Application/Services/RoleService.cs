using AutoMapper;
using Market.Application.DTOs.Role;
using Market.Application.Interfacies;
using Market.Domain.Models;

namespace Market.Application.Services
{
    public class RoleService(IRoleRepository repository, IMapper mapper) : IGenericService<RoleRequest, RoleUpdateRequest, RoleResponse>
    {
        public string Create(RoleRequest item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return "The name cannot be empty";
            }
            else
            {
                var mapToEntity = mapper.Map<Role>(item);
                repository.Add(mapToEntity);
                return $"Created new item with this ID: {mapToEntity.Id}";
            }
        }

        public IEnumerable<RoleResponse> GetAll()
        {
            try
            {
                List<RoleResponse>? responses = new List<RoleResponse>();
                var roles = repository.GetAll().ToList();
                if (roles.Count > 0)
                {
                    foreach (var role in roles)
                    {
                        var response = mapper.Map<RoleResponse>(role);
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

        public RoleResponse GetById(Guid id)
        {
            try
            {
                RoleResponse responses = null;
                var response = repository.GetById(id).FirstOrDefault();
                if (response != null)
                {
                    responses = mapper.Map<RoleResponse>(response);
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
            var _item = repository.GetById(id).FirstOrDefault();
            if (_item is null)
            {
                return "Role is not found";
            }
            repository.Remove(id);

            return "Role is deleted";
        }

        public string Update(RoleUpdateRequest item)
        {
            try
            {
                var _item = repository.GetById(item.Id).FirstOrDefault();
                if (_item is null)
                {
                    return "Role is not found";
                }
                var map = mapper.Map<Role>(item);
                repository.Update(map);
                return "Role is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
