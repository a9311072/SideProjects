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
    public class EQPServicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EQPServices
        public async Task<ActionResult> Index()
        {
            return View(await db.EQPServices.ToListAsync());
        }

        // GET: EQPServices/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQPService eQPService = await db.EQPServices.FindAsync(id);
            if (eQPService == null)
            {
                return HttpNotFound();
            }
            return View(eQPService);
        }

        // GET: EQPServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EQPServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] EQPService eQPService)
        {
            if (ModelState.IsValid)
            {
                db.EQPServices.Add(eQPService);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(eQPService);
        }

        // GET: EQPServices/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQPService eQPService = await db.EQPServices.FindAsync(id);
            if (eQPService == null)
            {
                return HttpNotFound();
            }
            return View(eQPService);
        }

        // POST: EQPServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] EQPService eQPService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eQPService).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eQPService);
        }

        // GET: EQPServices/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQPService eQPService = await db.EQPServices.FindAsync(id);
            if (eQPService == null)
            {
                return HttpNotFound();
            }
            return View(eQPService);
        }

        // POST: EQPServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EQPService eQPService = await db.EQPServices.FindAsync(id);
            db.EQPServices.Remove(eQPService);
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
