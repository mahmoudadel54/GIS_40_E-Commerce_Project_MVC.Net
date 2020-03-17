using Our_Ecommerce_store_GIS_40.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Our_Ecommerce_store_GIS_40.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //
        private string strCart = "Cart";
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult removeone(int id)
        {



            List<cart> lscart = (List<cart>)Session[strCart];
            int check = isExsitingcheck(id);
            if (lscart[check].Quantity > 0)
            {
                lscart[check].Quantity--;
                Session[strCart] = lscart;
            }
            if (lscart[check].Quantity == 0) { lscart.RemoveAt(check); }


            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult orderNow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session[strCart] == null)
            {
                List<cart> lscart = new List<cart>
                {
                    new cart (db.products.Find(id),1)
                };


                Session[strCart] = lscart;
            }
            else
            {

                List<cart> lscart = (List<cart>)Session[strCart];
                int check = isExsitingcheck(id);
                if (check == -1) { lscart.Add(new cart(db.products.Find(id), 1)); }
                else
                {
                    lscart[check].Quantity++;
                    Session[strCart] = lscart;
                }

            }

            return RedirectToAction("index");
        }

        private int isExsitingcheck(int? id)
        {
            List<cart> lscart = (List<cart>)Session[strCart];
            for (int i = 0; i < lscart.Count; i++)
            {
                if (lscart[i].Product.Id == id) { return i; }

            }
            return -1;
        }
        [HttpGet]
        public ActionResult Checkout()
        {
            return View("Checkout");
        }
        [HttpPost]
        public ActionResult ProcessOrder(FormCollection frc)
        {
            List<cart> lscart = (List<cart>)Session[strCart];
            //1- Save order into Order table in database
            Order order = new Order()
            {
                CustomerName = frc["custName"],
                CustomerPhone = frc["custPhone"],
                CustomerMail = frc["custEmail"],
                CustomerAddress = frc["custAddress"],
                date = DateTime.Now

            };
            db.Order.Add(order);
            db.SaveChanges();

            //2- Save the order detail for each product in database
            foreach (cart cart in lscart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    FK_order=order.OrderID,
                    FK_Product=cart.Product.Id,
                    Quantity=cart.Quantity,
                    Price=cart.Product.Cost*cart.Quantity
                };
                db.OrderDetail.Add(orderDetail);
                db.SaveChanges();
            }
            //3- remove the cart from session
            Session.Remove(strCart);
            return View("OrderSuccess");
        }
    }
}