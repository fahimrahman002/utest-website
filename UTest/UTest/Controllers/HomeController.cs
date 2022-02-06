using UTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace UTest.Controllers
{
    public class HomeController : Controller
    {       Login2 db =new Login2();

        /* public ActionResult Signup()
         {

             return View();
         }

         [HttpPost]
         public ActionResult Signup(User us)
         {
             db.Users.Add(us);
             db.SaveChangesAsync();
            // db.SaveChanges();
             return RedirectToAction("Login");
         }
         [HttpGet]
         public ActionResult Login()
         {

             return View();
         }

         [HttpPost]
         public ActionResult Login(User us)
         {
             var obj=db.Users.Where(x => x.Email.Equals(us.Email) && x.Password.Equals(us.Password)).FirstOrDefault();
             if (obj != null)
             {
                 return RedirectToAction("Dashboard");
             }
            return View();
         }
         public ActionResult Dashboard()
         {

             return View();
         }*/
        [HttpGet]
        public ActionResult Signup()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Signup(User us)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(us);
                db.SaveChangesAsync();
            }
           
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public ActionResult Create(User us)
        {
            if (ModelState.IsValid)
            {
               
                db.Users.Add(us);
          
                db.SaveChanges();
               
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
         public ActionResult Login()
         {

             return View();
         }

         [HttpPost]
         public ActionResult Login(User us)
         {
             var obj=db.Users.Where(x => x.Email.Equals(us.Email) && x.Password.Equals(us.Password)).FirstOrDefault();
             if (obj != null)
             {
                 return RedirectToAction("Dashboard");
             }
            return View();
         }
         public ActionResult Dashboard()
         {

             return View();
         }

    }
}