using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Our_Ecommerce_store_GIS_40.Models;
using Our_Ecommerce_store_GIS_40.Models.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Our_Ecommerce_store_GIS_40.Controllers
{
    public class BrandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Brands
        [AllowAnonymous]
        public ActionResult Index()
        {
            
            Cat_Pro_Bra_viewModel vM = new Cat_Pro_Bra_viewModel();
            vM.Brands = db.brands.ToList();
            vM.Categories = db.categories.ToList();
            return View(vM);

        }

        // GET: Brands/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat_Pro_Bra_viewModel vM = new Cat_Pro_Bra_viewModel();
            vM.brand = db.brands.Find(id);
            vM.Categories = vM.brand.cat_List.ToList();
            vM.Products = vM.brand.products.ToList();
            vM.Products = db.products.ToList();
            if (vM.brand == null)
            {
                return HttpNotFound();
            }
            return View(vM);
        }

        // GET: Brands/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            Cat_Pro_Bra_viewModel vM = new Cat_Pro_Bra_viewModel();
            vM.Categories = db.categories.ToList();
            return View(vM);
        }

        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create( Brands brand, int? CatId)
        {

            if (ModelState.IsValid)
            {
                var catOfBrand = db.categories.Find(CatId);
                brand.approved = "Yes";
                db.brands.Add(brand);
                db.SaveChanges();
                db.brands.Find(brand.Id).cat_List.Add(catOfBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Cat_Pro_Bra_viewModel vM = new Cat_Pro_Bra_viewModel();
                vM.Categories = db.categories.ToList();
                return View(vM);
            }

        }

        // GET: Brands/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat_Pro_Bra_viewModel vM = new Cat_Pro_Bra_viewModel();
            vM.brand = db.brands.Find(id);
            vM.Categories = db.categories.ToList();
            vM.brand.cat_List = db.brands.Find(id).cat_List.ToList();
            //db.brands.Include(e => e.Name == "LG");
            if (vM.brand == null)
            {
                return HttpNotFound();
            }
            return View(vM);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,Name")] Brands brand, int? Id)
        {

            if (ModelState.IsValid)
            {

                db.Entry(brand).State = EntityState.Modified;
                var cat = db.categories.Find(Id);
                if (cat.Brands.Contains(brand))
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    db.brands.Find(brand.Id).cat_List.Add(cat);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                //var cat=db.brands.Find(brand.Id).catList.Where(e => e.Id == Id);
                //brand.catList.Add(cat);                
            }

            return View(brand);
        }

        
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            Brands brands = db.brands.Find(id);
            db.brands.Remove(brands);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult deleteCatFromBrand(int Id, int idBrand)
        {
            Brands brand = db.brands.Find(idBrand);
            Categories cat = db.categories.Find(Id);
            brand.cat_List.Remove(cat);
            db.SaveChanges();

            Cat_Pro_Bra_viewModel vM = new Cat_Pro_Bra_viewModel();
            vM.brand = db.brands.Find(idBrand);
            vM.Categories = db.categories.ToList();
            vM.brand.cat_List.Where(e => e.Id == Id);
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
