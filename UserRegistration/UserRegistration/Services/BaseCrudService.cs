using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

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

        // GET: TRepo/
        public virtual async Task<IHttpActionResult> GetAll()
        {
            var obj = await _repo.GetAll();
            return Ok(obj);
        }

        // GET: TRepo/Details/5
        public virtual async Task<IHttpActionResult> Get(int id)
        {
            var obj = await _repo.Get(id);
            if (obj == null)
                return NotFound();

            return Ok(obj);
        }

        // POST: TRepo/Create
        public virtual async Task<IHttpActionResult> Post(T entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repo.Add(entity);
            await _repo.Commit();

            return Created("DefaultApi", entity);
        }

        // PUT: TRepo/Edit/5
        public virtual async Task<IHttpActionResult> Put(int id, T entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            foreach (var property in entity.GetType().GetProperties())
                if (property.Name == "Id")
                    if (id != Convert.ToInt32(property.GetValue(entity)))
                        return BadRequest("Id is inconsistency");
            try
            {
                _repo.Update(entity);
                await _repo.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjExists(id)) return NotFound();
                else throw;
            }

            return Ok(entity);
        }

        // POST: TRepo/Delete/5
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

        private bool ObjExists(int id)
        {
            return _repo.Get(id) != null;
        }
    }
}