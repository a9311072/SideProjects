﻿using ASPNET_MVC_5.Areas.Product.Models;
using ASPNET_MVC_5.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASPNET_MVC_5.Areas.Product
{
    [Authorize]
    public class ProductCatagoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product/ProductCatagories
        public async Task<ActionResult> Index()
        {
            return View(await db.ProductCatagories.ToListAsync());
        }

        // GET: Product/ProductCatagories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCatagory productCatagory = await db.ProductCatagories.FindAsync(id);
            if (productCatagory == null)
            {
                return HttpNotFound();
            }
            return View(productCatagory);
        }

        // GET: Product/ProductCatagories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/ProductCatagories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description")] ProductCatagory productCatagory)
        {
            if (ModelState.IsValid)
            {
                db.ProductCatagories.Add(productCatagory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productCatagory);
        }

        // GET: Product/ProductCatagories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCatagory productCatagory = await db.ProductCatagories.FindAsync(id);
            if (productCatagory == null)
            {
                return HttpNotFound();
            }
            return View(productCatagory);
        }

        // POST: Product/ProductCatagories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description")] ProductCatagory productCatagory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productCatagory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productCatagory);
        }

        // GET: Product/ProductCatagories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCatagory productCatagory = await db.ProductCatagories.FindAsync(id);
            if (productCatagory == null)
            {
                return HttpNotFound();
            }
            return View(productCatagory);
        }

        // POST: Product/ProductCatagories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductCatagory productCatagory = await db.ProductCatagories.FindAsync(id);
            db.ProductCatagories.Remove(productCatagory);
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
