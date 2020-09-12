using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MVC_Carts.Models;

namespace MVC_Carts.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Models.OrderModel.Ship postback)
        {
            if( ModelState.IsValid )
            {   //取得目前購物車
                var currentcart = MVC_Carts.Models.Operation.GetCurrentCart();

                //取得目前登入使用者Id
                string userId = HttpContext.User.Identity.GetUserId();
                if(userId == null) return RedirectToAction("Login","Account");

                using ( MVC_Carts.Models.CartsEntities db = new MVC_Carts.Models.CartsEntities() )
                {
                    //建立Order物件
                    var order = new MVC_Carts.Models.Order()
                    {
                        UserId = userId,
                        ReceiverName = postback.RecieverName,
                        ReceiverPhone = postback.RecieverPhone,
                        ReceiverAddress = postback.RecieverAddress
                    };
                    //加其入Orders資料表後，儲存變更
                    db.Orders.Add(order);
                    db.SaveChanges();

                    //取得購物車中OrderDetai物件
                    var orderDetails = currentcart.ToOrderDetailList(order.Id);

                    //更新商品庫存數量
                    for (int i = 0; i < orderDetails.Count; i++)
                    {
                        int productId = Convert.ToInt32(orderDetails[i].Id);
                        var result = (from p in db.Products where p.Id == productId select p).FirstOrDefault();
                        result.Quantity = result.Quantity - orderDetails[i].Quantity;
                    }

                    //將其加入OrderDetails資料表後，儲存變更
                    db.OrderDetails.AddRange(orderDetails);
                    db.SaveChanges();

                    //清空購物車商品
                    currentcart.ClearCart();
                }
                //DB連線釋放
            }
            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult MyOrder()
        {
            //取得目前登入使用者Id
            var userId = HttpContext.User.Identity.GetUserId();

            using (MVC_Carts.Models.CartsEntities db = new MVC_Carts.Models.CartsEntities())
            {
                var result = (from s in db.Orders
                              where s.UserId == userId
                              select s).ToList();

                return View(result);
            }
        }

        public ActionResult MyOrderDetail(int id)
        {
            using (MVC_Carts.Models.CartsEntities db = new MVC_Carts.Models.CartsEntities())
            {
                var result = (from s in db.OrderDetails
                              where s.OrderId == id
                              select s).ToList();

                if (result.Count == 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result);
                }
            }
        }

    }
}

