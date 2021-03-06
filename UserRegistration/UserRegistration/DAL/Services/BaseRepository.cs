﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UserRegistration.Infrastructures;
using UserRegistration.DAL.Interfaces;

namespace UserRegistration.Services
{
    public abstract class BaseRepository<TEntity> : IUnitOfWork, IRepository<TEntity>, IDisposable where TEntity : class
    {
        protected readonly MsSqlContext _context;
        protected IUnitOfWork _db;
        private bool _disposed = false;


        public BaseRepository()
        {
            _context = new MsSqlContext();
            _db = new UnitOfWork(_context);
        }

        public virtual async Task<TEntity> Get(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public virtual TEntity SingleOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).SingleOrDefault();
        }

        public virtual TEntity FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        public virtual TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            return entities;
        }

        public virtual TEntity Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return entity;
        }

        public virtual IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            return entities;
        }

        public virtual TEntity RemoveEntity(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(entity);
            }
            _context.Set<TEntity>().Remove(entity);
            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        /// <summary>  
        /// Commit the DB changes  
        /// </summary>  
        public virtual async Task CommitAsync()
        {
            await _db.CommitAsync();
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