

namespace MarketApi.Infrastructure.Interfacies
{
    public interface IRepository<T> where T : class
    {        
        IQueryable<T> GetAll();

        IQueryable<T> GetAll(int pageSize, int pageNumber);

        IQueryable<T> GetById(Guid id);
        T Add(T entity);
        bool Remove(Guid id);
        bool Update(T entity);
    }
}