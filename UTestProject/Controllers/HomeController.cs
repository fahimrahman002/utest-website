using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTestProject.Models;

namespace UTestProject.Controllers
{
    public class HomeController : Controller
    {
        UTestDBEntities1 studentDb = new UTestDBEntities1();
        UTestDBEntities2 categoryDb = new UTestDBEntities2();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [ActionName("Login")]
        [HttpPost]
        public ActionResult LoginForm()
        {
            string email = Request.Form["emailInput"];
            string password = Request.Form["passwordInput"];
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Response.Redirect(Request.RawUrl);
            }

            List<Student> students = studentDb.Student.Where(temp => temp.Email.Equals(email) && temp.Password.Equals(password)).ToList();
            if (students.Count == 0)
            {
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                return RedirectToAction("Dashboard");
            }

            return View();

        }
        [HttpGet]
        public ActionResult SignUp()
        {
            List<Category> categories = categoryDb.Category.ToList();

            return View(categories);
        }

        [ActionName("SignUp")]
        [HttpPost]
        public ActionResult SignUpForm()
        {
            string name = Request.Form["nameInput"];
            string email = Request.Form["emailInput"];
            string password = Request.Form["passwordInput"];
            string category = Request.Form["categoryInput"];
            Student student = new Student() { Name = name, Email = email, Password = password, Category = category };
            studentDb.Student.Add(student);
            studentDb.SaveChanges();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }


    }
}