using System;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http;

namespace UserRegistration.Services
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

    public abstract class BaseCrudService<T, TRepo> : ApiController, IBaseCrudService<T, TRepo>
        where T : class
        where TRepo : IRepository<T>, new()
    {
        protected IRepository<T> _repo;
        public BaseCrudService()
        {
            _repo = new TRepo();
        }

        // GET: /api/T/
        [HttpGet]
        public virtual async Task<IHttpActionResult> GetAll()
        {
            var obj = await _repo.GetAll();
            return Ok(obj);
        }

        // GET: /api/T/id
        [HttpGet("{id}")]
        public virtual async Task<IHttpActionResult> Get(int id)
        {
            var obj = await _repo.Get(id);
            if (obj == null)
                return NotFound();

            return Ok(obj);
        }

        // POST: /api/T/
        [HttpPost]
        public virtual async Task<IHttpActionResult> Post(T entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repo.Add(entity);
            await _repo.Commit();

            return Created("DefaultApi", entity);
        }

        // PUT: /api/T/id
        [HttpPut("{id}")]
        public virtual async Task<IHttpActionResult> Put(int id, T entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            foreach (var property in entity.GetType().GetProperties())
                if (property.Name == "Id")
                    if (id != Convert.ToInt32(property.GetValue(entity)))
                        return BadRequest("Id is inconsistency");

            _repo.Update(entity);
            await _repo.Commit();

            return Ok(entity);
        }

        // Delete: /api/T/id
        [HttpDelete]
        public virtual async Task<IHttpActionResult> Delete(int id)
        {
            var obj = await _repo.Get(id);
            if (obj == null)
                return NotFound();

            _repo.Remove(obj);
            await _repo.Commit();
            return Ok(obj);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _repo.Dispose();

            base.Dispose(disposing);
        }

    }
}
