using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTestProject.Models;

namespace UTestProject.Controllers
{
    public class ExamController : Controller
    {

        SubjectDbEntities subjectDb = new SubjectDbEntities();
        StudentDbEntities studentDb = new StudentDbEntities();
        // GET: Exam
        public ActionResult Mock()
        {
            var email = Session["UserEmail"];
            var pass = Session["UserPass"];
            if (email != null && pass != null)
            {
                Student student = studentDb.Students.Where(temp => temp.Email == email && temp.Password == pass).FirstOrDefault();
                List<Subject> subjects = subjectDb.Subjects.Where(temp => temp.Category == student.Category).ToList();

                ViewBag.subjects = subjects;
                return View();

            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        [HttpGet]
        public ActionResult MockExam()
        {
            List<Subject> subjectList = subjectDb.Subjects.ToList();
            ViewBag.subjectList = subjectList;
            //return Content("Hello world");
            return View();
        }

        [ActionName("MockExam")]
        [HttpPost]
        public ActionResult MockExamForm()
        {

            return RedirectToAction("ExamResult");
        }

        public ActionResult ExamResult()
        {
            return View();
        }
        public ActionResult AllExam()
        {
            return View();
        }
    }
}