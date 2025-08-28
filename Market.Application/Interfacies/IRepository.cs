

namespace MarketApi.Infrastructure.Interfacies
{
    public interface IRepository<T> where T : class
    {        
        public abstract IQueryable<T> GetAll();

        public abstract IQueryable<T> GetAll(int pageSize, int pageNumber);

        public abstract IQueryable<T> GetById(Guid id);
        public abstract T Add(T entity);
        public abstract bool Remove(Guid id);
        public abstract bool Update(T entity);
    }
}