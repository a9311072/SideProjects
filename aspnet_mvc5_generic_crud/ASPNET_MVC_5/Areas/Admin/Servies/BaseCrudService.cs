using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASPNET_MVC_5.Areas.Admin.Services
{
    public interface IBaseCrudService<T, TRepo>
        where T : class
        where TRepo : IRepository<T>, new()
    {
        Task<ActionResult> Index();
        Task<ActionResult> Details(int id);
        ActionResult Create();
        Task<ActionResult> Create(T entity);
        Task<ActionResult> Edit(int id);
        Task<ActionResult> Edit(T entity);
        Task<ActionResult> Delete(int id);
        Task<ActionResult> DeleteConfirmed(int id);
    }

    public abstract class BaseCrudService<T, TRepo>: Controller, IBaseCrudService<T, TRepo>
        where T : class
        where TRepo : IRepository<T>, new()
    {
        protected IRepository<T> _repo;
        public BaseCrudService()
        {
            _repo = new TRepo();
        }

        // GET: TRepo/
        public virtual async Task<ActionResult> Index()
        {
            var model = await _repo.GetAll();
            return View(model);
        }

        // GET: TRepo/Details/5
        public virtual async Task<ActionResult> Details(int id)
        {
            var obj = await _repo.Get(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // GET: TRepo/
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: TRepo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(T entity)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(entity);
                await _repo.Commit();
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        // GET: TRepo/Edit/5
        public virtual async Task<ActionResult> Edit(int id)
        {
            var entity = await _repo.Get(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        // POST: TRepo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(T entity)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(entity);
                await _repo.Commit();
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        // GET: TRepo/Delete/5
        public virtual async Task<ActionResult> Delete(int id)
        {
            var entity = await _repo.Get(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        // POST: TRepo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> DeleteConfirmed(int id)
        {
            var entity = await _repo.Get(id);
            _repo.Remove(entity);
            await _repo.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}
