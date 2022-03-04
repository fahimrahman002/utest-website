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
        // GET: Exam
        public ActionResult Mock()
        {
            List<Subject> subjects = subjectDb.Subjects.ToList();

            ViewBag.subjects = subjects;
            return View();
        }

        public ActionResult MockExam()
        {
            List<Subject> subjectList = subjectDb.Subjects.ToList();
            ViewBag.subjectList = subjectList;
            //return Content("Hello world");
            return View();
        }


        public ActionResult AllExam()
        {
            return View();
        }
    }
}