using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Our_Ecommerce_store_GIS_40.Models;
using Microsoft.AspNet.Identity;
using Our_Ecommerce_store_GIS_40.Models.ViewModels;
using Newtonsoft.Json;

namespace Our_Ecommerce_store_GIS_40.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        [Authorize(Roles ="admin,owner")]
        public ActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                return View(db.products.ToList());
            }
            if (User.IsInRole("owner"))
            {
               var xx = db.products.ToList().Where(e => e.owner_Id == User.Identity.GetUserId());

                return View(xx);
            }
            return HttpNotFound();
        }

        // GET: Products/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.products.Find(id);
            if (User.IsInRole("admin"))
            {
                if (products == null)
                {
                    return HttpNotFound();
                }
                return View(products);
            }
            else if(User.IsInRole("owner"))
            {
                if (products == null || products.Approved != "Yes" || products.owner_Id != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }
            return View(products);
            }
            else
            {
                if (products == null || products.Approved != "Yes" )
                {
                    return HttpNotFound();
                }
                return View(products);
            }
        }

        

        // GET: Products/Edit/5
        [Authorize(Roles = "admin,owner")]

        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Products products = db.products.Find(id);
            if (products.owner_Id == User.Identity.GetUserId() || User.IsInRole("admin"))
            {
                if (products == null)
                {
                    return HttpNotFound();
                }
                ViewBag.brand_Id = new SelectList(db.brands, "Id", "Name", products.brand_Id);
                ViewBag.cat_Id = new SelectList(db.categories, "Id", "Name", products.cat_Id);

                return View(products);
            }
            return HttpNotFound();
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "owner,admin")]
        public ActionResult Edit(Products product, HttpPostedFileBase UploadImage)
        {
            if (ModelState.IsValid)
            {


                if (product.owner_Id == User.Identity.GetUserId() || User.IsInRole("admin"))
                {
                    if (product.owner_Id == User.Identity.GetUserId()) { product.Approved = "No"; }

                    if (UploadImage != null)
                    {
                        product.Imageurl = UploadImage.FileName;
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.brand_Id = new SelectList(db.brands, "Id", "Name", product.brand_Id);
            ViewBag.cat_Id = new SelectList(db.categories, "Id", "Name", product.cat_Id);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "admin,owner")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,owner")]

        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.products.Find(id);
            db.products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "admin,owner")]

        public ActionResult create()
        {
            Cat_Pro_Bra_viewModel sss = new Cat_Pro_Bra_viewModel();
            sss.Categories = db.categories.ToList();
            sss.Brands = db.brands.ToList();
            return View(sss);
        }

        public JsonResult Create2(int id)
        {
            Cat_Pro_Bra_viewModel sss = new Cat_Pro_Bra_viewModel();
            var ll = db.categories.Find(id);
            //sss.brand.Where(e => e.catList == ll).ToList();


            string ss = JsonConvert.SerializeObject(ll.Brands.Where(e => e.approved == "Yes"),
                Formatting.None,
                 new JsonSerializerSettings()
                 {
                     ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                 });
            return Json(ss, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,owner")]
        public ActionResult Create(Products product, string NEWBRAND, HttpPostedFileBase UploadImage)
        {
            Cat_Pro_Bra_viewModel cc = new Cat_Pro_Bra_viewModel();
            cc.Categories = db.categories.ToList();
            cc.product = product;
            if (User.IsInRole("admin"))
            {
                cc.product.Approved = "Yes";
            }
            if (NEWBRAND != "")
            {
                Brands brands = new Brands();

                brands.Name = NEWBRAND;
                if (User.IsInRole("admin"))
                {
                    brands.approved = "Yes";
                }
                db.brands.Add(brands);
                db.SaveChanges();
                var cch = db.categories.Find(product.cat_Id);
                cch.Brands.Add(brands);
                db.SaveChanges();
                product.brand_Id = brands.Id;

                product.owner_Id = User.Identity.GetUserId();


                if (UploadImage != null && product.brand_Id != null)
                {
                    ModelState.Remove("product.Imageurl");
                    ModelState.Remove("product.brand_Id");
                    UploadImage.SaveAs(Server.MapPath("/") + "/Content/" + UploadImage.FileName);
                    product.Imageurl = UploadImage.FileName;
                }
                else { return View(cc); }
                if (ModelState.IsValid)

                {
                    db.products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else { return View(cc); }

            }

            if (UploadImage != null && product.brand_Id != null)
            {
                ModelState.Remove("product.Imageurl");
                ModelState.Remove("product.brand_Id");
                product.owner_Id = User.Identity.GetUserId();
                if (UploadImage.ContentType == "image/png" || UploadImage.ContentType == "image/jpg" || UploadImage.ContentType == "image/jpeg")
                {
                    UploadImage.SaveAs(Server.MapPath("/") + "/Content/" + UploadImage.FileName);
                    product.Imageurl = UploadImage.FileName;
                }
            }


            else { return View(cc); }
            if (ModelState.IsValid)
            {

                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else { return View(cc); }

        }
        [Authorize(Roles = "admin,owner")]

        public ActionResult Confirm(int id)
        {
            var pro = db.products.Find(id);
            pro.Approved = "Yes";
            pro.brand.approved = "Yes";
            db.SaveChanges();
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult cofirmproducts()
        {
           
            return View(db.products.ToList().Where(e => e.Approved == "No"));
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
