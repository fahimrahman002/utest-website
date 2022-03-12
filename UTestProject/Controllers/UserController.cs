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
        ExamDbEntities examDb = new ExamDbEntities();

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

        [HttpPost]
        public ActionResult GoogleAuth(string name, string user_name, string email, string picture, string token)
        {

            if (ModelState.IsValid)
            {
                Student student = studentDb.Students.Where((temp) => temp.Email == email && temp.Password == token).FirstOrDefault();

                if (student != null)
                {
                    Session["UserName"] = student.Name;
                    Session["UserEmail"] = student.Email;
                    Session["UserPass"] = student.Password;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    Student newStudent = new Student() { Name = name, Email = email, Password = token, Category = 1 };
                    try
                    {
                        studentDb.Students.Add(newStudent);
                        studentDb.SaveChanges();
                        Session["UserName"] = newStudent.Name;
                        Session["UserEmail"] = newStudent.Email;
                        Session["UserPass"] = newStudent.Password;
                        return RedirectToAction("Dashboard");

                    }
                    catch (DbUpdateException ex)
                    {
                        if (ex.InnerException?.InnerException is SqlException sqlEx &&
            (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                        {

                            ViewData["MessageText"] = "Email already exists.";
                            ViewData["MessageType"] = "error";
                            return View("Signup");

                        }

                    }

                }

                return RedirectToAction("Dashboard");
            }


            return RedirectToAction("Dashboard");
        }


        [ActionName("SignUp")]
        [HttpPost]
        public ActionResult SignUpForm()
        {
            List<Category> categories = categoryDb.Categories.ToList();
            ViewBag.categories = categories;

            string name = Request.Form["nameInput"];
            string email = Request.Form["emailInput"];
            string password = Request.Form["passwordInput"];
            int category = int.Parse(Request.Form["categoryInput"]);
            if (password.Length < 6)
            {
                ViewData["MessageText"] = "Password must be minimum 6 characters";
                ViewData["MessageType"] = "error";
                return View();

            }
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
            var email = Session["UserEmail"];
            var pass = Session["UserPass"];
            if (email != null && pass != null)
            {


                int? totalCorrectAns = 0, totalWrongAns = 0, totalSkippedAns = 0;
                double? totalPoint = 0;
                int totalExam = 0;
                Student student = studentDb.Students.Where(temp => temp.Email == email && temp.Password == pass).FirstOrDefault();
                List<Exam> examList = examDb.Exams.Where(temp => temp.Student == student.ID).ToList();
                totalExam = examList.Count();
                foreach (Exam exam in examList)
                {
                    totalCorrectAns += exam.Total_correct_ans;
                    totalWrongAns += exam.Total_wrong_ans;
                    totalSkippedAns += exam.Total_skipped_ans;
                    totalPoint += exam.Obtained_marks;
                }
                List<Ranking> rankingList = getRankList(student);
                int studentRank = 1;
                foreach (Ranking ranking in rankingList)
                {
                    if (ranking.studentId == student.ID) break;
                    studentRank++;
                }


                ViewBag.totalCorrectAns = totalCorrectAns;
                ViewBag.totalWrongAns = totalWrongAns;
                ViewBag.totalSkippedAns = totalSkippedAns;
                ViewBag.totalPoint = totalPoint;
                ViewBag.totalExam = totalExam;
                ViewBag.studentRank = studentRank;

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
                int category = int.Parse(Request.Form["categoryInput"]);
                Student student = studentDb.Students.Where(temp => temp.Email == email && temp.Password == pass).FirstOrDefault();
                student.Name = name;
                student.About = about;
                student.Category = category;
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
                List<Category> categories = categoryDb.Categories.ToList();

                ViewBag.categories = categories;
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
            var email = Session["UserEmail"];
            var pass = Session["UserPass"];
            if (email != null && pass != null)
            {
                Student student = studentDb.Students
                .Where(temp => temp.Email == email && temp.Password == pass).FirstOrDefault();

                // Leaderboard summery
                List<Ranking> rankingList = getRankList(student);
                ViewBag.rankingList = rankingList.Take(5);

                // Student profile summery(Total correct ans, wrong ans, skipped ans)
                int? totalCorrectAns = 0, totalWrongAns = 0, totalSkippedAns = 0;
                List<Exam> examList = examDb.Exams.Where(temp => temp.Student == student.ID).ToList();
                foreach (Exam exam in examList)
                {
                    totalCorrectAns += exam.Total_correct_ans;
                    totalWrongAns += exam.Total_wrong_ans;
                    totalSkippedAns += exam.Total_skipped_ans;
                }
                ViewBag.totalCorrectAns = totalCorrectAns;
                ViewBag.totalWrongAns = totalWrongAns;
                ViewBag.totalSkippedAns = totalSkippedAns;
                return View();
            }
            else
            {

                return RedirectToAction("Login");
            }

        }

        public class Ranking
        {
            public int? studentId { get; set; }
            public string studentName { get; set; }
            public double? totalPoint { get; set; }
        }

        public List<Ranking> getRankList(Student curStudent)
        {
            // Ranking List
            List<Ranking> rankingList = new List<Ranking>();


            // All students by category
            List<Student> students = studentDb.Students
                .Where(temp => temp.Category == curStudent.Category)
                .ToList();

            // Student Dictionary for accessing student name by id quickly
            IDictionary<int?, string> studentDic = new Dictionary<int?, string>();
            foreach (Student item in students)
            {
                studentDic.Add(item.ID, item.Name);
            }

            // Exams group by student
            var examGroupByStudent = examDb.Exams
                .Where(temp => temp.Category == curStudent.Category)
                .GroupBy(e => e.Student);

            foreach (var group in examGroupByStudent)
            {
                int? studentID = group.Key;
                string studentName = studentDic[studentID];
                double? obtainedMarks = group.Max(temp => temp.Obtained_marks);
                rankingList.Add(new Ranking { studentId = studentID, studentName = studentName, totalPoint = obtainedMarks });
            }

            return rankingList;

        }

        [HttpGet]
        public ActionResult Leaderboard()
        {
            var email = Session["UserEmail"];
            var pass = Session["UserPass"];

            if (email != null && pass != null)
            {
                Student student = studentDb.Students
                .Where(temp => temp.Email == email && temp.Password == pass).FirstOrDefault();

                List<Ranking> rankingList = getRankList(student);

                ViewBag.rankingList = rankingList;
                return View();
            }
            else
            {

                return RedirectToAction("Login");
            }

        }

    }
}