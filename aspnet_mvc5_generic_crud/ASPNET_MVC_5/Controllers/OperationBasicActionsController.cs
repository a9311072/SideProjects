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
    public class OperationBasicActionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OperationBasicActions
        public async Task<ActionResult> Index()
        {
            return View(await db.OperationBasicActions.ToListAsync());
        }

        // GET: OperationBasicActions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationBasicAction operationBasicAction = await db.OperationBasicActions.FindAsync(id);
            if (operationBasicAction == null)
            {
                return HttpNotFound();
            }
            return View(operationBasicAction);
        }

        // GET: OperationBasicActions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OperationBasicActions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] OperationBasicAction operationBasicAction)
        {
            if (ModelState.IsValid)
            {
                db.OperationBasicActions.Add(operationBasicAction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(operationBasicAction);
        }

        // GET: OperationBasicActions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationBasicAction operationBasicAction = await db.OperationBasicActions.FindAsync(id);
            if (operationBasicAction == null)
            {
                return HttpNotFound();
            }
            return View(operationBasicAction);
        }

        // POST: OperationBasicActions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] OperationBasicAction operationBasicAction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operationBasicAction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(operationBasicAction);
        }

        // GET: OperationBasicActions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationBasicAction operationBasicAction = await db.OperationBasicActions.FindAsync(id);
            if (operationBasicAction == null)
            {
                return HttpNotFound();
            }
            return View(operationBasicAction);
        }

        // POST: OperationBasicActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OperationBasicAction operationBasicAction = await db.OperationBasicActions.FindAsync(id);
            db.OperationBasicActions.Remove(operationBasicAction);
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
