using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistration.DAL.Interfaces
{
    public interface IRepository<TEntity> : IUnitOfWork where TEntity : class
    {

        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();
        IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        TEntity Remove(TEntity entity);
        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);
        TEntity RemoveEntity(TEntity entity);
        TEntity Update(TEntity entity);
    }

}
