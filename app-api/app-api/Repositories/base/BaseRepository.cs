using System.Linq.Expressions;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> Filter(T criteria);
        Guid Add(T item);
        void AddRange(IEnumerable<T> items);
        Guid Update(T item);
        void Delete(T entity);
        Task<T> GetByIdAsync(Guid id);
        void DeleteRange(IEnumerable<T> entity);
        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);

    };
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public ApiDataContext Context { get; }

        protected readonly DbSet<T> DbSet;

        public BaseRepository(ApiDataContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }
        public virtual Guid Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(string.Format("Called Create on entity {0}", typeof(T)));
            }
            Context.Add(item);
            Context.SaveChanges();
            return item.Id;
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(string.Format("Called Create on entity {0}", typeof(T)));
            }
            Context.AddRange(items);
            Context.SaveChanges();
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
            Context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            DbSet.RemoveRange(entity);
            Context.SaveChanges();
        }

        public virtual IEnumerable<T> Filter(T criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return (await DbSet.ToListAsync()).AsEnumerable();
        }

        public T GetById(Guid id)
        {
            var result = DbSet.FirstOrDefault(entry => entry.Id.Equals(id));

            if (result == null)
            {
                throw new Exception(string.Format("entity {0} wasn't found with id {1}", typeof(T), id));
            }

            return result;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var result = await DbSet.FirstOrDefaultAsync(entry => entry.Id.Equals(id));

            if (result == null)
            {
                throw new Exception(string.Format("entity {0} wasn't found with id {1}", typeof(T), id));
            }

            return result;
        }

        public Guid Update(T item)
        {
            var result = DbSet.FirstOrDefault(entry => entry.Id.Equals(item.Id));

            if (result == null)
            {
                throw new Exception(string.Format("entity {0} wasn't found with id {1}", typeof(T), item.Id));
            }

            DbSet.Update(item);

            return item.Id;
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = DbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

    }
}