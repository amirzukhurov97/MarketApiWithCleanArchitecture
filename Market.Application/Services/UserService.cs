using AutoMapper;
using Market.Application.DTOs.Address;
using Market.Application.DTOs.User;
using Market.Application.Interfacies;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.Services
{
    public class UserService(IUserRepository repository, IMapper mapper) : IGenericService<UserRequest, UserUpdateRequest, UserResponse>
    {
        public string Create(UserRequest item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return "The name cannot be empty";
            }
            else
            {
                var mapToEntity = mapper.Map<User>(item);
                repository.Add(mapToEntity);
                //var rabbit = new RabbitMqService();
                return $"Created new item with this ID: {mapToEntity.Id}";
            }
        }

        public IEnumerable<UserResponse> GetAll()
        {
            try
            {
                List<UserResponse>? responses = new List<UserResponse>();
                var adresses = repository.GetAll().ToList();
                if (adresses.Count > 0)
                {
                    foreach (var measurement in adresses)
                    {
                        var response = mapper.Map<UserResponse>(measurement);
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

        public UserResponse GetById(Guid id)
        {
            try
            {
                UserResponse responses = null;
                var response = repository.GetById(id).FirstOrDefault();
                if (response !=null)
                {
                    responses = mapper.Map<UserResponse>(response);
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
                return "User is not found";
            }
            repository.Remove(id);

            return "User is deleted";
        }

        public string Update(UserUpdateRequest item)
        {
            try
            {
                var _item = repository.GetById(item.Id).ToList();
                if (_item is null)
                {
                    return "User is not found";
                }
                var map = mapper.Map<User>(item);
                repository.Update(map);
                return "User is updated";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
