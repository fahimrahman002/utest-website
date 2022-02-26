using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UTestProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User Profile
        public ActionResult Profile()
        {
            return View();
        }

        // GET: User settings
        public ActionResult Settings()
        {
            return View();
        }
    }
}