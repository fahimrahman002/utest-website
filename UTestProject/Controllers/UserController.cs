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
        StudentDbEntities studentDb = new StudentDbEntities();
        CategoryDbEntities categoryDb = new CategoryDbEntities();

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["UserEmail"] != null && Session["UserPass"] != null)
            {
                return RedirectToAction("Dashboard");
            }
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

            Student student = studentDb.Students.Where(temp => temp.Email.Equals(email) && temp.Password.Equals(password)).FirstOrDefault();

            if (student == null)
            {
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                Session["UserName"] = student.Name;
                Session["UserEmail"] = email;
                Session["UserPass"] = password;
                return RedirectToAction("Dashboard");
            }

            return View();

        }
        [HttpGet]
        public ActionResult SignUp()
        {
            if (Session["UserEmail"] != null && Session["UserPass"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            List<Category> categories = categoryDb.Categories.ToList();
            ViewBag.categories = categories;
            return View();
        }

        [ActionName("SignUp")]
        [HttpPost]
        public ActionResult SignUpForm()
        {

            string name = Request.Form["nameInput"];
            string email = Request.Form["emailInput"];
            string password = Request.Form["passwordInput"];
            int category = int.Parse(Request.Form["categoryInput"]);
            Student student = new Student() { Name = name, Email = email, Password = password, Category = category };
            try
            {
                studentDb.Students.Add(student);
                studentDb.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.InnerException is SqlException sqlEx &&
    (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    List<Category> categories = categoryDb.Categories.ToList();
                    ViewBag.categories = categories;
                    ViewData["MessageText"] = "Email already exists.";
                    ViewData["MessageType"] = "error";
                    return View();

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
        [HttpPost]
        public ActionResult UpdatePassword()
        {
            var email = Session["UserEmail"];
            var pass = Session["UserPass"];
            if (email != null && pass != null)
            {
                var newPassword = Request.Form["newPassword"];
                var confirmPassword = Request.Form["confirmPassword"];
                if (newPassword != confirmPassword)
                {
                    ViewData["MessageText"] = "Confirm Password mismatch";
                    ViewData["MessageType"] = "error";

                    return RedirectToAction("Settings");

                }
                if (newPassword != null && confirmPassword != null)
                {
                    Student student = studentDb.Students.Where(temp => temp.Email == email && temp.Password == pass).FirstOrDefault();
                    student.Password = newPassword;
                    Session["UserPass"] = newPassword;
                    studentDb.SaveChanges();

                    ViewBag.student = student;

                    ViewData["MessageText"] = "Profile updated successfully";
                    ViewData["MessageType"] = "success";

                    return RedirectToAction("Settings");
                }

            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult UpdateProfile()
        {
            var email = Session["UserEmail"];
            var pass = Session["UserPass"];
            if (email != null && pass != null)
            {
                var name = Request.Form["userName"];
                var about = Request.Form["userAbout"];
                Student student = studentDb.Students.Where(temp => temp.Email == email && temp.Password == pass).FirstOrDefault();
                student.Name = name;
                student.About = about;
                studentDb.SaveChanges();

                Session["UserName"] = name;

                ViewBag.student = student;

                ViewData["MessageText"] = "Profile updated successfully";
                ViewData["MessageType"] = "success";

                return RedirectToAction("Settings");
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        // GET: User settings
        public ActionResult Settings()
        {
            var email = Session["UserEmail"];
            var pass = Session["UserPass"];
            if (email != null && pass != null)
            {
                Student student = studentDb.Students.Where(temp => temp.Email == email && temp.Password == pass).FirstOrDefault();

                ViewBag.student = student;

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