using ASPNET_MVC_5.Areas.Admin.Models;
using ASPNET_MVC_5.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_MVC_5.Areas.Service
{
    public interface IRepository<TEntity>: IUnitOfWork where TEntity : class
    {
        /// <summary>  
        /// Gets the specified identifier.  
        /// </summary>  
        /// <param name="id">The identifier.</param>  
        /// <returns></returns>  
        Task<TEntity> Get(int id);

        /// <summary>  
        /// Gets all.  
        /// </summary>  
        /// <returns></returns>  
        Task<IEnumerable<TEntity>> GetAll();

        /// <summary>  
        /// Finds the specified predicate.  
        /// </summary>  
        /// <param name="predicate">The predicate.</param>  
        /// <returns></returns>  
        IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);

        /// <summary>  
        /// Singles the or default.  
        /// </summary>  
        /// <param name="predicate">The predicate.</param>  
        /// <returns></returns>  
        TEntity SingleOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);

        /// <summary>  
        /// First the or default.  
        /// </summary>  
        /// <returns></returns>  
        TEntity FirstOrDefault();

        /// <summary>  
        /// Adds the specified entity.  
        /// </summary>  
        /// <param name="entity">The entity.</param>  
        TEntity Add(TEntity entity);

        /// <summary>  
        /// Adds the range.  
        /// </summary>  
        /// <param name="entities">The entities.</param>  
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        /// <summary>  
        /// Removes the specified entity.  
        /// </summary>  
        /// <param name="entity">The entity.</param>  
        TEntity Remove(TEntity entity);

        /// <summary>  
        /// Removes the range.  
        /// </summary>  
        /// <param name="entities">The entities.</param>  
        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>  
        /// Removes the Entity  
        /// </summary>  
        /// <param name="entity"></param>  
        TEntity RemoveEntity(TEntity entity);

        /// <summary>  
        /// Update the Entity  
        /// </summary>  
        /// <param name="entity"></param>  
        TEntity Update(TEntity entity);
    }

    public abstract class BaseRepository<TEntity> : IUnitOfWork, IRepository<TEntity>, IDisposable where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected IUnitOfWork _db;
        private bool _disposed = false;

        /// <summary>  
        /// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.  
        /// Note that here I've stored Context.Set<TEntity>() in the constructor and store it in a private field like _entities.   
        /// This way, the implementation  of our methods would be cleaner:        ///   
        /// _entities.ToList();  
        /// _entities.Where();  
        /// _entities.SingleOrDefault();  
        /// </summary>  
        public BaseRepository()
        {
            _context = new ApplicationDbContext();
            _db = new UnitOfWork(_context);
        }

        /// <summary>  
        /// Gets the specified identifier.  
        /// </summary>  
        /// <param name="id">The identifier.</param>  
        /// <returns></returns>  
        public virtual async Task<TEntity> Get(int id)
        { 
            return await _context.Set<TEntity>().FindAsync(id);
        }

        /// <summary>  
        /// Gets all.  
        /// </summary>  
        /// <returns></returns>  
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        /// <summary>  
        /// Finds the specified predicate.  
        /// </summary>  
        /// <param name="predicate">The predicate.</param>  
        /// <returns></returns>  
        public virtual IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        /// <summary>  
        /// Singles the or default.  
        /// </summary>  
        /// <param name="predicate">The predicate.</param>  
        /// <returns></returns>  
        public virtual TEntity SingleOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).SingleOrDefault();
        }

        /// <summary>  
        /// First the or default.  
        /// </summary>  
        /// <returns></returns>  
        public virtual TEntity FirstOrDefault()
        {
            return _context.Set<TEntity>().SingleOrDefault();
        }

        /// <summary>  
        /// Adds the specified entity.  
        /// </summary>  
        /// <param name="entity">The entity.</param>  
        public virtual TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        /// <summary>  
        /// Adds the range.  
        /// </summary>  
        /// <param name="entities">The entities.</param>  
        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            return entities;
        }

        /// <summary>  
        /// Removes the specified entity.  
        /// </summary>  
        /// <param name="entity">The entity.</param>  
        public virtual TEntity Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return entity;
        }

        /// <summary>  
        /// Removes the range.  
        /// </summary>  
        /// <param name="entities">The entities.</param>  
        public virtual IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            return entities;
        }

        /// <summary>  
        /// Removes the Entity  
        /// </summary>  
        /// <param name="entity"></param>  
        public virtual TEntity RemoveEntity(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(entity);
            }
            _context.Set<TEntity>().Remove(entity);
            return entity;
        }

        /// <summary>  
        /// Update the Entity  
        /// </summary>  
        /// <param name="entity"></param>  
        public virtual TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        /// <summary>  
        /// Commit the DB changes  
        /// </summary>  
        public virtual async Task Commit()
        {
            await _db.Commit();
            Dispose();
        }

        /// <summary>  
        /// Dispose the entity  
        /// </summary>  
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _db.Dispose();
                }
            }
            _disposed = true;
        }
    }

}
