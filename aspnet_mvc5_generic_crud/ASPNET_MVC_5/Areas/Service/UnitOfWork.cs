using ASPNET_MVC_5.Models;
using System;
using System.Threading.Tasks;

namespace ASPNET_MVC_5.Areas.Service
{
    public interface IUnitOfWork
    {
        Task Commit();
        void Dispose();
    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public UnitOfWork(ApplicationDbContext context)
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
                catch (Exception)
                {
                    _dbTransaction.Rollback();
                    Dispose();
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