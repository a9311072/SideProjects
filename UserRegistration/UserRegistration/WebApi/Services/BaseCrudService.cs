using System;
using System.Threading.Tasks;
using System.Web.Http;
using UserRegistration.DAL.Interfaces;
using UserRegistration.WebApi.Interfaces;

namespace UserRegistration.Services
{

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
        [HttpGet]
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
            await _repo.CommitAsync();

            return Created("DefaultApi", entity);
        }

        // PUT: /api/T/id
        [HttpPut]
        public virtual async Task<IHttpActionResult> Put(int id, T entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            foreach (var property in entity.GetType().GetProperties())
                if (property.Name == "Id")
                    if (id != Convert.ToInt32(property.GetValue(entity)))
                        return BadRequest("Id is inconsistency");

            _repo.Update(entity);
            await _repo.CommitAsync();

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
            await _repo.CommitAsync();
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
