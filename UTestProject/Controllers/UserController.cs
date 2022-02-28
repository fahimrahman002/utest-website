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
    public class UserController : Controller
    {
        UTestDBEntities1 studentDb = new UTestDBEntities1();
        UTestDBEntities2 categoryDb = new UTestDBEntities2();

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
                Session["UserName"] = students[0].Name;
                Session["UserEmail"] = email;
                Session["UserPass"] = password;
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
            try
            {
                studentDb.Student.Add(student);
                studentDb.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.InnerException is SqlException sqlEx &&
    (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    List<Category> categories = categoryDb.Category.ToList();
                    ViewData["MessageText"] = "Email already exists.";
                    ViewData["MessageType"] = "error";
                    return View(categories);

                }

            }

            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

        public new ActionResult Profile()
        {
            if (Session["UserEmail"] != null && Session["UserPass"] != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("Login");
            }

        }

        // GET: User settings
        public ActionResult Settings()
        {
            if (Session["UserEmail"] != null && Session["UserPass"] != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("Login");
            }

        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            if (Session["UserEmail"] != null && Session["UserPass"] != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("Login");
            }

        }

    }
}