using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTestProject.Models;

namespace UTestProject.Controllers
{
    public class HomeController : Controller
    {

        QuestionDbEntities questionDb = new QuestionDbEntities();
        public ActionResult Index()
        {
            var email = Session["UserEmail"];
            var pass = Session["UserPass"];

            if (email != null && pass != null)
            {
                return RedirectToAction("Dashboard", "User");
            }
            else
            {
                Question randomQuestion = questionDb.Questions
                    .OrderBy(temp => Guid.NewGuid())
                    .FirstOrDefault();
                ViewBag.randomQuestion = randomQuestion;
                return View();

            }

        }



    }
}