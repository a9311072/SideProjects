using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MVC_Carts.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            //取得目前登入使用者Id
            string userId = HttpContext.User.Identity.GetUserId();
            if (userId == null) return RedirectToAction("Login", "Account");

            List<Models.Product> result = new List<Models.Product>();
            ViewBag.ResultMessage = TempData["ResultMessage"];

            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                result = (from p in db.Products select p).ToList();
            }
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Product postback)
        {
            if (this.ModelState.IsValid)
            {
                using (Models.CartsEntities db = new Models.CartsEntities())
                {
                    db.Products.Add(postback);
                    db.SaveChanges();

                    TempData["ResultMessage"] = String.Format("商品[{0}]已成功建立", postback.Name);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.ResultMessage = "資料有誤, 請檢查資料";
            return View(postback);
        }

        public ActionResult Edit(int id)
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from p in db.Products where p.Id == id select p).FirstOrDefault();
                if (result != default(Models.Product)) { return View(result); }

                TempData["ResultMessage"] = String.Format("商品資料有誤, 請重新操作");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Models.Product postback)
        {
            if (this.ModelState.IsValid)
            {
                using (Models.CartsEntities db = new Models.CartsEntities())
                {
                    var result = (from p in db.Products where p.Id == postback.Id select p).FirstOrDefault();

                    result.Name = postback.Name; result.PublishDate = postback.PublishDate;
                    result.Price = postback.Price; result.Quantity = postback.Quantity;
                    result.Status = postback.Status; result.CategoryId = postback.CategoryId;
                    result.DefaultImageId = postback.DefaultImageId;
                    result.Description = postback.Description;
                    result.DefaultImageURL = postback.DefaultImageURL;

                    db.SaveChanges();

                    TempData["ResultMessage"] = String.Format("商品[{0}]已成功編輯", postback.Name);
                    return RedirectToAction("Index");
                }
            }
            return View(postback);
        }

        public ActionResult Delete(int id)
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from p in db.Products where p.Id == id select p).FirstOrDefault();
                if (result != default(Models.Product)) {
                    db.Products.Remove(result);
                    db.SaveChanges();

                    TempData["ResultMessage"] = String.Format("商品{0}成功刪除", result.Name);
                    return RedirectToAction("Index");
                }
                TempData["ResultMessage"] = String.Format("商品資料有誤, 請重新操作");
                return RedirectToAction("Index");
            }
        }

    }
}