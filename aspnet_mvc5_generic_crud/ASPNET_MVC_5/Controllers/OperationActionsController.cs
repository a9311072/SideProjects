﻿using System;
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
    public class OperationActionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OperationActions
        public async Task<ActionResult> Index()
        {
            return View(await db.OperationActions.ToListAsync());
        }

        // GET: OperationActions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationAction operationAction = await db.OperationActions.FindAsync(id);
            if (operationAction == null)
            {
                return HttpNotFound();
            }
            return View(operationAction);
        }

        // GET: OperationActions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OperationActions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] OperationAction operationAction)
        {
            if (ModelState.IsValid)
            {
                db.OperationActions.Add(operationAction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(operationAction);
        }

        // GET: OperationActions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationAction operationAction = await db.OperationActions.FindAsync(id);
            if (operationAction == null)
            {
                return HttpNotFound();
            }
            return View(operationAction);
        }

        // POST: OperationActions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] OperationAction operationAction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operationAction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(operationAction);
        }

        // GET: OperationActions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationAction operationAction = await db.OperationActions.FindAsync(id);
            if (operationAction == null)
            {
                return HttpNotFound();
            }
            return View(operationAction);
        }

        // POST: OperationActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OperationAction operationAction = await db.OperationActions.FindAsync(id);
            db.OperationActions.Remove(operationAction);
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
