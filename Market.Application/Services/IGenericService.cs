using Market.Domain.Abstract.Entity;

namespace Market.Application.Services
{
    public interface IGenericService<TRequest, TUpdateRequest,TResponse> where TRequest: EntityBaseRequest where TUpdateRequest:EntityBaseUpdateRequest where TResponse : EntityBaseResponse
    {
        public string Create(TRequest item);
        IEnumerable<TResponse> GetAll();
        IEnumerable<TResponse> GetAll(int pageSize, int pageNumber);
        TResponse GetById(Guid id);
        string Update(TUpdateRequest item);
        string Remove(Guid id);
    }
}
