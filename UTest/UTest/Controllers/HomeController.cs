
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTest.Models;


namespace UTest.Controllers
{
    public class HomeController : Controller
    {
       DataEntities db=new DataEntities();
        [HttpGet]
        public ActionResult SignUp()
        {


            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User us)
        {
            
            
                db.Users.Add(us);
                db.SaveChanges();
            
           
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        

    }
}