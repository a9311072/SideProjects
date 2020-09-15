using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPNET_MVC_5.Models;

namespace ASPNET_MVC_5.Controllers
{
    [Authorize]
    public class EQPsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EQPs
        public async Task<ActionResult> Index()
        {
            return View(await db.EQPs.ToListAsync());
        }

        // GET: EQPs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQP eQP = await db.EQPs.FindAsync(id);
            if (eQP == null)
            {
                return HttpNotFound();
            }
            return View(eQP);
        }

        // GET: EQPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EQPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] EQP eQP)
        {
            if (ModelState.IsValid)
            {
                db.EQPs.Add(eQP);
                var obj = db.EQPServiceLists.Add(new EQPServiceList { EQP_Id = eQP.Id, EQPService_Id = 5 });

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(eQP);
        }

        // GET: EQPs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQP eQP = await db.EQPs.FindAsync(id);
            if (eQP == null)
            {
                return HttpNotFound();
            }
            return View(eQP);
        }

        // POST: EQPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] EQP eQP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eQP).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eQP);
        }

        // GET: EQPs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQP eQP = await db.EQPs.FindAsync(id);
            if (eQP == null)
            {
                return HttpNotFound();
            }
            return View(eQP);
        }

        // POST: EQPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EQP eQP = await db.EQPs.FindAsync(id);
            db.EQPs.Remove(eQP);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
