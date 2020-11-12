using System.Threading.Tasks;
using System.Web.Http;
using UserRegistration.DAL.Interfaces;

namespace UserRegistration.WebApi.Interfaces
{
    public interface IBaseCrudService<T, TRepo>
        where T : class
        where TRepo : IRepository<T>, new()
    {
        Task<IHttpActionResult> GetAll();
        Task<IHttpActionResult> Get(int id);
        Task<IHttpActionResult> Post(T entity);
        Task<IHttpActionResult> Put(int id, T entity);
        Task<IHttpActionResult> Delete(int id);
    }

}
