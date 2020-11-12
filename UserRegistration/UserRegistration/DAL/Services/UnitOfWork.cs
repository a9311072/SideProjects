using System;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using UserRegistration.Infrastructures;
using UserRegistration.DAL.Interfaces;

namespace UserRegistration.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected readonly MsSqlContext _dbContext;
        private bool _disposed = false;

        public UnitOfWork(MsSqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task CommitAsync()
        {
            using (var _dbTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    await _dbContext.SaveChangesAsync();
                    _dbTransaction.Commit();
                }
                catch (DbUpdateException dbEx)
                {
                    _dbTransaction.Rollback();
                    throw dbEx;
                }
                catch (Exception ex)
                {
                    _dbTransaction.Rollback();
                    throw ex;
                }
            }

        }

        public void Dispose()
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
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}