using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Our_Ecommerce_store_GIS_40.Models;
using Our_Ecommerce_store_GIS_40.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Our_Ecommerce_store_GIS_40.Controllers
{
    public class HomeController : Controller
    {
       
            ApplicationDbContext con = new ApplicationDbContext();
            public ActionResult Index()
            {
            ////creating roles //////

            //IdentityRole admin = new IdentityRole("admin");
            //IdentityRole owner = new IdentityRole("owner");
            //IdentityRole client = new IdentityRole("client");
            ////////creating roles roles to database by creating object of rolemanger which has function called createrole

            //RoleManager<IdentityRole> rolemanger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            //rolemanger.Create(admin);
            //rolemanger.Create(owner);
            // rolemanger.Create(client);

            //assign roles by usermanger/////

            //UserManager<ApplicationUser> usermanger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //ApplicationUser admin = usermanger.FindByEmail("admin@gmail.com"); 
            //ApplicationUser owner = usermanger.FindByEmail("owner1@gmail.com");
            //ApplicationUser client = usermanger.FindByEmail("client1@gmail.com");

            //usermanger.AddToRole(admin.Id, "admin");
            //usermanger.AddToRole(owner.Id, "owner");
            //usermanger.AddToRole(client.Id, "client");

            if (con.categories.ToList().Count == 0) {

                Categories c1 = new Categories();
                Categories c2 = new Categories();
                Categories c3 = new Categories();
                Categories c4 = new Categories();
                c1.Name = "Tvs";
                c2.Name = "Mobile";
                c3.Name = "LabTops";
                c4.Name = "clother";
                con.categories.Add(c1);
                con.categories.Add(c2 );
                con.categories.Add(c3);
                con.categories.Add(c4);
                con.SaveChanges();
            }
            
            ////////////////////////////////////////////////////
            Cat_Pro_Bra_viewModel GVM = new Cat_Pro_Bra_viewModel();
                GVM.Products = con.products.ToList();
                GVM.Categories = con.categories.ToList();


                return View(GVM);
            }
            [HttpGet]
            public ActionResult searching()
            {
            Cat_Pro_Bra_viewModel GVM = new Cat_Pro_Bra_viewModel();
                GVM.Products = con.products.ToList();
                return View(GVM);
            }
            [HttpPost]
            public ActionResult searching(string search)
            {

            if (!string.IsNullOrWhiteSpace(search))
            {
                if (!User.IsInRole("admin"))
                {
            Cat_Pro_Bra_viewModel GVM = new Cat_Pro_Bra_viewModel();
                GVM.Products = con.products.Where(e => (e.Descripton.Contains(search) || e.Name.Contains(search)||e.brand.Name.Contains(search)||e.cat.Name.Contains(search))&&e.Approved=="Yes").ToList();
                return View("searching", GVM);
                }
                else
                {
                    Cat_Pro_Bra_viewModel GVM = new Cat_Pro_Bra_viewModel();
                    GVM.Products = con.products.Where(e => (e.Descripton.Contains(search) || e.Name.Contains(search) || e.brand.Name.Contains(search) || e.cat.Name.Contains(search))).ToList();
                    return View("searching", GVM);
                } 
            }
            else
            {
                return RedirectToAction("index");
            }
            }

            public ActionResult About()
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }

            public ActionResult Contact()
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }
            [Authorize(Roles = "admin")]
            public ActionResult addowner()
            {
                dynamic listOfUsers = con.Users.ToList();
                ViewBag.Message = "addowner";
                return View(listOfUsers);
            }
            [HttpPost]
            [Authorize(Roles = "admin")]
            public ActionResult addowner(string Email)
            {
                dynamic listOfUser = con.Users.ToList();
                UserManager<ApplicationUser> usermanger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                ApplicationUser User = usermanger.FindByEmail(Email);
                usermanger.AddToRole(User.Id, "owner");
                return View("successfulAdding", listOfUser);
            }
            [Authorize(Roles = "admin,owner")]
            public ActionResult addbrandtocatogory()
            {
                return View();
            }
        public ActionResult CartShop()
        {
            Cat_Pro_Bra_viewModel GVM = new Cat_Pro_Bra_viewModel();
            GVM.Products = con.products.ToList();
            return View(GVM);
        }
        }
    }