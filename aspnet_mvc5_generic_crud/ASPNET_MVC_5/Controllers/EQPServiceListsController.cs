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
using ASPNET_MVC_5.ViewModels;

namespace ASPNET_MVC_5.Controllers
{
    public class EQPServiceListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EQPServiceLists
        public async Task<ActionResult> Index()
        {
            var result = (from o in db.EQPServiceLists
                          from p in db.EQPs
                          from q in db.EQPServices
                          where o.EQP_Id == p.Id && o.EQPService_Id == q.Id
                          orderby q.Name
                          select new EQPServiceRelations
                          { EQP_Id = o.EQP_Id,
                            EQP_Name = p.Name,
                            Service_Id =o.EQPService_Id,
                            Service_Name=q.Name
                          }).ToList();

            return View(result);
        }

        // GET: EQPServiceLists/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQPServiceList eQPServiceList = await db.EQPServiceLists.FindAsync(id);
            if (eQPServiceList == null)
            {
                return HttpNotFound();
            }
            return View(eQPServiceList);
        }

        // GET: EQPServiceLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EQPServiceLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EQP_Id,EQPService_Id")] EQPServiceList eQPServiceList)
        {
            if (ModelState.IsValid)
            {
                db.EQPServiceLists.Add(eQPServiceList);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(eQPServiceList);
        }

        // GET: EQPServiceLists/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQPServiceList eQPServiceList = await db.EQPServiceLists.FindAsync(id);
            if (eQPServiceList == null)
            {
                return HttpNotFound();
            }
            return View(eQPServiceList);
        }

        // POST: EQPServiceLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EQP_Id,EQPService_Id")] EQPServiceList eQPServiceList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eQPServiceList).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eQPServiceList);
        }

        // GET: EQPServiceLists/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQPServiceList eQPServiceList = await db.EQPServiceLists.FindAsync(id);
            if (eQPServiceList == null)
            {
                return HttpNotFound();
            }
            return View(eQPServiceList);
        }

        // POST: EQPServiceLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EQPServiceList eQPServiceList = await db.EQPServiceLists.FindAsync(id);
            db.EQPServiceLists.Remove(eQPServiceList);
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
