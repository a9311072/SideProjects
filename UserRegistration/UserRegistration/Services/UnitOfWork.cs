using System;
using System.Threading.Tasks;
using UserRegistration.Models;

namespace UserRegistration.Services
{
    public interface IUnitOfWork
    {
        Task Commit();
        void Dispose();
    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected readonly WebApplicationContext _context;
        private bool _disposed = false;

        public UnitOfWork(WebApplicationContext context)
        {
            _context = context;
        }

        public virtual async Task Commit()
        {
            using (var _dbTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    _dbTransaction.Commit();
                    Dispose();
                }
                catch (Exception ex)
                {
                    _dbTransaction.Rollback();
                    Dispose();
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
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}