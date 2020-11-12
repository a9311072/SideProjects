using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistration.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Dispose();
    }

}
