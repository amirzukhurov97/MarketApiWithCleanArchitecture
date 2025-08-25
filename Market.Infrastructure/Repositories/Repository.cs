using Market.Domain.Abstract.Entity;
using Market.Infrastructure.DataBase;
using MarketApi.Infrastructure.Interfacies;
using Microsoft.EntityFrameworkCore;

namespace Market.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T Add(T item)
        {
            try
            {
                _dbSet.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual IQueryable<T> GetAll()
        {
            try
            {
                return _dbSet.AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<T> GetAll(int pageSize, int pageNumber)
        {
            var totalCount = _context.Set<T>().Count();
            var pagesCount = (int)Math.Ceiling((double)totalCount / pageSize);
            var skipCount = pageSize * (pageNumber - 1);
            return _context.Set<T>()
                .Skip(skipCount)
                .Take(pageSize);
        }

        public IQueryable<T> GetById(Guid id)
        {
            try
            {
                return _dbSet.Where(i=>i.Id==id).AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Remove(Guid id)
        {
            try
            {
                var item = GetById(id).ToList();
                _dbSet.Remove(item[0]);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(T item)
        {
            try
            {
                var itemResult = _dbSet.Local.FirstOrDefault(i=>i.Id==item.Id);    
                _context.Entry(itemResult).State = EntityState.Detached;
                _context.Update(item);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
