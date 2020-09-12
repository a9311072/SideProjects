using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPNET_MVC_5.Models;
using ASPNET_MVC_5.ViewModels;

namespace ASPNET_MVC_5.Controllers
{
    public class EQPServicesController_Old : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EQPServices
        //public ActionResult Index(int? id)
        //{
        //    //var viewModel = new EQPServiceDatas();
        //    //viewModel.EQPServices = db.EQPServices;

        //    //if (id != null)
        //    //{
        //    //    ViewBag.EQPServiceId = id.Value;
        //    //    viewModel.EQPs = viewModel.EQPServices.Where(
        //    //        i => i.Id == id.Value).Single().EQPs;
        //    //}
        //    //return View(viewModel);
        //}
        //public ActionResult Index()
        //{
        //    var result = from c in db.EQPServices
        //                 from p in db.EQPs
        //                 where c.Id == p.Id
        //                 orderby c.Name
        //                 select new { c.Id, c.Name, EQPName = p.Name };


        //    return View(result.ToList());      
        //}


        // GET: EQPServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQPService eQPService = db.EQPServices.Find(id);
            if (eQPService == null)
            {
                return HttpNotFound();
            }
            return View(eQPService);
        }

        // GET: EQPServices/Create
        public ActionResult Create()
        {
            //var viewModel = new EQPServiceDatas();
            //viewModel.EQPs = db.EQPs;


            List<SelectListItem> dropDownlist = new List<SelectListItem>();
            foreach(var eqp in db.EQPs)
            {
                dropDownlist.Add( new SelectListItem()
                {
                    Text = eqp.Name,
                    Value = eqp.Id.ToString()
                });
            }
            ViewBag.EQPList = dropDownlist;

            return View();
        }

        // POST: EQPServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EQPService eQPService)
        {
            if (ModelState.IsValid)
            {
                
                //db.EQPServices.Add(eQPService);
                
                //result.Id = eQPService.Id;
                //result.Name = eQPService.Name;
                //result.eqpId = ViewBag.EQPList.Value;
                //result.eqpName = ViewBag.EQPList.Text;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eQPService);
        }


        // GET: EQPServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQPService eQPService = db.EQPServices.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,Name")] EQPService eQPService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eQPService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eQPService);
        }

        // GET: EQPServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQPService eQPService = db.EQPServices.Find(id);
            if (eQPService == null)
            {
                return HttpNotFound();
            }
            return View(eQPService);
        }

        // POST: EQPServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EQPService eQPService = db.EQPServices.Find(id);
            db.EQPServices.Remove(eQPService);
            db.SaveChanges();
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
